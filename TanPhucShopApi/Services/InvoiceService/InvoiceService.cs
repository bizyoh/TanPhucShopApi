using AutoMapper;
using TanPhucShopApi.Data;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;
using TanPhucShopApi.Models.DTO.InvoiceDto;

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

        public bool Create(CreateInvoiceDto createInvoiceDto)
        {
            var invoice = mapper.Map<Invoice>(createInvoiceDto);
            var invoiceDetails = mapper.Map<List<InvoiceDetail>>(createInvoiceDto.CreateInvoiceDetailDtos);
            invoice.InvoiceDetails = invoiceDetails;
            if (invoice.InvoiceDetails.Count > 0)
            {
                db.Invoices.Add(invoice);
            }
            return db.SaveChanges()>0;
        }

        public List<Invoice> FindInvoiceByUserId(int id)
        {
            return db.Invoices.Where(invoice => invoice.UserId == id).ToList();
        }
    }    
}
