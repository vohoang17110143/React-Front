using ExamToeicOnline_BackEnd_Clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamToeicOnline_BackEnd_Clients.IServices
{
    public interface ILoginInfoService
    {
        Task<LoginInfo> SignUp(LoginInfo oLoginInfo);
        Task<string> ConfirmMail(string username);
    } 
}
 