using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Backend.Domain.Migration
{
	public class CrmDbContext : DbContext
	{
		public CrmDbContext()
			: base("CRMContext")
		{
		}

		public IDbSet<AccountBase> Accounts { get; set; }
		public IDbSet<AccountExtensionBase> AccountExtension { get; set; }
		public IDbSet<ContactBase> Contacts { get; set; }
		public IDbSet<CountyExtensionBase> Counties { get; set; }
		public IDbSet<CountySalesTaxExtensionBase> CountySalesTax { get; set; }
		public IDbSet<CustomerAddressBase> CustomerAddresses { get; set; }
		public IDbSet<GlItemBase> GlItems { get; set; }
		public IDbSet<ProductBase> Products { get; set; }
		public IDbSet<ProductExtensionBase> ProductsExtension { get; set; }
		public IDbSet<QuoteBase> Quotes { get; set; }
		public IDbSet<QuoteExtensionBase> QuotesExtension { get; set; }
		public IDbSet<QuoteDetailBase> QuoteDetails { get; set; }
		public IDbSet<QuoteDetailExtensionBase> QuoteDetailsExtension { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}