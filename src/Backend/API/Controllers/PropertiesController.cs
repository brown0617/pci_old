using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	public class PropertiesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IPropertyRepository _repository;

		public PropertiesController(IPropertyRepository propertyRepository, IMapper mapper)
		{
			_repository = propertyRepository;
			_mapper = mapper;
		}

		public IEnumerable<PropertyData> Get()
		{
			var propertyData = new List<PropertyData>();
			_mapper.Map(_repository.Get(), propertyData);
			return propertyData.OrderBy(x => x.Name);
		}

		[Route("api/properties/customer/{customerId}")]
		public IEnumerable<PropertyData> GetByCustomerId(int customerId)
		{
			var propertyData = new List<PropertyData>();
			var property = _repository.Get().Where(w => w.CustomerId == customerId);
			_mapper.Map(property, propertyData);
			return propertyData.OrderBy(x => x.Name);
		}

		public PropertyData Get(int id)
		{
			var propertyData = new PropertyData();
			_mapper.Map(_repository.Get(id), propertyData);
			return propertyData;
		}

		public void Put([FromBody] PropertyData propertyData)
		{
			var property = _mapper.Map(propertyData, new Property());
			_repository.Save(property);
		}
	}
}