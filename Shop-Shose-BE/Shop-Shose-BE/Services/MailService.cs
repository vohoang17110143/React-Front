using ExamToeicOnline_BackEnd_Clients.Common;
using ExamToeicOnline_BackEnd_Clients.Models;
using Shop_Shose_BE.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Service
{
    public class MailService:IMailService
    {
        public string GetMailBody(LoginInfo oLoginInfo)
        {
            string url = Global.DomainName + "api/LoginInfos/ConfirmMail?username=" + oLoginInfo.Username;

            return string.Format(@"<div style='text-align:center;'>
                                    <h1>Welcome to our Web Site</h1>
                                    <h3>Click below button for verify your Email Id</h3>
                                    <form method='post' action='{0}' style='display: inline;'>
                                      <button type = 'submit' style=' display: block;
                                                                    text-align: center;
                                                                    font-weight: bold;
                                                                    background-color: #008CBA;
                                                                    font-size: 16px;
                                                                    border-radius: 10px;
                                                                    color:#ffffff;
                                                                    cursor:pointer;
                                                                    width:100%;
                                                                    padding:10px;'>
                                        Confirm Mail
                                      </button>
                                    </form>
                                </div>", url, oLoginInfo.Username);
        }

        public async Task<string> SendMail(MailClass oMailClass)
        {
            try
            {
                using(MailMessage mail=new MailMessage())
                {
                    mail.From = new MailAddress(oMailClass.FromMailId);
                    oMailClass.ToMailIds.ForEach(x =>
                    {
                        mail.To.Add(x);
                    });
                    mail.Subject = oMailClass.Subject;
                    mail.Body = oMailClass.Body;
                    mail.IsBodyHtml = oMailClass.IsBodyHtml;
                    oMailClass.Attachments.ForEach(x =>
                    {
                        mail.Attachments.Add(new Attachment(x));
                    });
                    using(SmtpClient smtp=new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(oMailClass.FromMailId, oMailClass.FromMailPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return Message.MailSent;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
    }
}
