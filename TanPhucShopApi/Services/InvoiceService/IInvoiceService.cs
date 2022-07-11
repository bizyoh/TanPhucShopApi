using TanPhucShopApi.Models;

namespace TanPhucShopApi.Services.InvoiceService
{
    public interface IInvoiceService
    {
        public Task<bool> Create(Invoice invoice);
    }
}
