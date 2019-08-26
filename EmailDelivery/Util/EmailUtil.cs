using System;
using System.Net;
using System.Net.Mail;

namespace EmailDelivery.Util
{
    public static class EmailUtils
    {
        private readonly static log4net.ILog _log = log4net.LogManager.GetLogger(typeof(EmailUtils));

        private const string SmtpServer = "10.138.160.15";
        private const string DefaultEmailBcc = "portal.devs@reset.net";
        private const string DefaultEmailSender = "it-test-DEV@reset.net";

        public static bool SendEmail(MailMessage msg)
        {
            _log.InfoFormat("Sending email to {0} - subject: {1}", msg.To, msg.Subject);

            /* TODO ERROR: Skipped IfDirectiveTrivia *//* TODO ERROR: Skipped DisabledTextTrivia *//* TODO ERROR: Skipped EndIfDirectiveTrivia */
            try
            {
                msg.Bcc.Add(DefaultEmailBcc);

                SmtpClient smtpServer = new SmtpClient(SmtpServer)
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials
                };

                // TODO: Handle AllowSendingToRealCustomers !!! Cross check that all emails end in @reset.net

                smtpServer.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Unable to sending email to {0} - subject: {1}", msg.To, msg.Subject), ex);
            }

            return false;
        }

        public static bool SendEmailWithoutBcc(MailMessage msg)
        {
            _log.InfoFormat("Sending email to {0} - subject: {1}", msg.To, msg.Subject);

            try
            {
                if (msg.From == null)
                    msg.From = new MailAddress(DefaultEmailSender);

                SmtpClient smtpServer = new SmtpClient(SmtpServer)
                {
                    Credentials = CredentialCache.DefaultNetworkCredentials
                };

                // TODO: Handle AllowSendingToRealCustomers !!! Cross check that all emails end in @reset.net

                smtpServer.Send(msg);

                return true;
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Unable to sending email to {0} - subject: {1}", msg.To, msg.Subject), ex);
            }

            return false;
        }
    }
}
