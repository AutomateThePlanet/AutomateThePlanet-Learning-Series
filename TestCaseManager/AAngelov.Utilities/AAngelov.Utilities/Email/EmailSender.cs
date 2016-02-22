using System.Linq;
using System.Net.Mail;

namespace AAngelov.Utilities.Email
{
    /// <summary>
    /// Contains Methods for Email Sending
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        public static string CurrentSmtpClient { get; set; }

        /// <summary>
        /// Gets or sets the current SMTP client port.
        /// </summary>
        /// <value>
        /// The current SMTP client port.
        /// </value>
        public static int CurrentSmtpClientPort { get; set; }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="emailData">The email data.</param>
        public static void SendEmail(EmailMessageData emailData)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient(CurrentSmtpClient);
            smtpServer.Port = CurrentSmtpClientPort;

            mail.From = new MailAddress(emailData.FromEmail);

            foreach (string currentEmail in emailData.Emails)
            {
                mail.To.Add(currentEmail);
            }

            mail.Subject = emailData.Subject;
            mail.IsBodyHtml = true;
            mail.Body = emailData.EmailBody;

            AddAttachmentsToEmail(emailData, mail);

            smtpServer.Send(mail);

            DisposeAllAttachments(mail);
            mail.Dispose();
            smtpServer.Dispose();
        }

        /// <summary>
        /// Adds the attachments to email.
        /// </summary>
        /// <param name="emailData">The email data.</param>
        /// <param name="mail">The mail.</param>
        private static void AddAttachmentsToEmail(EmailMessageData emailData, MailMessage mail)
        {
            foreach (string currentPath in emailData.AttachmentPaths)
            {
                Attachment attachment = new Attachment(currentPath);
                mail.Attachments.Add(attachment);
            }
        }

        /// <summary>
        /// Disposes all attachments.
        /// </summary>
        /// <param name="mail">The mail.</param>
        private static void DisposeAllAttachments(MailMessage mail)
        {
            foreach (Attachment currentAttachment in mail.Attachments)
            {
                currentAttachment.Dispose();
            }
        }
    }
}