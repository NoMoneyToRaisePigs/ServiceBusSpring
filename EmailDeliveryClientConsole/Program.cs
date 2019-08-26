using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailDelivery;
using EmailDelivery.Messages;
using Reset.ServiceBus;
using System.Net.Mail;

namespace EmailDeliveryClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            sendEmail();
        }

        static void sendEmail()
        {
            using (EmailDeliveryClient edc = new EmailDeliveryClient())
            {

                EmailDeliveryRequest req = new EmailDeliveryRequest();

                MailAddress from = new MailAddress("gaofan@test.com");
                MailAddress to = new MailAddress("gaofan@test.net");
                req.mailMessage = new MailMessage(from, to);

                edc.OnResponseReceived += (object sender, SimpleMessageResponse<EmailDeliveryRequest> e) =>
                {
                    Console.WriteLine("Received: " + e.MessageHeader.NmsCorrelationId);
                };

                EmailDeliveryRequest[] x = new EmailDeliveryRequest[1] { req };

                edc.SubmitRequest(x, false);

                while (!edc.WaitForComplete(250))
                {
                }

                Console.WriteLine("xxxx");
            }
        }
    }
}
