using FluentValidation;
using IbanNet.FluentValidation;
using IbanNet;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Application.Features.Customer.Dtos;
using PhoneNumbers;

namespace Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly PhoneNumberUtil _phoneNumberUtil;
        private readonly IIbanValidator _ibanValidator;

        public CreateCustomerCommandValidator(ICustomerRepository customerRepository, IIbanValidator ibanValidator)
        {
            _customerRepository = customerRepository;
            _ibanValidator = ibanValidator;
            _phoneNumberUtil = PhoneNumberUtil.GetInstance();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");


            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists.");

            RuleFor(x => x.BankAccountNumber)
                .NotEmpty().WithMessage("Bank account number is required.")
                .Iban(_ibanValidator).WithMessage("Invalid Bank account format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Must(IsValidPhoneNumber).WithMessage("Invalid phone number.");

            RuleFor(x => new CustomerValidationDto() { FirstName = x.FirstName, LastName = x.LastName, DateOfBirth = x.DateOfBirth })
                .MustAsync(BeUniqueCustomer).WithMessage("Customer with the same name and date of birth already exists.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _customerRepository.AnyAsync(c => c.Email.ToLower() == email.Trim().ToLower(), cancellationToken);
        }

        private async Task<bool> BeUniqueCustomer(CustomerValidationDto customer, CancellationToken cancellationToken)
        {
            return !await _customerRepository.AnyAsync(c =>
                c.FirstName.ToLower() == customer.FirstName.Trim().ToLower() &&
                c.LastName.ToLower() == customer.LastName.Trim().ToLower() &&
                c.DateOfBirth == customer.DateOfBirth, cancellationToken);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            try
            {
                var parsedNumber = _phoneNumberUtil.Parse(phoneNumber, "US");  
                return _phoneNumberUtil.IsValidNumber(parsedNumber);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
