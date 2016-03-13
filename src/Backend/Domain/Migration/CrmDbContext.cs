using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Backend.Domain.Migration
{	
	public class CrmDbContext :DbContext
	{
		public CrmDbContext()
			: base("CRMContext")
		{
		}

		public IDbSet<ContactBase> Contacts { get; set; }
		public IDbSet<AccountBase> Accounts { get; set; }
		public IDbSet<CustomerAddressBase> CustomerAddresses { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}