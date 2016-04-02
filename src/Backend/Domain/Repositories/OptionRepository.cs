using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class OptionRepository : IOptionRepository
	{
		public IEnumerable<Option> Get(string tEnum)
		{
			var enumType = Type.GetType(tEnum, true);

			if (!enumType.IsEnum)
			{
				throw new ArgumentException("tEnum must be an enum.");
			}

			var enumValues = (int[]) Enum.GetValues(enumType);
			return enumValues.Select(enumValue => new Option {Id = enumValue, Name = Enum.GetName(enumType, enumValue)}).ToList();
		}
	}
}