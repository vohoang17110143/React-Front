using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop_Shose_BE.Common;
using Shop_Shose_BE.EF;
using Shop_Shose_BE.Models;
using Shop_Shose_BE.Models.DTO;
using BC = BCrypt.Net.BCrypt;

namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        private IConfiguration _config;
        public AccountsController(ShopShoseContext shopShoseContext, IConfiguration configuration)
        {
            this._context = shopShoseContext;
            this._config = configuration;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var accounts = await (from a in this._context.Accounts
                                      select new AccountDTO()
                                      {
                                          AccountId=a.AccountId,
                                          Username = a.Username,
                                          Password = a.Password,
                                          DateCreate = a.DateCreate,
                                          Status = a.Status.ToString()
                                      }).ToArrayAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetOneAccount(string username)
        {
            try
            {
                var account = await (from c in this._context.Accounts
                                     where c.Username.Contains(username)
                                     select new AccountDTO()
                                     {
                                         AccountId = c.AccountId,
                                         Username = c.Username,
                                         Password = c.Password,
                                         DateCreate = c.DateCreate,
                                         Status = c.Status.ToString()
                                     }).ToArrayAsync();
                if (account.Count()>0)
                {
                    return Ok(account);
                }
                return NotFound(new { message = Message.ErrorFound + " " + username });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message=ex.Message});
            }
           
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            try
            {
                var account = await (from c in this._context.Accounts
                                     where c.AccountId==accountId
                                     select new AccountDTO()
                                     {
                                         AccountId = c.AccountId,
                                         Username = c.Username,
                                         Password = c.Password,
                                         DateCreate = c.DateCreate,
                                         Status = c.Status.ToString()
                                     }).ToArrayAsync();
                if (account.Count() > 0)
                {
                    return Ok(account);
                }
                return NotFound(new { message = Message.ErrorFound + " " + accountId });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] GuestInfoDTO guestInfoDTO)
        {

            try
            {
                if (!await CheckExistAccount(guestInfoDTO.Username) && !await CheckExistEmail(guestInfoDTO.Email))
                {
                    //create account
                    Account account = new Account()
                    {
                        Username = guestInfoDTO.Username,
                        Password = BC.HashPassword(guestInfoDTO.Password),
                        DateCreate = DateTime.Now,
                        Status = Status.Inactive.ToString()

                    };
                    this._context.Accounts.Add(account);
                    
                    //create customer
                    Customer customer = new Customer()
                    {
                        Name = guestInfoDTO.Name,
                        BirthDay = guestInfoDTO.BirthDay,
                        PhoneNumber = guestInfoDTO.PhoneNumber,
                        Image = guestInfoDTO.Image,
                        Email = guestInfoDTO.Email
                       // AccountId = 1
                    };
                    this._context.Customers.Add(customer);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.UserCreateVerifyMail });
                }
                else
                {
                    return Ok(new { message = Message.Error });
                }
               
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        private async Task<bool> CheckExistAccount(string username)
        {
            var account = await this._context.Accounts.Where(a => a.Username == username).FirstOrDefaultAsync();
            if (account!=null)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> CheckExistEmail(string email)
        {
            var customer = await this._context.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (customer != null)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            try
            {
                var account = await this._context.Accounts.FirstOrDefaultAsync(acc=>acc.AccountId==accountId);
                if (account == null)
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
                account.Status = Status.Inactive.ToString();
                this._context.Accounts.Update(account);
                await this._context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            catch (Exception ex)
            {
                return Ok(new { message = ex.Message });
            }
            
        }

        //login
        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            AccountDTO accountLogin = new AccountDTO();
            accountLogin.Username = username;
            accountLogin.Password = password;
            IActionResult response = Unauthorized();
            var account = AuthenticateAccount(accountLogin);
            if (account.Username != null)
            {
                accountLogin.CustomerId = account.CustomerId;
                var tokenStr = GenerateJSONWebToken(accountLogin);
                response = Ok(new { token = tokenStr });
                return Ok(response);
            }

            return Unauthorized("Login fail");
        }

        private AccountDTO AuthenticateAccount(AccountDTO accountLogin)
        {
            AccountDTO AccountDTO = new AccountDTO();
            var account = this._context.Accounts.FirstOrDefault(x => x.Username.Equals(accountLogin.Username.Trim()));
            if (account != null && account.Status==Status.Active.ToString())
            {
                if (BC.Verify(accountLogin.Password, account.Password))
                {
                    AccountDTO.CustomerId = account.CustomerId;
                    AccountDTO.Username = account.Username;
                    AccountDTO.Password = account.Password;
                }
            }
            return AccountDTO;
        }
        private string GenerateJSONWebToken(AccountDTO account)
        {
            var secuirityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secuirityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim(JwtRegisteredClaimNames.Sub, account.CustomerId.ToString()),

            };
            var tokent = new JwtSecurityToken(
                issuer: _config["Jwt:Key"],
                audience: _config["Jwt:Issuer"],
                claim,
                expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials
            );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(tokent);
            return encodetoken;
        }
    }
}
