using Application.Features.Basket.Queries;
using Application.Features.Basket.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderwings.UnitTests.Validators
{
    public class GetBasketQueryValidatorTests
    {
        private readonly GetBasketQueryValidator _validator;

        public GetBasketQueryValidatorTests()
        {
            _validator = new GetBasketQueryValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void GetBasketQueryValidator_WithValidUserId_ShouldNotHaveValidationError(int userId)
        {
            //arrange
            var query = new GetBasketQuery(userId);

            //act and assert
            _validator.TestValidate(query).ShouldNotHaveValidationErrorFor(query => query.UserId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void GetBasketQueryValidator_WithInvalidUserId_ShouldHaveValidationError(int userId)
        {
            //arrange
            var query = new GetBasketQuery(userId);

            //act
            var result = _validator.TestValidate(query);

            //assert
            result.ShouldHaveValidationErrorFor(query => query.UserId)
                  .WithErrorMessage("UserId must be greater than 0");
        }

    }
}
