using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
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
		public IDbSet<Person> People { get; set; }
		public IDbSet<Property> Properties { get; set; }
		public IDbSet<Quote> Quotes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}

	public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
	{
		// seed data from MS Dynamics CRM extract
		private IEnumerable<Person> _people;

		protected override void Seed(AppDbContext pciContext)
		{
			base.Seed(pciContext);

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

			_people = pciContext.People.ToList();

			// Accounts ==> Property (Commercial)
			var allCrmAccounts = crmContext.Accounts.Select(c => new
			{
				Account = c,
				Address = c.Addresses.FirstOrDefault(a => a.AddressNumber == 1)
			}).ToList();

			foreach (
				var account in
					allCrmAccounts.Where(account => !crmContext.Accounts.Any(w => w.ParentAccountId == account.Account.AccountId)))
			{
				int? customerId = null;
				var crmParentAccountId = account.Account.ParentAccountId;
				if (crmParentAccountId == null)
				{
					// property has no parent account in crm so create customer from property info
					var newFakeCustomer = new CommercialCustomer
					{
						Name = account.Account.Name,
						BillingAddressStreet1 = account.Address.Line1,
						BillingAddressStreet2 = account.Address.Line2,
						BillingAddressCity = account.Address.City,
						BillingAddressState = account.Address.StateOrProvince,
						BillingAddressZip = account.Address.PostalCode,
						CrmAccountId = account.Account.AccountId,
						PrimaryContactId = GetPersonId(account.Account.PrimaryContactId)
					};
					pciContext.Customers.Add(newFakeCustomer);
					pciContext.SaveChanges();
					customerId = newFakeCustomer.Id;
				}

				// add the property
				var newProperty = new Property
				{
					Name = account.Account.Name,
					AddressStreet1 = account.Address.Line1,
					AddressStreet2 = account.Address.Line2,
					AddressCity = account.Address.City,
					AddressState = account.Address.StateOrProvince,
					AddressZip = account.Address.PostalCode,
					CrmAccountId = account.Account.AccountId,
					CrmParentAccountId = crmParentAccountId,
					PrimaryContactId = GetPersonId(account.Account.PrimaryContactId),
					CustomerId = customerId
				};
				pciContext.Properties.Add(newProperty);
				pciContext.SaveChanges();

				if (crmParentAccountId == null) continue;
				var existingCustomer = pciContext.Customers.FirstOrDefault(x => x.CrmAccountId == newProperty.CrmParentAccountId);
				if (existingCustomer != null)
				{
					// customer exists, add to property
					newProperty.CustomerId = existingCustomer.Id;
					pciContext.Properties.AddOrUpdate(newProperty);
				}
				else
				{
					// add the property's customer
					var parentAccount = allCrmAccounts.FirstOrDefault(x => x.Account.AccountId == account.Account.ParentAccountId);
					if (parentAccount != null)
					{
						var newCommercialCustomer = new CommercialCustomer
						{
							AccountNumber = parentAccount.Account.AccountNumber,
							BillingAddressCity = parentAccount.Address.City,
							BillingAddressStreet1 = parentAccount.Address.Line1,
							BillingAddressStreet2 = parentAccount.Address.Line2,
							BillingAddressState = parentAccount.Address.StateOrProvince,
							BillingAddressZip = parentAccount.Address.PostalCode,
							CrmAccountId = parentAccount.Account.AccountId,
							PrimaryContactId = GetPersonId(parentAccount.Account.PrimaryContactId),
							Name = parentAccount.Account.Name
						};
						pciContext.Customers.Add(newCommercialCustomer);
						pciContext.SaveChanges();

						newProperty.CustomerId = newCommercialCustomer.Id;
						pciContext.Properties.AddOrUpdate(newProperty);
					}
				}
			}

			// QuoteBase/QuoteExtensionBase ==> Quote
			var quotes =
				crmContext.QuoteBases.Join(crmContext.QuoteExtensionBases, quote => quote.QuoteId, quoteExt => quoteExt.QuoteId,
					(quote, quoteExt) => new {quote, quoteExt})
					.Where(c => c.quote.AccountId.HasValue)
					.ToList();

			foreach (var quote in quotes)
			{
				var property =
					pciContext.Properties.FirstOrDefault(w => w.CrmAccountId == quote.quote.AccountId);

				if (property == null) continue;

				var newQuote = new Quote
				{
					AnnualIncreasePercentage = quote.quoteExt.New_AnnualIncrease ?? 0,
					BillingDay = (BillingDay) (quote.quoteExt.New_BillingDay ?? 1),
					BillingStart = (Month) (quote.quoteExt.New_BillingStart ?? 1),
					ContractTermYears = (quote.quoteExt.New_ContractTermYears ?? 1),
					ContractYear = quote.quoteExt.New_ContractYear,
					PropertyId = property.Id,
					NumberOfPayments = (quote.quoteExt.New_NumPayments ?? 1),
					SalesTaxAmount = (quote.quoteExt.New_SalesTaxAmount ?? 0),
					Season = (Season) (quote.quoteExt.New_Season ?? 1),
					Status = (QuoteStatus) quote.quote.StatusCode,
					Taxable = (quote.quoteExt.New_Taxable ?? true),
					Title = quote.quote.Name,
					TotalAmountLabor = (quote.quoteExt.New_TotalAmountLabor ?? 0),
					TotalAmountMaterials = (quote.quoteExt.New_TotalAmountMaterials ?? 0),
					TotalAmountPretax = (quote.quoteExt.New_TotalAmountPretax ?? 0),
					TotalAmountQuote = (quote.quoteExt.New_TotalAmountQuote ?? 0),
					TotalEstimatedManHours = (quote.quoteExt.New_TotalManHoursEst ?? 0),
					Type = (QuoteType) (quote.quoteExt.New_QuoteType ?? 0)
				};

				pciContext.Quotes.Add(newQuote);
				pciContext.SaveChanges();
			}
		}

		private int? GetPersonId(Guid? crmContactId)
		{
			if (crmContactId == null)
			{
				return null;
			}

			return _people.Where(w => w.CrmContactId == crmContactId).Select(x => x.Id).FirstOrDefault();
		}

		//private int? GetCustomerId(Guid? crmAccountId)
		//{
		//	if (crmAccountId == null)
		//	{
		//		return null;
		//	}

		//	return _customers.Where(w => w.CrmAccountId == crmAccountId).Select(x => x.Id).FirstOrDefault();
		//}

		//private bool IsCommercialCustomer(Guid? crmAccountId)
		//{
		//	var crmContext = new CrmDbContext();

		//	if (crmAccountId == null)
		//	{
		//		return false;
		//	}

		//	var account = crmContext.Accounts.FirstOrDefault(w => w.AccountId == crmAccountId);

		//	return (account != null && allCrmAccounts.Any(w => w.ParentAccountId == account.AccountId));
		//}

		private bool IsCommercialProperty(Guid? crmAccountId)
		{
			var crmContext = new CrmDbContext();

			if (crmAccountId == null)
			{
				return false;
			}

			var account = crmContext.Accounts.FirstOrDefault(w => w.AccountId == crmAccountId);

			return (account != null && account.ParentAccountId.HasValue &&
			        !crmContext.Accounts.Any(w => w.ParentAccountId == account.AccountId));
		}
	}
}