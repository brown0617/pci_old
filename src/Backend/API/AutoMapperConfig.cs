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

			// Property
			CreateMap<Property, PropertyData>()
				.ForMember(dto => dto.CustomerName, opts => opts.MapFrom(ent => ent.Customer.Name))
				.ForMember(dto => dto.PrimaryContactName, opts => opts.MapFrom(ent => ent.PrimaryContact.FullName));
			CreateMap<PropertyData, Property>();

			// Quote
			CreateMap<Quote, QuoteData>()
				.ForMember(dto => dto.PropertyName, opts => opts.MapFrom(ent => ent.Property.Name))
				.ForMember(dto => dto.CustomerName, opts => opts.MapFrom(ent => ent.Property.Customer.Name));
			CreateMap<QuoteData, Quote>()
				.ForMember(ent => ent.BillingDayDesc, opts => opts.Ignore())
				.ForMember(ent => ent.SeasonDesc, opts => opts.Ignore())
				.ForMember(ent => ent.StatusDesc, opts => opts.Ignore())
				.ForMember(ent => ent.BillingStartDesc, opts => opts.Ignore())
				.ForMember(ent => ent.TypeDesc, opts => opts.Ignore());
		}
	}
}