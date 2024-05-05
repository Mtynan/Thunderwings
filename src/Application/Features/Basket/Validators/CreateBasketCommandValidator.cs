using Application.Features.Basket.Commands;
using Application.Features.MilitaryJets.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Basket.Validators
{
    public sealed class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .GreaterThan(0)
                 .WithMessage("UserId must be greater than 0");

        }
    }
}

