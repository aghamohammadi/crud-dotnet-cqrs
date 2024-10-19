using Ardalis.GuardClauses;
using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;

namespace Mc2.CrudTest.Application.Features.Customer.Commands.UpdateCustomer
{
    public record UpdateCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = _unitOfWork.Customers;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);


            Guard.Against.NotFound(request.Id, entity);

            entity.FirstName = Guard.Against.NullOrEmpty(request.FirstName?.Trim());
            entity.LastName = Guard.Against.NullOrEmpty(request.LastName?.Trim());
            entity.Email = Guard.Against.NullOrEmpty(request.Email?.Trim());
            entity.PhoneNumber = Guard.Against.NullOrEmpty(request.PhoneNumber?.Trim());
            entity.DateOfBirth = Guard.Against.Default(request.DateOfBirth, nameof(request.DateOfBirth));
            entity.BankAccountNumber = Guard.Against.NullOrEmpty(request.BankAccountNumber?.Trim());

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
