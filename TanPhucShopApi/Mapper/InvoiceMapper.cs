using AutoMapper;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.InvoiceDto;

namespace TanPhucShopApi.Mapper
{
    public class InvoiceMapper : Profile
    {
        public InvoiceMapper()
        {
            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<CreateInvoiceDetailDto, InvoiceDetail>();
            CreateMap<Invoice, GetAllInvoiceDto>();
        }
    }
}
