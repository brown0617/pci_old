﻿using System;
using System.Collections.Generic;

namespace Backend.Domain.Enums
{
	public static class EnumExtensions
	{
		public static string ToDescription(this Enum enumeration)
		{
			var type = enumeration.GetType();
			var memInfo = type.GetMember(enumeration.ToString());

			if (memInfo.Length <= 0) return enumeration.ToString();
			var attrs = memInfo[0].GetCustomAttributes(typeof (EnumDescription), false);
			return attrs.Length > 0 ? ((EnumDescription) attrs[0]).Text : enumeration.ToString();
		}

		public static IReadOnlyList<T> GetValues<T>()
		{
			return (T[]) Enum.GetValues(typeof (T));
		}
	}
}