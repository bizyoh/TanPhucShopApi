using FluentValidation;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Validatiors.Invoice

{
    public class InvoiceValidator : AbstractValidator<Models.Invoice>
    {
        public InvoiceValidator()
        {
            

        }

    }

}
