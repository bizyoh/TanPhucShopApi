﻿using FluentValidation;
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
                .MaximumLength(20).WithMessage("Category'name length from 2- 20 letters").MinimumLength(2).WithMessage("Category'name length from 2 - 20 letters");
        }

    }

}
