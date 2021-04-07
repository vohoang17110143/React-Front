using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using ExamToeicOnline_BackEnd_Clients.IServices;
using ExamToeicOnline_BackEnd_Clients.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Shose_BE.IServices;

namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginInfosController : ControllerBase
    {
        private ILoginInfoService _loginInfoService;
        private IMailService _mailService;

        public LoginInfosController(ILoginInfoService loginInfoService,IMailService mailService)
        {
            _loginInfoService = loginInfoService;
            _mailService = mailService;
        }
        // POST api/<LoginInfosController>
        [AllowAnonymous]
        [HttpPost("SingUp")]
        public async Task<IActionResult> SignUp([FromBody] LoginInfo oLoginInfo)
        {
            string sMessage = "";
            var user = await _loginInfoService.SignUp(oLoginInfo);
            if (user==null) return BadRequest(new { message = Message.ErrorFound });
            if (user.Message==Message.VerifyMil)
            {
                MailClass oMailClass = this.GetMailObject(user);
                await _mailService.SendMail(oMailClass);
                return BadRequest(new { message = Message.VerifyMil });
            }
            #region Send Confirmation Mail
            if (user.Message==Message.Success)
            {
                MailClass oMailClass = this.GetMailObject(user);
                sMessage = await _mailService.SendMail(oMailClass);
            }
            if (sMessage!=Message.MailSent) return BadRequest(new { message = sMessage });
            else return Ok(new { message = Message.UserCreateVerifyMail });
            #endregion
        }
        [AllowAnonymous]
        [HttpPost("ConfirmMail")]
        public async Task<IActionResult> ConfirmMail(string username)
        {
            string sMessage = await _loginInfoService.ConfirmMail(username);
            return Ok(new { message = sMessage });
        }

        public MailClass GetMailObject(LoginInfo user)
        {
            MailClass oMailClass = new MailClass();
            oMailClass.Subject = "Mail Confirmation";
            oMailClass.Body = _mailService.GetMailBody(user);
            oMailClass.ToMailIds = new List<string>()
            {
                user.EmailId
            };
            return oMailClass;
        }
    }
}
