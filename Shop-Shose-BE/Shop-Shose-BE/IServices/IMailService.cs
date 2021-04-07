using ExamToeicOnline_BackEnd_Clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.IServices
{
    public interface IMailService
    {
        Task<string> SendMail(MailClass oMailClass);
        string GetMailBody(LoginInfo oLoginInfo);

    }
}
