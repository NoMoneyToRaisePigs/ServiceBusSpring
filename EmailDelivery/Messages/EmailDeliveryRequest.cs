using System;
using Reset.Utils;
using Reset.ServiceBus;

namespace EmailDelivery.Messages
{
    public class EmailDeliveryRequest : SimpleMessageBase
    {
        public System.Net.Mail.MailMessage mailMessage;

        public SerializableDictionary<string, string> Parameters = new SerializableDictionary<string, string>();

        public EmailDeliveryResponse CreateResponse(ResponseStates status, string ExtraCorrelation = "")
        {
            EmailDeliveryResponse mess = new EmailDeliveryResponse();
            mess.MessageHeader.NmsCorrelationId = this.MessageHeader.NmsCorrelationId + ExtraCorrelation;
            mess.Request = this;
            mess.Status = status;
            return mess;
        }

        public override string ToString()
        {
            try
            {
                return "xxx" + "-" + "yyy";
            }
            catch (Exception ex)
            {
                return base.ToString();
            }
        }
    }
}
