using AutoMapper;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Presentation.Shared.Dtos;

namespace Mc2.CrudTest.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
