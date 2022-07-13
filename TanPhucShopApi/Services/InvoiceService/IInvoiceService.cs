using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.InvoiceDto;

namespace TanPhucShopApi.Services.InvoiceService
{
    public interface IInvoiceService
    {
        public bool Create(CreateInvoiceDto createInvoiceDto);
        public List<Invoice> FindInvoiceByUserId(int id);
        public List<InvoiceDetailUserViewModel> GetAllInvoiceDetailUserViewModel(int id);
        public List<InvoiceUserViewModel> GetAllInvoiceUserViewModel();
        public List<InvoiceUserViewModel> GetAllInvoiceUserViewModelByUserId(int userId);
    }
}
