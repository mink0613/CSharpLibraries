using System;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CommonLibrary
{
    public class Email
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="id">Sender's email id</param>
        /// <param name="password">Sender's email password</param>
        /// <param name="displayName">Display name</param>
        /// <param name="targetEmailList">Receiver's email (multiple) addresses.</param>
        /// <param name="attachments">(Multiple> attachments if needed.</param>
        /// <param name="bodies">Messages to send to receiver</param>
        /// <returns></returns>
        public static bool SendEmail(string id, string password, string displayName, string [] targetEmailList, string [] attachments, params string [] bodies)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // Use gmail
            client.UseDefaultCredentials = false; // Do not use default credentials.
            client.EnableSsl = true;  // Use SSL.
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // Use this option to get authenticated from gmail.
            client.Credentials = new System.Net.NetworkCredential(id, password);

            MailAddress from = new MailAddress(id + "@gmail.com", displayName, Encoding.UTF8);
            MailAddress to = new MailAddress(targetEmailList[0]);

            MailMessage message = new MailMessage(from, to);
            if (targetEmailList.Count() > 1)
            {
                for (int i = 1; i < targetEmailList.Count(); i++)
                {
                    message.To.Add(new MailAddress(targetEmailList[i]));
                }
            }

            foreach (string body in bodies)
            {
                message.Body += body + "\n";
            }

            message.BodyEncoding = Encoding.UTF8;

            if (attachments != null && attachments.Count() > 0)
            {
                foreach (string attachment in attachments)
                {
                    Attachment file = new Attachment(attachment);
                    message.Attachments.Add(file);
                }
            }

            bool result = false;

            try
            {
                // Send email
                client.Send(message);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                // Clean up.
                message.Dispose();
                client.Dispose();
            }

            return result;
        }
    }
}
