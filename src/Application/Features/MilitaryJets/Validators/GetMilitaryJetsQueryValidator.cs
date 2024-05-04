using Application.Features.MilitaryJets.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MilitaryJets.Validators
{
    public sealed class GetMilitaryJetsQueryValidator : AbstractValidator<GetMilitaryJetsQuery>
    {
        public GetMilitaryJetsQueryValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.TopSpeed)
               .GreaterThan(0)
               .WithMessage("Top speed must be greater than 0")
               .When(x => x.TopSpeed.HasValue);
        }
    }
}
