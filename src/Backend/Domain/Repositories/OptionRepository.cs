using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;
using Backend.Domain.Enums;

namespace Backend.Domain.Repositories
{
	public class OptionRepository : IOptionRepository
	{
		public List<Option> GetBillingDayList()
		{
			var options = EnumExtensions.GetValues<BillingDay>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetBillingMethodList()
		{
			var options = EnumExtensions.GetValues<BillingMethod>();
			return
				options.Select(option => new Option { Id = (int)option, Name = option.ToDescription() }).ToList();
		}

		public List<Option> GetMonthList()
		{
			var options = EnumExtensions.GetValues<Month>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetQuoteStatusList()
		{
			var options = EnumExtensions.GetValues<QuoteStatus>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetQuoteTypeList()
		{
			var options = EnumExtensions.GetValues<QuoteType>();
			return
				options.Select(option => new Option {Id = (int) option, Name = option.ToDescription()}).ToList();
		}

		public List<Option> GetSeasonList()
		{
			var options = EnumExtensions.GetValues<Season>();
			return
				options.Select(option => new Option { Id = (int)option, Name = option.ToDescription() }).ToList();
		}

		public List<Option> GetServiceFrequencyList()
		{
			var options = EnumExtensions.GetValues<ServiceFrequency>();
			return options.Select(option => new Option { Id = (int)option, Name = option.ToDescription() }).ToList();
		}
	}
}