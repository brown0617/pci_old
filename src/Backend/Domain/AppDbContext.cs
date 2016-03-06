using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Backend.Domain.Entities;
using Backend.Domain.Migration;

namespace Backend.Domain
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("PCIData")
		{
		}

		public IDbSet<Customer> Customers { get; set; }
		public IDbSet<Property> Properties { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
	{
		protected override void Seed(AppDbContext pciContext)
		{
			base.Seed(pciContext);

			// seed data from MS Dynamics CRM extract
			var crmContext = new CrmDbContext();

			// Accounts ==> CommercialCustomer
			crmContext.Accounts.Select(c => new
			{
				Customer = c,
				Address = c.Addresses.FirstOrDefault(a => a.AddressNumber == 1)
			}).Where(w => !w.Customer.ParentAccountId.HasValue).ToList().ForEach(
				x => pciContext.Customers.Add(
					new CommercialCustomer
					{
						AccountNumber = x.Customer.AccountNumber,
						BillingAddressCity = x.Address.City,
						BillingAddressStreet1 = x.Address.Line1,
						BillingAddressStreet2 = x.Address.Line2,
						BillingAddressState = x.Address.StateOrProvince,
						BillingAddressZip = x.Address.PostalCode,
						CrmAccountId = x.Customer.AccountId,
						Name = x.Customer.Name
					}));

			// Accounts ==> Property
			crmContext.Accounts.Where(x => x.ParentAccountId.HasValue).ToList().ForEach(
				a =>
					pciContext.Properties.Add(new Property
					{
						Name = a.Name,
						CrmAccountId = a.AccountId
					}));

			pciContext.SaveChanges();
		}
	}
}