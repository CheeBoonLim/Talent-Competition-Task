using Talent.Data.Entities;
using Talent.Service.Models;
using System;
using System.Net.Mail;
using System.Text;

namespace Talent.Service.SendMail
{
    public class EmailService : IEmailService
    {
        // When user registed, send verification email to user
        public void SendMail(string websiteUrl, string emailFrom, string emailTo, Guid emailCode,
                                string emailVerificationUrl, string senderName = null)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Confirm Your Registration</H2>");
            //sb.AppendFormat("<BR/>");
            sb.AppendFormat("<H5><a href='{0}'>Welcome to Skin!</a></H5>", websiteUrl);
            sb.AppendFormat("<p><b>Click the link below to complete verification:</b></p>");
            sb.AppendFormat("<p><b><a href='{0}?email={1}&emailCode={2}'>Verify Email</a></b></p>",
                                            emailVerificationUrl, emailTo, emailCode);
            sb.AppendFormat("<p><b>If this activity is not your own operation, please contact us immediately.</b></p>");
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            //  var sender = from;
            MailMessage mailMessage = new MailMessage(emailFrom, emailTo);
            mailMessage.From = new MailAddress(emailFrom, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Email Verification";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // When user registed, send verification email to user
        public void SendForgotPassword(string resetPasswordUrl, string emailFrom, 
                                        string emailTo, string senderName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Reset Your Password</H2>");
            //sb.AppendFormat("<BR/>");
            sb.AppendFormat("<p>Let's get your password updated so you can get back to your Skin account.</p>");
            sb.AppendFormat("<p>Click the link below to reset your password:</p>");
            sb.AppendFormat("<p><b><a href='{0}'>Reset Password</a></b></p>", resetPasswordUrl);
            sb.AppendFormat("<p><b>If this activity is not your own operation, please contact us immediately.</b></p>");
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            //  var sender = from;
            MailMessage mailMessage = new MailMessage(emailFrom, emailTo);
            mailMessage.From = new MailAddress(emailFrom, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Reset Password";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send an user contact to support
        public void SendContact(string supportEmail, ContactModel model)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Contact Infomation</H2>");
            //sb.AppendFormat("<BR/>");
            sb.AppendFormat("<p><b>First Name:</b> {0}</p>", model.FirstName);
            sb.AppendFormat("<p><b>Last Name:</b> {0}</p>", model.LastName);
            sb.AppendFormat("<p><b>Email:</b> {0}</p>", model.Email);
            sb.AppendFormat("<p><b>Phone:</b> {0}</p>", model.MobilePhone);
            sb.AppendFormat("<p><b>Subject/s:</b> {0}</p>", model.Subject);
            sb.AppendFormat("<p><b>Message:</b> {0}</p>", model.Message);
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var sender = model.Email;
            var senderName = string.Format("{0} {1}", model.FirstName, model.LastName);
            MailMessage mailMessage = new MailMessage(sender, supportEmail);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = model.Subject;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a new upload document notification to support
        public void SendUploadDocumentNotification(string uploadUrl, string emailFrom, string supportEmail,
                                    string fileName, string documentType, string senderName, string phone)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Upload Document Notification</H2>");
            sb.AppendFormat("<p><b>Full Name:</b> {0}</p>", senderName);
            sb.AppendFormat("<p><b>Email:</b> {0}</p>", emailFrom);
            sb.AppendFormat("<p><b>Phone:</b> {0}</p>", phone);
            sb.AppendFormat("<p><b>Document type:</b> {0}</p>", documentType);
            sb.AppendFormat("<p><a href='{0}{1}' target='_blank'> {2} </a></p>", uploadUrl, fileName, fileName);
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var sender = emailFrom;
            MailMessage mailMessage = new MailMessage(sender, supportEmail);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Upload document";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a changed document status notification to both user and support
        public void SendChangedDocumentStatusNotification(string uploadUrl, string emailFrom, string emailTo, string oldStatus,
                        string newStatus, string fileName, string documentType, string senderName, string changedBy, string fullName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Changed Document Status Notification</H2>");
            sb.AppendFormat("<p><b>Full Name:</b> {0}</p>", fullName);
            sb.AppendFormat("<p><b>Document type:</b> {0}</p>", documentType);
            sb.AppendFormat("<p><a href='{0}{1}' target='_blank'> {2} </a></p>", uploadUrl, fileName, fileName);
            sb.AppendFormat("<p><b>Your document status has been changed:</b> from <b>{0}</b> to <b>{1}</b></p>", oldStatus, newStatus);
            sb.AppendFormat("<p><b>Change by:</b> {0}</p>", changedBy);

            var sender = emailFrom;
            MailMessage mailMessage = new MailMessage(sender, emailTo);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Change document status";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a changed Order status notification to both user and support
        public void SendChangedOrderStatusNotification(string emailFrom, string emailTo, string oldStatus, string newStatus,
                                                          string type, string crypto, string fiat, string rateAudited,
                                                          string senderName, string changedBy, string fullName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Changed Order Status Notification</H2>");
            sb.AppendFormat("<p><b>Full Name:</b> {0}</p>", fullName);
            sb.AppendFormat("<p><b>Type:</b> {0}</p>", type);
            sb.AppendFormat("<p><b>Crypto:</b> {0}</p>", crypto);
            sb.AppendFormat("<p><b>Fiat:</b> {0}</p>", fiat);
            sb.AppendFormat("<p><b>Rate Audited:</b> {0}</p>", rateAudited);
            sb.AppendFormat("<p><b>Your document status has been changed:</b> from <b>{0}</b> to <b>{1}</b></p>", oldStatus, newStatus);
            sb.AppendFormat("<p><b>Change by:</b> {0}</p>", changedBy);
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var sender = emailFrom;
            MailMessage mailMessage = new MailMessage(sender, emailTo);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Change Order status";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a login notification to user
        public void SendLoginNotification(string emailFrom, string emailTo,
                        string ipAddress, string senderName, string fullName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>Login Notification</H2>");
            sb.AppendFormat("<p>Dear {0},</p>", fullName);
            sb.AppendFormat("<p>Your account has been accessed from IP {0}." +
                " If you did not log into your account at this time, please report it to our staff.</p>", ipAddress);
            sb.AppendFormat("<p>Best regards,</p>");
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var sender = emailFrom;
            MailMessage mailMessage = new MailMessage(sender, emailTo);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "Login Notification";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a new register notification to support
        public void SendNewRegisterNotification(string emailTo, SignUpPersonal user)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>A New Register Notification</H2>");
            sb.AppendFormat("<p><b>First Name:</b> {0}</p>", user.FirstName);
            sb.AppendFormat("<p><b>Last Name:</b> {0}</p>", user.LastName);
            sb.AppendFormat("<p><b>Phone:</b> {0}{1}</p>", user.DialCode, user.MobileNumber);
            sb.AppendFormat("<p><b>Email:</b> {0}</p>", user.EmailAddress);
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var senderName = string.Format("{0} {1}", user.FirstName, user.LastName);
            var sender = user.EmailAddress;
            MailMessage mailMessage = new MailMessage(sender, emailTo);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "A New Register Notification";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        // Send a verified email notification to support and user
        public void SendVerifiedEmailNotification(string loginURL, string emailFrom,
                                                    string emailTo, string senderName, Login user)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<H2>A Verified Email Notification</H2>");
            sb.AppendFormat("<p><b>Email:</b> {0}</p>", user.Username);
            sb.AppendFormat("<p><b>First Name:</b> {0}</p>", user.Person.FirstName);
            sb.AppendFormat("<p><b>Last Name:</b> {0}</p>", user.Person.LastName);
            sb.AppendFormat("<p><b>Phone:</b> +{0}{1}</p>", user.Person.DialCode, user.Person.MobilePhone);
            sb.AppendFormat("<p>Your email has been verified successfully." +
                " Please click the below link to login.</p>");
            sb.AppendFormat("<p><a href='{0}' target='_blank'> Login to Skin </a></p>", loginURL);
            sb.AppendFormat("<br/>");
            sb.AppendFormat("<p><b>Skin Team</b></p>");
            sb.AppendFormat("<p><b>Automated message. Please do not reply</b></p>");

            var sender = emailFrom;
            MailMessage mailMessage = new MailMessage(sender, emailTo);
            mailMessage.From = new MailAddress(sender, senderName);

            mailMessage.IsBodyHtml = true;
            mailMessage.Body = sb.ToString();
            mailMessage.Subject = "A Verified Email Notification";
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }
    }
}
