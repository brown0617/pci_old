using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;

namespace Backend.API
{
	public class AutoMapperConfig : Profile
	{
		protected override void Configure()
		{
			// Customer
			CreateMap<Customer, CustomerData>()
				.ForMember(dto => dto.Type, opts => opts.MapFrom(ent => ent.GetType().Name.Split('_')[0]));
			CreateMap<CustomerData, Customer>();
			
			// Person
			CreateMap<Person, PersonData>();
			CreateMap<PersonData, Person>();
		}
	}
}