using Dapper;
using ExamToeicOnline_BackEnd_Clients.Common;
using ExamToeicOnline_BackEnd_Clients.IServices;
using ExamToeicOnline_BackEnd_Clients.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Service
{
    public class LoginInfoService:ILoginInfoService
    {
        LoginInfo _oLoginInfo = new LoginInfo();
        private readonly ShopShoseContext _context;
        public LoginInfoService(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }
        public async Task<string> ConfirmMail(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) return "Invalid Username";
                LoginInfo oLoginInfo = new LoginInfo()
                {
                    Username = username
                };
                LoginInfo loginInfo = await this.CheckRecordExistence(oLoginInfo);
                if (loginInfo == null)
                {
                    return Message.InvalidUser;
                }
                else
                {
                    using(IDbConnection con=new SqlConnection(Global.ConnectionString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        {
                            var oLoginInfos = await con.QueryAsync<LoginInfo>("SP_LoginInfo",
                            this.SetPagramerters(loginInfo, (int)OperationType.UpdateConfirmMail),
                            commandType: CommandType.StoredProcedure);
                            if (oLoginInfos != null && oLoginInfos.Count() > 0)
                            {
                                _oLoginInfo = oLoginInfos.FirstOrDefault();
                            }
                            return "Mail Confirmed";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<LoginInfo> SignUp(LoginInfo oLoginInfo)
        {
            _oLoginInfo = new LoginInfo();
            try
            {
                LoginInfo loginInfo = await this.CheckRecordExistence(oLoginInfo);

                if (loginInfo == null)

                {

                    using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                    {
                        if (con.State == ConnectionState.Closed) con.Open();
                        var oLoginInfos = await con.QueryAsync<LoginInfo>("SP_LoginInfo",
                            this.SetPagramerters(loginInfo, (int)OperationType.Singup),
                            commandType: CommandType.StoredProcedure);
                        if (oLoginInfos != null && oLoginInfos.Count() > 0)
                        {
                            _oLoginInfo = oLoginInfos.FirstOrDefault();
                        }
                        _oLoginInfo.Message = Message.Success;
                    }
                }
                else
                {
                    _oLoginInfo = loginInfo;
                }
            }
            catch (Exception ex)
            {

                _oLoginInfo.Message=ex.Message;
            }
            return _oLoginInfo;
        }
        private async Task<LoginInfo> CheckRecordExistence(LoginInfo oLoginInfo)
        {
            LoginInfo loginInfo = new LoginInfo();
            if (!string.IsNullOrEmpty(oLoginInfo.Username))
            {
                loginInfo = await this.GetLoginUser(oLoginInfo.Username);
                if (loginInfo!=null)
                {
                    if (!loginInfo.IsMailConfirmed)
                    {
                        loginInfo.Message = Message.VerifyMil;
                    }
                    else if (loginInfo.IsMailConfirmed)
                    {
                        loginInfo.Message = Message.UserAlreadyCreate;
                    }
                }
            }
            return loginInfo;
        }
        private async Task<LoginInfo> GetLoginUser(string username)
        {
            _oLoginInfo = new LoginInfo();
            //var oLoginInfo = await this._context.LoginInfos.Where(c => c.Username == username).FirstOrDefaultAsync();
            //if (oLoginInfo != null)
            //{
            //    _oLoginInfo = oLoginInfo;
            //}
            //else return null;


            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string sSQL = "SELECT * FROM LoginInfos WHERE 1=1";
                if (!string.IsNullOrEmpty(username)) sSQL += "AND Username='" + username + "'";
                var oLoginInfo = (await con.QueryAsync<LoginInfo>(sSQL)).ToList();
                if (_oLoginInfo != null && oLoginInfo.Count > 0) _oLoginInfo = oLoginInfo.SingleOrDefault();
                else return null;
            }
            return _oLoginInfo;

        }

        private DynamicParameters SetPagramerters(LoginInfo oLoginInfo, int nOperationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserInfoId", oLoginInfo.UserInfoId);
            parameters.Add("@EmailId", oLoginInfo.EmailId);
            parameters.Add("@Username", oLoginInfo.Username);
            parameters.Add("@Password", oLoginInfo.Password);
            parameters.Add("@IsMailConfirmed", oLoginInfo.IsMailConfirmed);
            parameters.Add("@OperationType", nOperationType);
            return parameters;
        }
    }
}
