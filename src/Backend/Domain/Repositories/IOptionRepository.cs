using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IOptionRepository
	{
		List<Option> GetBillingDayList();
		List<Option> GetBillingMethodList();
		List<Option> GetMonthList();
		List<Option> GetQuoteStatusList();
		List<Option> GetQuoteTypeList();
		List<Option> GetSeasonList();
		List<Option> GetServiceFrequencyList();
	}
}