using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCourse.Demo1.Products
{
    public class CreateUpdateProdcutValidator:AbstractValidator<CreateUpdateProductDto>
    {

        public CreateUpdateProdcutValidator()
        {

            RuleFor(x => x.NameAr)
                .NotEmpty()
                .MaximumLength(Demo1Consts.GeneralTextMaxLength)
                .WithErrorCode(Demo1DomainErrorCodes.INVALID_PRODUCT_DATA_NAME_AR)
                .WithMessage("Products:InvalidProductNameAr");
            RuleFor(x => x.NameEn)
                .NotEmpty()
                .MaximumLength(Demo1Consts.GeneralTextMaxLength)
                .WithErrorCode(Demo1DomainErrorCodes.INVALID_PRODUCT_DATA_NAME_EN)
                .WithMessage("Products:InvalidProductNameEn");
            RuleFor(x => x.DescriptionAr)
                .NotEmpty()
                .MaximumLength(Demo1Consts.GeneralDescMaxLength)
                .WithErrorCode(Demo1DomainErrorCodes.INVALID_PRODUCT_DATA_DESC_AR)
                .WithMessage("Products:InvalidProductDescAr");
            RuleFor(x => x.DescriptionEn)
                .NotEmpty()
                .MaximumLength(Demo1Consts.GeneralDescMaxLength)
                .WithErrorCode(Demo1DomainErrorCodes.INVALID_PRODUCT_DATA_DESC_EN)
                .WithMessage("Products:InvalidProductDescEn");
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithErrorCode(Demo1DomainErrorCodes.INVALID_PRODUCT_CATEGORY)
                .WithMessage("Products:InvalidProductCategory");

        }
    }
}
