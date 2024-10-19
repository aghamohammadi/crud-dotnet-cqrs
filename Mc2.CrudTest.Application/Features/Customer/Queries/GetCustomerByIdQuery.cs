using AutoMapper;
using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Presentation.Shared.Dtos;
using MediatR;

namespace Mc2.CrudTest.Application.Features.Customer.Queries
{
    public record GetCustomerByIdQuery() : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }



    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);


            return _mapper.Map<CustomerDto>(result);

        }

    }
}
