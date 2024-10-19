using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using IbanNet;
using IbanNet.Validation.Results;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mc2.CrudTest.Application.UnitTests.Features.Customer.Commands.CreateCustomer
{

    [TestFixture]
    public class CreateCustomerCommandValidatorTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<IIbanValidator> _ibanValidatorMock;
        private CreateCustomerCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _ibanValidatorMock = new Mock<IIbanValidator>();
            _validator = new CreateCustomerCommandValidator(_customerRepository.Object, _ibanValidatorMock.Object);
        }

        [Test]
        public async Task Validate_ValidCommand_ShouldNotHaveAnyErrors()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Ahmad",
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819" 
            };

            _ibanValidatorMock.Setup(iban => iban.Validate(It.IsAny<string>())).Returns(new ValidationResult() { });


            _customerRepository.Setup(a => a.AnyAsync(It.IsAny<Expression<Func<Domain.Entities.Customer, bool>>>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(false);

           // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public async Task Validate_DuplicateEmail_ShouldHaveError()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Ahmad",
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819"
            };

            _ibanValidatorMock.Setup(iban => iban.Validate(It.IsAny<string>())).Returns(new ValidationResult() { });

            _customerRepository.Setup(a => a.AnyAsync(It.IsAny<Expression<Func<Domain.Entities.Customer, bool>>>(), It.IsAny<CancellationToken>()))
      .ReturnsAsync(true);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public async Task Validate_InvalidBankAccountNumber_ShouldHaveError()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Ahmad",
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "" 
            };

            _ibanValidatorMock.Setup(iban => iban.Validate(It.IsAny<string>())).Returns(new ValidationResult() { Error = new ErrorResult("") });

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BankAccountNumber);
        }

        [Test]
        public async Task Validate_EmptyFirstName_ShouldHaveError()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "", 
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "+12124567890",
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819"
            };

            _ibanValidatorMock.Setup(iban => iban.Validate(It.IsAny<string>())).Returns(new ValidationResult() { });


            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public async Task Validate_InvalidPhoneNumber_ShouldHaveError()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                FirstName = "Ahmad",
                LastName = "Aghamohammadi",
                Email = "ahmad.aghamohammadi@gmail.com",
                PhoneNumber = "invalid-phone", 
                DateOfBirth = new DateTime(1990, 1, 1),
                BankAccountNumber = "GB29NWBK60161331926819"
            };

            _ibanValidatorMock.Setup(iban => iban.Validate(It.IsAny<string>())).Returns(new ValidationResult() { });


            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }
    }
}
