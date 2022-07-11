using AutoMapper;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;

namespace TanPhucShopApi.Services.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private AppDBContext db;
        private IMapper mapper;
        public InvoiceService(AppDBContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        public async Task<bool> Create(Invoice invoice)
        {
            db.Invoices.Add(invoice);
           return db.SaveChanges()>0;
        }
    }    
}
