using System;
using System.Collections.Generic;
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
		public IDbSet<Person> People { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
	{
		private IEnumerable<Person> people;

		protected override void Seed(AppDbContext pciContext)
		{
			base.Seed(pciContext);

			// seed data from MS Dynamics CRM extract
			var crmContext = new CrmDbContext();

			// Contacts ==> People
			crmContext.Contacts.ToList().ForEach(
				x => pciContext.People.Add(
					new Person
					{
						FirstName = x.FirstName,
						LastName = x.LastName,
						MiddleName = x.MiddleName,
						CrmContactId = x.ContactId,
						EMailAddressHome = x.EMailAddress1,
						EMailAddressWork = x.EMailAddress2,
						Fax = x.Fax,
						MobilePhone = x.MobilePhone,
						Salutation = x.Salutation,
						Suffix = x.Suffix,
						TelephoneWork = x.Telephone1,
						TelephoneHome = x.Telephone2
					}));

			pciContext.SaveChanges();

			people = pciContext.People.ToList();

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
						PrimaryContactId = GetPersonId(x.Customer.PrimaryContactId),
						Name = x.Customer.Name
					}));

			// Accounts ==> Property (Commercial)
			crmContext.Accounts.Where(x => x.ParentAccountId.HasValue).ToList().ForEach(
				a =>
					pciContext.Properties.Add(new Property
					{
						Name = a.Name,
						CrmAccountId = a.AccountId
					}));

			pciContext.SaveChanges();
		}

		private int? GetPersonId(Guid? crmContactId)
		{
			if (crmContactId == null)
			{
				return null;
			}

			return people.Where(w => w.CrmContactId == crmContactId).Select(x => x.Id).FirstOrDefault();
		}
	}
}