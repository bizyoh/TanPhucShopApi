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
    }    
}
