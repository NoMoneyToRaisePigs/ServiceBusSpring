using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Spring.Messaging.Nms.Core;
using Reset.Utils;
using System.ComponentModel;
using Reset.ServiceBus;

namespace EmailDelivery
{
    [SpringObjectPopulator.SpringContextsAttribute("assembly://EmailDelivery/EmailDelivery.EmailDeliveryService/xml")]
    public class EmailDeliveryService : BaseService
    {
        [Serializable()]
        public class Launcher : AppDomainLauncher<EmailDeliveryService>
        {
        }

        private Messages.EmailDeliveryRequest _currentRequest;

        private const int MAX_LOGGERS_TO_RETAIN = 5;

        [SpringObjectPopulator.SpringVariableName()]
        protected SimpleMessageListenerContainer StatusRequestsListener;

        [SpringObjectPopulator.SpringVariableName()]
        protected NmsTemplate StatusResponseEndpoint;

        public EmailDeliveryService()
        {
            CreateContext();

            RequestsListener.Start();
        }


        private void OnMessageReceived(Messages.EmailDeliveryRequest message)
        {

            _currentRequest = message;



            Console.WriteLine(String.Format("Model intitiator request received: {0}", message.ToString()));

            Messages.EmailDeliveryResponse response = new Messages.EmailDeliveryResponse();


            // Let the user know we're starting
            ReplyToTemplate.ConvertAndSend(message.CreateResponse(ResponseStates.Success, ".STARTING"));


            // SendEmail
            response = RunSendEmail(message);


            Console.WriteLine("Complete");

            // Send the actual response
            ReplyToTemplate.ConvertAndSend(response);


        }


        private Messages.EmailDeliveryResponse RunSendEmail(Messages.EmailDeliveryRequest message)
        {
            var response = message.CreateResponse(ResponseStates.Failed);

            //if (Util.EmailUtils.SendEmail(message.mailMessage))
            //{
            //    response.Status = ResponseStates.Success;
            //}

            response.Status = ResponseStates.Success;

            return response;
        }

        public override Spring.Objects.Factory.Config.IVariableSource DestinationsVariableSource
        {
            get
            {
                return new DestinationsVariableSource();
            }
        }

        public override string TypeIdFieldName
        {
            get
            {
                return "";
            }
        }

        // Some of these messages wont have __type__ fields so look them up from their class names
        public override System.Type ToType(string typeId)
        {
            var ns = typeof(Messages.EmailDeliveryRequest).Namespace;
            var ret = Type.GetType(ns + "." + typeId);
            if (ret == null)
            {
                var t = typeof(Reset.ServiceBus.Messages.CancellationMessage);
                ret = t.Assembly.GetType(t.Namespace + "." + typeId);
            }
            return ret;
        }
    }

    public class DestinationsVariableSource : Reset.ServiceBus.DestinationsVariableSource
    {
        public virtual string StatusRequestResponseTopic
        {
            get
            {
                return BuildQueueName(true, "Status");
            }
        }

        public override string ServiceBaseName
        {
            get
            {
                return "ModelInitiator";
            }
        }
    }
}
