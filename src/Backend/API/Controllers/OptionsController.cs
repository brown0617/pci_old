using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	public class OptionsController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IOptionRepository _repository;

		public OptionsController(IOptionRepository optionRepository, IMapper mapper)
		{
			_mapper = mapper;
			_repository = optionRepository;
		}

		public IEnumerable<OptionData> Get(string id)
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.Get("Backend.Domain.Enums." + id), optionData);
			return optionData;
		}
	}
}