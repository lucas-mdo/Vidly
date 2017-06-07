using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<Customer, CustomerDto>();

            Mapper.CreateMap<MovieDto, Movie>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<Movie, MovieDto>();
        }
    }
}