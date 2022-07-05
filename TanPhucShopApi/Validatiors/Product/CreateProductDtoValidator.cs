﻿using FluentValidation;
using TanPhucShopApi.Models;
using TanPhucShopApi.Models.DTO.Product;
using TanPhucShopApi.Models.DTO.UserDto;

namespace TanPhucShopApi.Validatiors.Product

{
    public class CreateRoleDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateRoleDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product'Name is required")
                .MaximumLength(30).WithMessage("Product'Name  length from 4 - 30 letters").MinimumLength(4).WithMessage("Product'Name  length from 4 - 30 letters");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Product'Price is required").GreaterThanOrEqualTo(1).WithMessage("Product'Price must be greater than 1");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");

            RuleFor(x => x.Photo).NotEmpty().WithMessage("Photo is required");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Please Select Category!");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required!").MaximumLength(1000).WithMessage("Description length from 4 - 30 letters");

        }

    }

}
