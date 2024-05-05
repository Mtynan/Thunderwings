using Application.Features.Basket.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket.Validators
{
    public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
    {
        public RemoveItemFromBasketCommandValidator()
        {
            RuleFor(x => x.UserId)
               .GreaterThan(0)
               .WithMessage("UserId must be greater than 0");

            RuleFor(x => x.ItemId)
               .GreaterThan(0)
               .WithMessage("ItemId must be greater than 0");
        }
    }
}
