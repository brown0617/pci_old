using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
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
			var options = new List<Option>();
			switch (id)
			{
				case "BillingDay":
					options = _repository.GetBillingDays();
					break;
				case "Month":
					options = _repository.GetMonths();
					break;
				case "QuoteStatus":
					options = _repository.GetQuoteStatuses();
					break;
				case "QuoteType":
					options = _repository.GetQuoteTypes();
					break;
				case "Season":
					options = _repository.GetSeasons();
					break;
			}

			_mapper.Map(options, optionData);
			return optionData;
		}
	}
}