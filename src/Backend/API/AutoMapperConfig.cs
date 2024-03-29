﻿using System.Linq;
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

			// Material
			CreateMap<Material, MaterialData>();
			CreateMap<MaterialData, Material>();

			// Option
			CreateMap<Option, OptionData>();

			// Order
			CreateMap<Order, OrderData>();
			CreateMap<OrderData, Order>();

			// OrderNoItems
			CreateMap<Order, OrderDataNoItems>();

			// Order
			CreateMap<OrderItem, OrderItemData>();
			CreateMap<OrderItemData, OrderItem>();

			// Person
			CreateMap<Person, PersonData>();
			CreateMap<PersonData, Person>();

			// Property
			CreateMap<Property, PropertyData>()
				//.ForMember(dto => dto.CustomerName, opts => opts.MapFrom(ent => ent.Customer.Name))
				.ForMember(dto => dto.PrimaryContactPhone, opts => opts.MapFrom(ent => ent.PrimaryContact.TelephoneWork))
				.ForMember(dto => dto.PrimaryContactEMail, opts => opts.MapFrom(ent => ent.PrimaryContact.EMailAddressWork))
				.ForMember(dto => dto.PrimaryContactName, opts => opts.MapFrom(ent => ent.PrimaryContact.FullName));
			CreateMap<PropertyData, Property>();

			// QuoteItem
			CreateMap<QuoteItem, QuoteItemData>();
			CreateMap<QuoteItemData, QuoteItem>();

			// Quote
			CreateMap<Quote, QuoteData>()
				.ForMember(dto => dto.PropertyName, opts => opts.MapFrom(ent => ent.Property.Name))
				.ForMember(dto => dto.CustomerName, opts => opts.MapFrom(ent => ent.Customer.Name))
				.ForMember(dto => dto.Items, opts => opts.MapFrom(ent => ent.Items));
			CreateMap<QuoteData, Quote>()
				.ForMember(ent => ent.BillingDayDesc, opts => opts.Ignore())
				.ForMember(ent => ent.SeasonDesc, opts => opts.Ignore())
				.ForMember(ent => ent.StatusDesc, opts => opts.Ignore())
				.ForMember(ent => ent.BillingStartDesc, opts => opts.Ignore())
				.ForMember(ent => ent.TypeDesc, opts => opts.Ignore());

			// QuoteItemData to OrderItem
			CreateMap<QuoteItemData, OrderItem>();

			// QuoteData to Order
			CreateMap<QuoteData, Order>()
				.ForMember(ent => ent.BillingDayDesc, opts => opts.Ignore())
				.ForMember(ent => ent.SeasonDesc, opts => opts.Ignore())
				.ForMember(ent => ent.BillingStartDesc, opts => opts.Ignore())
				.ForMember(ent => ent.TypeDesc, opts => opts.Ignore())
				.ForMember(ent => ent.Id, opts => opts.Ignore())
				.ForMember(ent => ent.QuoteId, opts => opts.MapFrom(dto => dto.Id));

			// Service
			CreateMap<Service, ServiceData>();
			CreateMap<ServiceData, Service>();

			// WorkOrder
			CreateMap<WorkOrder, WorkOrderData>();
			CreateMap<WorkOrderData, WorkOrder>();
		}
	}
}