using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;

namespace Mc2.CrudTest.Application.Features.Customer.Queries
{
    public class CheckUniqueEmailQuery : IRequest<bool>
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
    }

    public class CheckEmailQueryHandler : IRequestHandler<CheckUniqueEmailQuery, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public CheckEmailQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(CheckUniqueEmailQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.AnyAsync(c =>
                (request.Id == null || c.Id != request.Id) &&
                    c.Email.ToLower() == request.Email.Trim().ToLower()
                
                , cancellationToken);
        }
    }

}
