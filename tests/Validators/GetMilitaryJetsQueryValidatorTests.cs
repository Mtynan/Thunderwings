using Application.Features.MilitaryJets.Queries;
using Application.Features.MilitaryJets.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderwings.UnitTests.Validators
{
    public class GetMilitaryJetsQueryValidatorTests
    {
        private readonly GetMilitaryJetsQueryValidator _validator;

        public GetMilitaryJetsQueryValidatorTests()
        {
            _validator = new GetMilitaryJetsQueryValidator();
        }

        [Fact]
        public void Price_IsNull_ShouldNotHaveValidationError()
        {
            var model = new GetMilitaryJetsQuery(null, null, null, null, null, null,1,10);
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(m => m.Price);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void Price_IsGreaterThanZero_ShouldNotHaveValidationError(int validPrice)
        {
            var model = new GetMilitaryJetsQuery(null, null, null, null, null, validPrice, 1, 10);
            _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(m => m.Price);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Price_IsLessThanOrEqualToZero_ShouldHaveValidationError(int invalidPrice)
        {
            var model = new GetMilitaryJetsQuery(null, null, null, null, null, invalidPrice, 1, 10);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(m => m.Price)
                  .WithErrorMessage("Price must be greater than 0");
        }
    }
}
