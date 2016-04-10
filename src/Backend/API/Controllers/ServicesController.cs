using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	public class ServicesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IServiceRepository _repository;

		public ServicesController(IServiceRepository quoteRepository, IMapper mapper)
		{
			_repository = quoteRepository;
			_mapper = mapper;
		}

		public IEnumerable<ServiceData> Get()
		{
			var serviceData = new List<ServiceData>();
			_mapper.Map(_repository.Get(), serviceData);
			return serviceData;
		}
	}
}