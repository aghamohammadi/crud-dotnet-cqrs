using MediatR;
using Mc2.CrudTest.Application.Common.Interfaces;
using Ardalis.GuardClauses;

namespace Mc2.CrudTest.Application.Features.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
    }


    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }



        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);


            Guard.Against.NotFound(request.Id, entity);

            _customerRepository.Delete(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
