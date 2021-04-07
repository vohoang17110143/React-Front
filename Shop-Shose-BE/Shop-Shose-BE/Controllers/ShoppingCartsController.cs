using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_Shose_BE.EF;

namespace Shop_Shose_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {

        private readonly ShopShoseContext _context;
        public ShoppingCartsController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }


    }
}
