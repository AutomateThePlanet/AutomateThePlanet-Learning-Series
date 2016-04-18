// <copyright file="EmailSender.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
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