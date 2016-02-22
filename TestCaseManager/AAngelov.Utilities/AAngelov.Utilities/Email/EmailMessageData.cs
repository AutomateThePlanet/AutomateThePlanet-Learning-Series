using System;
using System.Collections.Generic;
using System.Linq;

namespace AAngelov.Utilities.Email
{
    /// <summary>
    /// Contains Email Message Properties
    /// </summary>
    public class EmailMessageData
    {
        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>
        /// The emails.
        /// </value>
        public List<string> Emails { get; set; }

        /// <summary>
        /// Gets or sets from email.
        /// </summary>
        /// <value>
        /// From email.
        /// </value>
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the email body.
        /// </summary>
        /// <value>
        /// The email body.
        /// </value>
        public string EmailBody { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the attachment paths.
        /// </summary>
        /// <value>
        /// The attachment paths.
        /// </value>
        public List<string> AttachmentPaths { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageData"/> class.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="emailBody">The email body.</param>
        /// <param name="subject">The subject.</param>
        public EmailMessageData(List<string> emails, string fromEmail, string emailBody, string subject)
        {
            this.Emails = emails;
            this.FromEmail = fromEmail;
            this.EmailBody = emailBody;
            this.Subject = subject;
            this.AttachmentPaths = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessageData"/> class.
        /// </summary>
        /// <param name="emails">The emails.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="emailBody">The email body.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="attachmentPath">The attachment path.</param>
        public EmailMessageData(List<string> emails, string fromEmail, string emailBody, string subject, List<string> attachmentPath) : this(emails, fromEmail, emailBody, subject)
        {
            this.Emails = emails;
            this.FromEmail = fromEmail;
            this.EmailBody = emailBody;
            this.Subject = subject;
            this.AttachmentPaths = attachmentPath;
        }
    }
}