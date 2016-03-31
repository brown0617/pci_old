using System;

namespace Backend.Domain.Enums
{
	public class EnumDescription : Attribute
	{
		public EnumDescription(string text)
		{
			Text = text;
		}

		public string Text { get; private set; }
	}
}