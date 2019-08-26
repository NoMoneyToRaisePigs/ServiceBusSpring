
using Reset.ServiceBus;

namespace EmailDelivery.Messages
{
    public class EmailDeliveryResponse : SimpleMessageResponse<EmailDeliveryRequest>
    {
        public string LogFilePath;
        public string StdOut;
        public string StdErr;
        public int ReturnCode;
    }
}
