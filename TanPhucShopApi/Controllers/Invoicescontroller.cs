using Microsoft.AspNetCore.Mvc;
using TanPhucShopApi.Models;
using TanPhucShopApi.Services.InvoiceService;

namespace TanPhucShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Invoicescontroller : ControllerBase
    {
        private IInvoiceService invoiceService;
        public Invoicescontroller(IInvoiceService _invoiceService)
        {
            invoiceService = _invoiceService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (await invoiceService.Create(invoice))
            {
                return Ok();
            }
          return BadRequest();
        }
    }
}
