﻿using System.Collections.Generic;
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
			_mapper.Map(_repository.GetBillingDays(), optionData);
			return optionData;
		}

		[Route("month")]
		public IEnumerable<OptionData> GetMonthOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetMonths(), optionData);
			return optionData;
		}

		[Route("quoteStatus")]
		public IEnumerable<OptionData> GetQuoteStatusOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetQuoteStatuses(), optionData);
			return optionData;
		}

		[Route("quoteType")]
		public IEnumerable<OptionData> GetQuoteType()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetQuoteTypes(), optionData);
			return optionData;
		}

		[Route("season")]
		public IEnumerable<OptionData> GetSeasonOptions()
		{
			var optionData = new List<OptionData>();
			_mapper.Map(_repository.GetSeasons(), optionData);
			return optionData;
		}
	}
}