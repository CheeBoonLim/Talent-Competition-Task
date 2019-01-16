using Talent.Data.Entities;
using Talent.Service.Models;
using System;

namespace Talent.Service.SendMail
{
    public interface IEmailService
    {
        void SendMail(string websiteUrl, string emailFrom, string emailTo, Guid emailCode,
                        string emailVerificationUrl, string senderName = null);

        void SendForgotPassword(string resetPasswordUrl, string emailFrom,
                                        string emailTo, string senderName);

        void SendContact(string supportEmail, ContactModel model);

        void SendUploadDocumentNotification(string uploadUrl, string emailFrom, string emailTo, string fileName,
                                            string documentType, string senderName, string phone);

        void SendChangedDocumentStatusNotification(string uploadUrl, string emailFrom, string emailTo, string oldStatus,
                string newStatus, string fileName, string documentType, string senderName, string changedBy, string fullName);

        void SendChangedOrderStatusNotification(string emailFrom, string emailTo, string oldStatus, string newStatus,
                                                          string type, string crypto, string fiat, string rateAudited,
                                                          string senderName, string changedBy, string fullName);

        void SendLoginNotification(string emailFrom, string emailTo,
                        string ipAddress, string senderName, string fullName);

        void SendNewRegisterNotification(string emailTo, SignUpPersonal user);

        void SendVerifiedEmailNotification(string loginURL, string emailFrom,
                                            string emailTo, string senderName, Login user);
    }
}
