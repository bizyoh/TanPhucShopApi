using FluentValidation;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Category;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Validatiors.Category

{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category'name is required")
                .MaximumLength(30).WithMessage("Category'name length from 4 - 30 letters").MinimumLength(4).WithMessage("Category'name length from 4 - 30 letters");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }

    }

}
