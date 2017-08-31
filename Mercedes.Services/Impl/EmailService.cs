using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace Mercedes.Services.Impl
{
    public class EmailService : IEmailService
    {
        public void SendEmail(EmailAccount emailAccount, string subject, string body, string fromAddress, string fromName, string toAddress, string toName, string replyToAddress = null, string replyToName = null, IEnumerable<string> bcc = null, IEnumerable<string> cc = null, string attachmentFilePath = null, string attachmentFileName = null, int attachedDownloadId = 0, IDictionary<string, string> headers = null)
        {
            var message = new MailMessage();
            message.From = new MailAddress(fromAddress, fromName);
            message.To.Add(new MailAddress(toAddress, toName));
            if (!String.IsNullOrEmpty(replyToAddress))
            {
                message.ReplyToList.Add(new MailAddress(replyToAddress, replyToName));
            }
            //BCC
            if (bcc != null)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }
            //CC
            if (cc != null)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }
            //content
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            //headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    message.Headers.Add(header.Key, header.Value);
                }
            }
            if (!String.IsNullOrEmpty(attachmentFilePath) &&
                File.Exists(attachmentFilePath))
            {
                var attachment = new Attachment(attachmentFilePath);
                attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentFilePath);
                attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentFilePath);
                attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentFilePath);
                if (!String.IsNullOrEmpty(attachmentFileName))
                {
                    attachment.Name = attachmentFileName;
                }
                message.Attachments.Add(attachment);
            }
            if (attachedDownloadId > 0)
            {

            }
            //send email
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.EnableSsl;
                smtpClient.Credentials = emailAccount.UseDefaultCredentials ?
                    CredentialCache.DefaultNetworkCredentials :
                    new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }
    }
}
