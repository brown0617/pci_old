namespace Backend.API.Models
{
	public class PropertyData
	{
		/// <summary>
		///     Property Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Name of the property
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Street address line 1 of the property
		/// </summary>
		public string AddressStreet1 { get; set; }

		/// <summary>
		///     Street address line 2 of the property
		/// </summary>
		public string AddressStreet2 { get; set; }

		/// <summary>
		///     City the property is located in
		/// </summary>
		public string AddressCity { get; set; }

		/// <summary>
		///     State the property is located in
		/// </summary>
		public string AddressState { get; set; }

		/// <summary>
		///     Zip code for the property
		/// </summary>
		public string AddressZip { get; set; }
	}
}