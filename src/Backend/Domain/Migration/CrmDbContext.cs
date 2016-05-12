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
		public IDbSet<EmployeeBase> Employees { get; set; }
		public IDbSet<GlItemBase> GlItems { get; set; }
		public IDbSet<SalesOrderBase> SalesOrders { get; set; }
		public IDbSet<SalesOrderExtensionBase> SalesOrdersExtension { get; set; }
		public IDbSet<SalesOrderDetailBase> SalesOrderDetails { get; set; }
		public IDbSet<SalesOrderDetailExtensionBase> SalesOrderDetailsExtension { get; set; }
		public IDbSet<ProductBase> Products { get; set; }
		public IDbSet<ProductExtensionBase> ProductsExtension { get; set; }
		public IDbSet<QuoteBase> Quotes { get; set; }
		public IDbSet<QuoteExtensionBase> QuotesExtension { get; set; }
		public IDbSet<QuoteDetailBase> QuoteDetails { get; set; }
		public IDbSet<QuoteDetailExtensionBase> QuoteDetailsExtension { get; set; }
		public IDbSet<WorkOrderBase> WorkOrders { get; set; }
		public IDbSet<WorkOrderFulfillmentBase> WorkOrderFulfillment { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}