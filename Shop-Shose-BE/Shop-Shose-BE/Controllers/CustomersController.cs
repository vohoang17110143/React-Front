using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.EF;
using Shop_Shose_BE.Models.DTO;

namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public CustomersController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customer = await (from cus in this._context.Customers
                                      select new CustomerDTO()
                                      {
                                          CustomerId = cus.CustomerId,
                                          Name = cus.Name,
                                          BirthDay = cus.BirthDay,
                                          PhoneNumber = cus.PhoneNumber,
                                          Email = cus.Email,
                                          Image = cus.Image
                                      }).ToArrayAsync();
                if (customer.Count() > 0)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            try
            {
                var customer = await this._context.Customers.FirstOrDefaultAsync(cus=>cus.CustomerId==customerId);
                if (customer!=null)
                {
                    CustomerDTO customerDTO = new CustomerDTO()
                    {
                        CustomerId = customer.CustomerId,
                        Name =customer.Name,
                        BirthDay=customer.BirthDay,
                        PhoneNumber=customer.PhoneNumber,
                        Email=customer.Email,
                        Image=customer.Image,
                        
                    };
                    return Ok(customerDTO);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetCustomerByName(string name)
        {
            try
            {
                var customer = await (from cus in this._context.Customers
                                      where cus.Name.Contains(name)
                                      select new CustomerDTO() {
                                          CustomerId = cus.CustomerId,
                                          Name = cus.Name,
                                          BirthDay = cus.BirthDay,
                                          PhoneNumber = cus.PhoneNumber,
                                          Email = cus.Email,
                                          Image = cus.Image
                                      }).ToArrayAsync();
                if (customer.Count()>0)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
           
        }

        
    }
}
