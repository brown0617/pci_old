using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IOptionRepository
	{
		List<Option> GetBillingDays();
		List<Option> GetMonths();
		List<Option> GetQuoteStatuses();
		List<Option> GetQuoteTypes();
		List<Option> GetSeasons();
	}
}