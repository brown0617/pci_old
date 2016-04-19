using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	[RoutePrefix("api/options")]
	public class OptionsController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IOptionRepository _repository;

		public OptionsController(IOptionRepository optionRepository, IMapper mapper)
		{
			_mapper = mapper;
			_repository = optionRepository;
		}

		[Route("billingDay")]
		public IEnumerable<OptionData> GetBillingDayOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetBillingDayList(), optionData);
			return optionData;
		}

		[Route("billingMethod")]
		public IEnumerable<OptionData> GetBillingMethodOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetBillingMethodList(), optionData);
			return optionData;
		}

		[Route("month")]
		public IEnumerable<OptionData> GetMonthOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetMonthList(), optionData);
			return optionData;
		}

		[Route("quoteStatus")]
		public IEnumerable<OptionData> GetQuoteStatusOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetQuoteStatusList(), optionData);
			return optionData;
		}

		[Route("quoteType")]
		public IEnumerable<OptionData> GetQuoteType()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetQuoteTypeList(), optionData);
			return optionData;
		}

		[Route("season")]
		public IEnumerable<OptionData> GetSeasonOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetSeasonList(), optionData);
			return optionData;
		}

		[Route("serviceFrequency")]
		public IEnumerable<OptionData> GetServiveFrequencyOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetServiceFrequencyList(), optionData);
			return optionData;
		}
	}
}