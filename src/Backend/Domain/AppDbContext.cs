using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

		public IDbSet<County> Counties { get; set; }
		public IDbSet<Customer> Customers { get; set; }
		public IDbSet<GlItem> GlItems { get; set; }
		public IDbSet<Material> Materials { get; set; }
		public IDbSet<Order> Orders { get; set; }
		public IDbSet<OrderItem> OrderItems { get; set; }
		public IDbSet<Person> People { get; set; }
		public IDbSet<Property> Properties { get; set; }
		public IDbSet<Quote> Quotes { get; set; }
		public IDbSet<QuoteItem> QuoteItems { get; set; }
		public IDbSet<Service> Services { get; set; }
		public IDbSet<State> States { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public override int SaveChanges()
		{
			var context = ((IObjectContextAdapter) this).ObjectContext;
			foreach (var entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
			{
				var entity = entry.Entity as IRowState;
				if (entry.State == EntityState.Added)
				{
					if (entity != null) entity.CreatedOn = DateTime.UtcNow;
				}
				if (entity != null) entity.ModifiedOn = DateTime.UtcNow;
			}
			return base.SaveChanges();
		}
	}

	public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
	{
		private IEnumerable<County> _counties;
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

			// State
			pciContext.States.Add(new State {Name = "New York", StateAbbreviation = "NY"});
			pciContext.SaveChanges();

			// CountyExtensionBase ==> Counties
			crmContext.Counties.Join(crmContext.CountySalesTax, county => county.CountyId, countyTax => countyTax.CountyId,
				(county, countyTax) => new {county, countyTax})
				.ToList()
				.ForEach(
					x => pciContext.Counties.Add(new County
					{
						Name = x.county.New_countyname,
						CrmCountyId = x.county.CountyId,
						SalesTaxRate = (x.countyTax.New_salestaxrate ?? 0)/100,
						StateAbbreviation = "NY"
					}));
			pciContext.SaveChanges();

			_counties = pciContext.Counties.ToList();

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
				var crmAccountId = account.Account.AccountId;
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
						BillingAddressState = "NY",
						BillingAddressZip = account.Address.PostalCode,
						CrmAccountId = crmAccountId,
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
					AddressState = "NY",
					AddressZip = account.Address.PostalCode,
					CrmAccountId = crmAccountId,
					CrmParentAccountId = crmParentAccountId,
					PrimaryContactId = GetPersonId(account.Account.PrimaryContactId),
					//CustomerId = customerId,
					AddressCountyId = GetCountyId(account.Account.AccountExtension.New_Address1_CountyId)
				};
				pciContext.Properties.Add(newProperty);
				pciContext.SaveChanges();

				if (crmParentAccountId == null) continue;
				var existingCustomer = pciContext.Customers.FirstOrDefault(x => x.CrmAccountId == newProperty.CrmParentAccountId);
				if (existingCustomer != null)
				{
					// customer exists, add to property
					customerId = existingCustomer.Id;
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

						customerId = newCommercialCustomer.Id;
						pciContext.Properties.AddOrUpdate(newProperty);
					}
				}

				// QuoteBase/QuoteExtensionBase ==> Quote
				var quotes =
					crmContext.Quotes.Join(crmContext.QuotesExtension, quote => quote.QuoteId, quoteExt => quoteExt.QuoteId,
						(quote, quoteExt) => new {quote, quoteExt})
						.Where(c => c.quote.AccountId == crmAccountId)
						.ToList();

				foreach (var quote in quotes)
				{
					var property =
						pciContext.Properties.Include("County").FirstOrDefault(w => w.CrmAccountId == quote.quote.AccountId);

					if (property == null) continue;

					var statusCode = 0;
					switch (quote.quote.StatusCode)
					{
						case 0:
							statusCode = 0;
							break;
						case 1:
							statusCode = 0;
							break;
						case 2:
							statusCode = 1;
							break;
					}

					var crmQuoteId = quote.quote.QuoteId;
					var contractTermYears = (quote.quoteExt.New_ContractTermYears ?? 1);
					var annualIncreasePercentage = quote.quoteExt.New_AnnualIncrease ?? 0;

					var newQuote = new Quote
					{
						AnnualIncreasePercentage = annualIncreasePercentage,
						BillingDay = (BillingDay) (quote.quoteExt.New_BillingDay ?? 1),
						BillingStart = (Month) (quote.quoteExt.New_BillingStart ?? 1),
						ContractTermYears = contractTermYears,
						ContractYear = quote.quoteExt.New_ContractYear,
						CrmQuoteId = crmQuoteId,
						PropertyId = property.Id,
						CustomerId = (int) customerId,
						NumberOfPayments = (quote.quoteExt.New_NumPayments ?? 1),
						SalesTaxAmount = (quote.quoteExt.New_SalesTaxAmount ?? 0),
						SalesTaxRate = property.County.SalesTaxRate,
						Season = (Season) (quote.quoteExt.New_Season ?? 1),
						Status = (QuoteStatus) statusCode,
						Taxable = (quote.quoteExt.New_Taxable ?? true),
						Title = quote.quote.Name,
						TotalLaborPrice = (quote.quoteExt.New_TotalAmountLabor ?? 0),
						TotalMaterialPrice = (quote.quoteExt.New_TotalAmountMaterials ?? 0),
						TotalPricePretax = (quote.quoteExt.New_TotalAmountPretax ?? 0),
						TotalPrice = (quote.quoteExt.New_TotalAmountQuote ?? 0),
						TotalEstimatedManHours = (quote.quoteExt.New_TotalManHoursEst ?? 0),
						Type = (QuoteType) (quote.quoteExt.New_QuoteType ?? 0),
						CreatedOn = (quote.quote.CreatedOn ?? DateTime.UtcNow),
						ModifiedOn = (quote.quote.ModifiedOn ?? DateTime.UtcNow)
					};

					pciContext.Quotes.Add(newQuote);
					pciContext.SaveChanges();

					// SalesOrderBase/SalesOrderExtensionBase ==> Order
					var orders =
						crmContext.SalesOrders.Join(crmContext.SalesOrdersExtension, order => order.SalesOrderId,
							orderExt => orderExt.SalesOrderId,
							(order, orderExt) => new {order, orderExt})
							.Where(w => w.order.QuoteId == crmQuoteId)
							.ToList();

					foreach (var order in orders)
					{
						if (property == null) continue;

						var newOrder = new Order
						{
							AnnualIncreasePercentage = annualIncreasePercentage,
							BillingDay = (BillingDay) (order.orderExt.New_BillingDay ?? 1),
							BillingStart = (Month) (order.orderExt.New_BillingStart ?? 1),
							ContractTermYears = contractTermYears,
							ContractYear = order.orderExt.New_ContractYear,
							QuoteId = newQuote.Id,
							PropertyId = property.Id,
							CustomerId = (int) customerId,
							NumberOfPayments = (order.orderExt.New_NumPayments ?? 1),
							SalesTaxAmount = (order.orderExt.New_SalesTaxAmount ?? 0),
							SalesTaxRate = property.County.SalesTaxRate,
							Season = (Season) (order.orderExt.New_Season ?? 1),
							Taxable = (order.orderExt.New_Taxable ?? true),
							Title = order.order.Name,
							TotalLaborPrice = (order.orderExt.New_TotalAmountLabor ?? 0),
							TotalMaterialPrice = (order.orderExt.New_TotalAmountMaterials ?? 0),
							TotalPricePretax = (order.orderExt.New_TotalAmountPretax ?? 0),
							TotalPrice = (order.orderExt.New_TotalAmountOrder ?? 0),
							TotalEstimatedManHours = (order.orderExt.New_TotalManHoursEst ?? 0),
							Type = (QuoteType) (order.orderExt.New_OrderType ?? 0),
							CreatedOn = (order.order.CreatedOn ?? DateTime.UtcNow),
							ModifiedOn = (order.order.ModifiedOn ?? DateTime.UtcNow),
							CrmSalesOrderId = order.order.SalesOrderId
						};

						pciContext.Orders.Add(newOrder);
						pciContext.SaveChanges();
					}
				}
			}

			// GlItemBase ==> GlItem
			crmContext.GlItems.ToList().ForEach(x => pciContext.GlItems.Add(new GlItem
			{
				Name = x.New_itemname,
				SubitemOf = x.New_Subitemof,
				CrmGlItemId = x.GlItemId
			}));
			pciContext.SaveChanges();

			// ProductBase/Extension ==> Service
			var products = crmContext.Products.Join(crmContext.ProductsExtension, p => p.ProductId, pe => pe.ProductId,
				(p, pe) => new {p, pe})
				.Where(w => w.p.Name.StartsWith("Service") || w.p.Name.StartsWith("Maintenance") || w.p.Name.StartsWith("Labor"))
				.ToList();

			foreach (var product in products)
			{
				int? glItemId = null;
				var crmGlItemId = product.pe.New_GLItemId;
				if (crmGlItemId.HasValue)
				{
					glItemId = pciContext.GlItems.Where(w => w.CrmGlItemId == crmGlItemId).Select(s => s.Id).FirstOrDefault();
				}
				pciContext.Services.Add(new Service
				{
					Name = CleanProductName(product.p.Name),
					Description = product.pe.New_Description,
					Cost = (product.p.CurrentCost ?? 0),
					Price = (product.p.Price ?? 0),
					GlItemId = glItemId,
					CrmProductId = product.p.ProductId,
					CompleteCare = product.pe.New_Description != null,
					Season = Season.Summer
				});
			}
			pciContext.SaveChanges();

			// ProductBase/Extension ==> Material
			var materials = crmContext.Products.Join(crmContext.ProductsExtension, p => p.ProductId, pe => pe.ProductId,
				(p, pe) => new {p, pe})
				.Where(w => !(w.p.Name.StartsWith("Service") || w.p.Name.StartsWith("Maintenance") || w.p.Name.StartsWith("Labor")))
				.ToList();

			foreach (var material in materials)
			{
				int? glItemId = null;
				var crmGlItemId = material.pe.New_GLItemId;
				if (crmGlItemId.HasValue)
				{
					glItemId = pciContext.GlItems.Where(w => w.CrmGlItemId == crmGlItemId).Select(s => s.Id).FirstOrDefault();
				}
				pciContext.Materials.Add(new Material
				{
					Name = material.p.Name,
					Description = material.p.Description,
					Cost = (material.p.CurrentCost ?? 0),
					Price = (material.p.Price ?? 0),
					GlItemId = glItemId,
					CrmProductId = material.p.ProductId
				});
			}
			pciContext.SaveChanges();

			// QuoteDetailBase/Extension ==> QuoteItem
			var quoteItems =
				crmContext.QuoteDetails.Join(crmContext.QuoteDetailsExtension, qd => qd.QuoteDetailId, qde => qde.QuoteDetailId,
					(qd, qde) => new {qd, qde})
					.ToList();
			foreach (var quoteItem in quoteItems)
			{
				var quoteId = pciContext.Quotes.Where(w => w.CrmQuoteId == quoteItem.qd.QuoteId).Select(s => s.Id).FirstOrDefault();
				if (quoteId == 0) continue;

				int? serviceId = null;
				var serviceProductId = quoteItem.qd.ProductId;
				if (serviceProductId != null)
				{
					serviceId = pciContext.Services.Where(w => w.CrmProductId == serviceProductId).Select(s => s.Id).FirstOrDefault();
					serviceId = serviceId == 0 ? null : serviceId;
				}
				int? materialId = null;
				var materialProductId = quoteItem.qde.New_MaterialsId;
				if (materialProductId != null)
				{
					materialId =
						pciContext.Materials.Where(w => w.CrmProductId == materialProductId).Select(s => s.Id).FirstOrDefault();
					materialId = materialId == 0 ? null : materialId;
				}

				var newQuoteItem = new QuoteItem
				{
					BillingMethod = quoteItem.qde.New_BillingMethod,
					BillingStart = quoteItem.qde.New_BillingStart,
					Description = quoteItem.qde.New_Details,
					ManualDiscountAmount = (quoteItem.qd.ManualDiscountAmount ?? 0),
					MaterialId = materialId,
					MaterialPrice = (quoteItem.qde.New_PricePerUnit_Materials ?? 0)*(quoteItem.qde.New_QuantityMaterials ?? 0),
					MaterialQuantity = (quoteItem.qde.New_QuantityMaterials ?? 0),
					MaterialUnitPrice = (quoteItem.qde.New_PricePerUnit_Materials ?? 0),
					NumberOfPayments = (quoteItem.qde.New_NumPayments ?? 0),
					QuoteId = quoteId,
					ServiceDeadline = quoteItem.qde.New_ServiceDeadline,
					ServiceFrequency = quoteItem.qde.New_ServiceFrequency,
					ServiceId = serviceId,
					ServicePrice = (quoteItem.qd.ExtendedAmount ?? 0),
					ServiceQuantity = (quoteItem.qd.Quantity ?? 0),
					ServiceUnitPrice = (quoteItem.qd.PricePerUnit ?? 0),
					Visits = (quoteItem.qde.New_Visits ?? 0),
					CreatedOn = (quoteItem.qd.CreatedOn ?? DateTime.UtcNow),
					ModifiedOn = (quoteItem.qd.ModifiedOn ?? DateTime.UtcNow)
				};
				pciContext.QuoteItems.Add(newQuoteItem);
				pciContext.SaveChanges();
			}
		}

		private int GetCountyId(Guid? countyId)
		{
			var defaultCounty = _counties.Where(w => w.Name == "Monroe").Select(x => x.Id).FirstOrDefault();
			if (countyId == null)
			{
				return defaultCounty;
			}
			var county = _counties.Where(w => w.CrmCountyId == countyId).Select(x => x.Id).FirstOrDefault();
			return county == 0 ? defaultCounty : county;
		}

		private int? GetPersonId(Guid? crmContactId)
		{
			if (crmContactId == null)
			{
				return null;
			}

			return _people.Where(w => w.CrmContactId == crmContactId).Select(x => x.Id).FirstOrDefault();
		}

		private string CleanProductName(string productName)
		{
			var newProductName = productName.Replace("Maintenance, ", "");
			newProductName = newProductName.Replace("Service, ", "");
			newProductName = newProductName.Replace("Labor, ", "");
			return newProductName;
		}

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