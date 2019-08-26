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
using log4net;
using Reset.Utils;
using Spring.Messaging.Nms.Listener.Adapter;
using Spring.Messaging.Nms.Listener;
using System.Threading;

using Reset.ServiceBus;
using Spring.Objects.Factory.Config;
using System.ComponentModel;

namespace EmailDelivery
{
    [SpringObjectPopulator.SpringContextsAttribute("assembly://Reset.ServiceBus/Reset.ServiceBus.BaseServiceClient/xml")]
    public class EmailDeliveryClient : BaseServiceClientExtended<Messages.EmailDeliveryRequest>
    {
        private Messages.EmailDeliveryResponse _response;

        public EmailDeliveryClient()
        {
            CreateContext();
        }

        public EmailDeliveryClient(string correlationId)
        {
            PlaceHolders["Client.CorrelationIdBase"] = correlationId;
            CreateContext();
        }

        protected override void OnInsufficientActivityMethod(object sender, System.EventArgs e)
        {
        }

        protected override SimpleMessageResponse<Messages.EmailDeliveryRequest> CreateResponseMessage(Messages.EmailDeliveryRequest request, ResponseStates status)
        {
            return request.CreateResponse(status);
        }

        protected override Spring.Objects.Factory.Config.IVariableSource DestinationProvider
        {
            get
            {
                return new DestinationsVariableSource();
            }
        }

        public Messages.EmailDeliveryResponse Response
        {
            get
            {
                return _response;
            }
        }

        protected override void OnMessageReceived(object sender, SimpleMessageResponse<Messages.EmailDeliveryRequest> message)
        {
            base.OnMessageReceived(sender, message);
            _response = (Messages.EmailDeliveryResponse)message;
        }

    }
}