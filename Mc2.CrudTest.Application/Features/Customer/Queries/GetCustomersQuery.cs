using AutoMapper;
using Mc2.CrudTest.Application.Common.Interfaces;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.Presentation.Shared.Dtos;

namespace Mc2.CrudTest.Application.Features.Customer.Queries
{
    public class GetCustomersQuery : IRequest<List<CustomerDto>>;
    

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetAll()
                .OrderByDescending(x => x.CreatedDate)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
