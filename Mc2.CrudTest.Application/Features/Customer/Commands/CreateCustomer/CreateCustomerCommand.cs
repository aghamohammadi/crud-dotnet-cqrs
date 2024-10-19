using MediatR;
using Mc2.CrudTest.Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Customer
            {
                Id = request.Id ?? Guid.NewGuid(),
                FirstName = Guard.Against.NullOrEmpty(request.FirstName?.Trim()),
                LastName = Guard.Against.NullOrEmpty(request.LastName?.Trim()),
                Email = Guard.Against.NullOrEmpty(request.Email?.Trim()),
                PhoneNumber = Guard.Against.NullOrEmpty(request.PhoneNumber?.Trim()),
                DateOfBirth = Guard.Against.Default(request.DateOfBirth, nameof(request.DateOfBirth)),
                BankAccountNumber = Guard.Against.NullOrEmpty(request.BankAccountNumber?.Trim()),
                CreatedDate = DateTime.Now
            };

            _unitOfWork.Customers.Add(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
