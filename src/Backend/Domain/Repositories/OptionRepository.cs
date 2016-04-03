using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Domain.Repositories
{
	public class OptionRepository : IOptionRepository
	{
		public List<Option> GetBillingDays()
		{
			var options = EnumExtensions.GetValues<BillingDay>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetMonths()
		{
			var options = EnumExtensions.GetValues<Month>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetQuoteStatuses()
		{
			var options = EnumExtensions.GetValues<QuoteStatus>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetQuoteTypes()
		{
			var options = EnumExtensions.GetValues<QuoteType>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetSeasons()
		{
			var options = EnumExtensions.GetValues<Season>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}
	}
}