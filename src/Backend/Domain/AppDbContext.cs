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
		public IDbSet<Employee> Employees { get; set; }
		public IDbSet<Material> Materials { get; set; }
		public IDbSet<Order> Orders { get; set; }
		public IDbSet<OrderItem> OrderItems { get; set; }
		public IDbSet<Person> People { get; set; }
		public IDbSet<Property> Properties { get; set; }
		public IDbSet<Quote> Quotes { get; set; }
		public IDbSet<QuoteItem> QuoteItems { get; set; }
		public IDbSet<Service> Services { get; set; }
		public IDbSet<State> States { get; set; }
		public IDbSet<WorkOrder> WorkOrders { get; set; }
		public IDbSet<WorkOrderTime> WorkOrderTimes { get; set; }

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
					if (entity != null) entity.CreatedOn = DateTime.UtcNow;
				if (entity != null) entity.ModifiedOn = DateTime.UtcNow;
			}
			return base.SaveChanges();
		}
	}

	public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
	{
		private IEnumerable<County> _counties;
		private IEnumerable<Employee> _employees;
		// seed data from MS Dynamics CRM extract
		private IEnumerable<Person> _people;

		protected override void Seed(AppDbContext pciContext)
		{
			base.Seed(pciContext);

			var crmContext = new CrmDbContext();

			// EmployeeBase ==> Employee
			crmContext.Employees.ToList().ForEach(x => pciContext.Employees.Add(new Employee
			{
				Name = x.New_employeename,
				CrmEmployeeId = x.EmployeeId,
				DateHired = x.New_DateHired,
				DateTerminated = x.New_DateTerminated,
				MobilePhone = x.New_MobilePhone
			}));
			pciContext.SaveChanges();
			_employees = pciContext.Employees.ToList();

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
					glItemId = pciContext.GlItems.Where(w => w.CrmGlItemId == crmGlItemId).Select(s => s.Id).FirstOrDefault();
				var productName = CleanProductName(product.p.Name);
				pciContext.Services.Add(new Service
				{
					Name = productName,
					Description = product.pe.New_Description,
					Cost = product.p.CurrentCost ?? 0,
					Price = product.p.Price ?? 0,
					GlItemId = glItemId,
					CrmProductId = product.p.ProductId,
					CompleteCare = product.pe.New_Description != null,
					Season = GetSeason(productName)
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
					glItemId = pciContext.GlItems.Where(w => w.CrmGlItemId == crmGlItemId).Select(s => s.Id).FirstOrDefault();
				pciContext.Materials.Add(new Material
				{
					Name = material.p.Name,
					Description = material.p.Description,
					Cost = material.p.CurrentCost ?? 0,
					Price = material.p.Price ?? 0,
					GlItemId = glItemId,
					CrmProductId = material.p.ProductId
				});
			}
			pciContext.SaveChanges();

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
					newProperty.CustomerId = customerId;
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
						newProperty.CustomerId = customerId;
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
					var crmQuoteId = quote.quote.QuoteId;
					var contractTermYears = quote.quoteExt.New_ContractTermYears ?? 1;
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
						NumberOfPayments = quote.quoteExt.New_NumPayments ?? 1,
						SalesTaxAmount = quote.quoteExt.New_SalesTaxAmount ?? 0,
						SalesTaxRate = property.County.SalesTaxRate,
						Season = (Season) (quote.quoteExt.New_Season ?? 1),
						Status = (QuoteStatus) statusCode,
						Taxable = quote.quoteExt.New_Taxable ?? true,
						Title = quote.quote.Name,
						TotalLaborPrice = quote.quoteExt.New_TotalAmountLabor ?? 0,
						TotalMaterialPrice = quote.quoteExt.New_TotalAmountMaterials ?? 0,
						TotalPricePretax = quote.quoteExt.New_TotalAmountPretax ?? 0,
						TotalPrice = quote.quoteExt.New_TotalAmountQuote ?? 0,
						TotalEstimatedManHours = quote.quoteExt.New_TotalManHoursEst ?? 0,
						Type = (QuoteType) (quote.quoteExt.New_QuoteType ?? 0),
						CreatedOn = quote.quote.CreatedOn ?? DateTime.UtcNow,
						ModifiedOn = quote.quote.ModifiedOn ?? DateTime.UtcNow
					};

					pciContext.Quotes.Add(newQuote);
					pciContext.SaveChanges();

					// QuoteDetailBase/Extension ==> QuoteItem
					var quoteItems =
						crmContext.QuoteDetails.Join(crmContext.QuoteDetailsExtension, qd => qd.QuoteDetailId, qde => qde.QuoteDetailId,
								(qd, qde) => new {qd, qde})
							.Where(w => w.qd.QuoteId == crmQuoteId)
							.ToList();
					foreach (var quoteItem in quoteItems)
					{
						var quoteId =
							pciContext.Quotes.Where(w => w.CrmQuoteId == quoteItem.qd.QuoteId).Select(s => s.Id).FirstOrDefault();
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
							ManualDiscountAmount = quoteItem.qd.ManualDiscountAmount ?? 0,
							MaterialId = materialId,
							MaterialPrice = (quoteItem.qde.New_PricePerUnit_Materials ?? 0)*(quoteItem.qde.New_QuantityMaterials ?? 0),
							MaterialQuantity = quoteItem.qde.New_QuantityMaterials ?? 0,
							MaterialUnitPrice = quoteItem.qde.New_PricePerUnit_Materials ?? 0,
							NumberOfPayments = quoteItem.qde.New_NumPayments ?? 0,
							QuoteId = quoteId,
							ServiceDeadline = quoteItem.qde.New_ServiceDeadline,
							ServiceFrequency = quoteItem.qde.New_ServiceFrequency,
							ServiceId = serviceId,
							ServicePrice = quoteItem.qd.ExtendedAmount ?? 0,
							ServiceQuantity = quoteItem.qd.Quantity ?? 0,
							ServiceUnitPrice = quoteItem.qd.PricePerUnit ?? 0,
							Visits = quoteItem.qde.New_Visits ?? 0,
							CreatedOn = quoteItem.qd.CreatedOn ?? DateTime.UtcNow,
							ModifiedOn = quoteItem.qd.ModifiedOn ?? DateTime.UtcNow
						};
						pciContext.QuoteItems.Add(newQuoteItem);
						pciContext.SaveChanges();
					}

					// SalesOrderBase/SalesOrderExtensionBase ==> Order
					var crmOrder =
						crmContext.SalesOrders
							.Join(crmContext.SalesOrdersExtension, order => order.SalesOrderId,
								orderExt => orderExt.SalesOrderId,
								(order, orderExt) => new {order, orderExt})
							.FirstOrDefault(w => w.order.QuoteId == crmQuoteId);

					if (crmOrder == null)
					{
						// no order
						if (newQuote.ContractYear != "2016")
						{
							// prior year quotes, mark them as lost
							newQuote.Status = QuoteStatus.Lost;
							pciContext.Quotes.AddOrUpdate(newQuote);
							pciContext.SaveChanges();
						}
						continue;
					}
					var crmSalesOrderId = crmOrder.order.SalesOrderId;
					var orderContractYear = crmOrder.orderExt.New_ContractYear;
					var newOrder = new Order
					{
						AnnualIncreasePercentage = annualIncreasePercentage,
						BillingDay = (BillingDay) (crmOrder.orderExt.New_BillingDay ?? 1),
						BillingStart = (Month) (crmOrder.orderExt.New_BillingStart ?? 1),
						ContractTermYears = contractTermYears,
						ContractYear = orderContractYear,
						QuoteId = newQuote.Id,
						PropertyId = property.Id,
						CustomerId = (int) customerId,
						NumberOfPayments = crmOrder.orderExt.New_NumPayments ?? 1,
						SalesTaxAmount = crmOrder.orderExt.New_SalesTaxAmount ?? 0,
						SalesTaxRate = property.County.SalesTaxRate,
						Season = (Season) (crmOrder.orderExt.New_Season ?? 1),
						Taxable = crmOrder.orderExt.New_Taxable ?? true,
						Title = crmOrder.order.Name,
						TotalLaborPrice = crmOrder.orderExt.New_TotalAmountLabor ?? 0,
						TotalMaterialPrice = crmOrder.orderExt.New_TotalAmountMaterials ?? 0,
						TotalPricePretax = crmOrder.orderExt.New_TotalAmountPretax ?? 0,
						TotalPrice = crmOrder.orderExt.New_TotalAmountOrder ?? 0,
						TotalEstimatedManHours = crmOrder.orderExt.New_TotalManHoursEst ?? 0,
						Type = (QuoteType) (crmOrder.orderExt.New_OrderType ?? 0),
						CreatedOn = crmOrder.order.CreatedOn ?? DateTime.UtcNow,
						ModifiedOn = crmOrder.order.ModifiedOn ?? DateTime.UtcNow,
						CrmSalesOrderId = crmSalesOrderId
					};

					pciContext.Orders.Add(newOrder);
					pciContext.SaveChanges();

					// order created, mark quote as Won
					newQuote.Status = QuoteStatus.Won;
					pciContext.Quotes.AddOrUpdate(newQuote);
					pciContext.SaveChanges();

					// SalesOrderDetailBase/Extension ==> OrderItem
					var orderItems =
						crmContext.SalesOrderDetails.Join(crmContext.SalesOrderDetailsExtension, sod => sod.SalesOrderDetailId,
								sode => sode.SalesOrderDetailId,
								(sod, sode) => new {sod, sode})
							.Where(w => w.sod.SalesOrderId == crmSalesOrderId)
							.ToList();

					foreach (var orderItem in orderItems)
					{
						var orderId =
							pciContext.Orders.Where(w => w.CrmSalesOrderId == orderItem.sod.SalesOrderId).Select(s => s.Id).FirstOrDefault();
						if (orderId == 0) continue;

						int? serviceId = null;
						var serviceProductId = orderItem.sod.ProductId;
						if (serviceProductId != null)
						{
							serviceId = pciContext.Services.Where(w => w.CrmProductId == serviceProductId).Select(s => s.Id).FirstOrDefault();
							serviceId = serviceId == 0 ? null : serviceId;
						}
						int? materialId = null;
						var materialProductId = orderItem.sode.New_MaterialsId;
						if (materialProductId != null)
						{
							materialId =
								pciContext.Materials.Where(w => w.CrmProductId == materialProductId).Select(s => s.Id).FirstOrDefault();
							materialId = materialId == 0 ? null : materialId;
						}

						var newOrderItem = new OrderItem
						{
							BillingMethod = orderItem.sode.New_BillingMethod,
							BillingStart = orderItem.sode.New_BillingStart,
							Description = orderItem.sode.New_Details,
							ManualDiscountAmount = orderItem.sod.ManualDiscountAmount ?? 0,
							MaterialId = materialId,
							MaterialPrice = (orderItem.sode.New_PricePerUnitMaterials ?? 0)*(orderItem.sode.New_QuantityMaterials ?? 0),
							MaterialQuantity = orderItem.sode.New_QuantityMaterials ?? 0,
							MaterialUnitPrice = orderItem.sode.New_PricePerUnitMaterials ?? 0,
							NumberOfPayments = orderItem.sode.New_NumPayments ?? 0,
							OrderId = orderId,
							ServiceDeadline = orderItem.sode.New_ServiceDeadline,
							ServiceFrequency = orderItem.sode.New_ServiceFrequency,
							ServiceId = serviceId,
							ServicePrice = orderItem.sod.ExtendedAmount ?? 0,
							ServiceQuantity = orderItem.sod.Quantity ?? 0,
							ServiceUnitPrice = orderItem.sod.PricePerUnit ?? 0,
							Visits = orderItem.sode.New_visits ?? 0,
							CreatedOn = orderItem.sod.CreatedOn ?? DateTime.UtcNow,
							ModifiedOn = orderItem.sod.ModifiedOn ?? DateTime.UtcNow
						};
						pciContext.OrderItems.Add(newOrderItem);
						pciContext.SaveChanges();

						// WorkOrderBase ==> WorkOrder
						var workOrders =
							crmContext.WorkOrders.Where(
								w => (w.New_SalesOrder_WorkOrderId == crmSalesOrderId) && (w.New_Details == newOrderItem.Description)).ToList();

						if (workOrders == null) continue;

						foreach (var workOrder in workOrders)
						{
							var crmWorkOrderId = workOrder.WorkOrderId;

							var newWorkOrder = new WorkOrder
							{
								ActualCompletion =
									(workOrder.New_ActualCompletion == null) && (orderContractYear != "2016")
										? Convert.ToDateTime("12/31/" + orderContractYear)
										: workOrder.New_ActualCompletion,
								ActualCrewSize = workOrder.New_ActualCrewSize,
								ActualManHours = workOrder.New_ActualManHours,
								ActualStart = workOrder.New_ActualStart,
								BillingComments = workOrder.New_BillingComments,
								Details = workOrder.New_Details,
								ForemanId = GetEmployeeId(workOrder.New_ForemanId),
								InvoiceNumber = workOrder.New_InvoiceNumber,
								ManHourVariance = workOrder.New_ManHourVariance,
								OrderItemId = newOrderItem.Id,
								ProjectedCompletion = workOrder.New_ProjectedCompletion,
								ProjectedStart = workOrder.New_ProjectedStart,
								ScheduledCompletion = workOrder.New_ScheduledCompletion,
								ScheduledCrewSize = workOrder.New_ScheduledCrewSize,
								ScheduledStart = workOrder.New_ScheduledStart,
								VarianceExplanation = workOrder.New_VarianceExplanation,
								VisitNumber = workOrder.New_VisitNumber,
								CrmWorkOrderId = crmWorkOrderId
							};
							pciContext.WorkOrders.AddOrUpdate(newWorkOrder);
							pciContext.SaveChanges();

							var workOrderTimes = crmContext.WorkOrderFulfillment.Where(w => w.WorkOrderId == crmWorkOrderId).ToList();

							if (workOrderTimes == null) continue;

							foreach (var workOrderTime in workOrderTimes)
							{
								var newWorkOrderTime = new WorkOrderTime
								{
									ActualManHours = workOrderTime.New_ActualManHours,
									Arrival = workOrderTime.New_Arrival,
									Break1Finish = workOrderTime.New_Break1Finish,
									Break1Start = workOrderTime.New_Break1Start,
									Break2Finish = workOrderTime.New_Break2Finish,
									Break2Start = workOrderTime.New_Break2Start,
									CrewSize = workOrderTime.New_CrewSize ?? 0,
									Departure = workOrderTime.New_Departure,
									ForemanId = GetEmployeeId(workOrderTime.New_CrewForemanId),
									JobComplete = workOrderTime.New_JobComplete ?? false,
									LunchFinish = workOrderTime.New_LunchFinish,
									LunchStart = workOrderTime.New_LunchStart,
									WorkOrderId = newWorkOrder.Id
								};
								pciContext.WorkOrderTimes.AddOrUpdate(newWorkOrderTime);
								pciContext.SaveChanges();
							}
						}
					}
				}
			}
		}

		private int GetCountyId(Guid? countyId)
		{
			var defaultCounty = _counties.Where(w => w.Name == "Monroe").Select(x => x.Id).FirstOrDefault();
			if (countyId == null)
				return defaultCounty;
			var county = _counties.Where(w => w.CrmCountyId == countyId).Select(x => x.Id).FirstOrDefault();
			return county == 0 ? defaultCounty : county;
		}

		private int? GetPersonId(Guid? crmContactId)
		{
			if (crmContactId == null)
				return null;

			return _people.Where(w => w.CrmContactId == crmContactId).Select(x => x.Id).FirstOrDefault();
		}

		private int? GetEmployeeId(Guid? crmEmployeeId)
		{
			if (crmEmployeeId == null)
				return null;

			var employeeId = _employees.Where(w => w.CrmEmployeeId == crmEmployeeId).Select(x => x.Id).FirstOrDefault();

			return employeeId == 0 ? (int?) null : employeeId;
		}

		private string CleanProductName(string productName)
		{
			var newProductName = productName.Replace("Maintenance, ", "");
			newProductName = newProductName.Replace("Service, ", "");
			newProductName = newProductName.Replace("Labor, ", "");
			return newProductName;
		}

		private Season GetSeason(string productName)
		{
			var snowProducts = new List<string>
			{
				"Salting (Roadways)",
				"Plowing",
				"Snow Plowing",
				"Sidewalk Snow Clearing",
				"Salting (SideWalks)",
				"Plow Damage Repair",
				"Salting (Driveways)",
				"Snow Plowing (per visit)",
				"Loader"
			};
			return snowProducts.Contains(productName) ? Season.Winter : Season.Summer;
		}


		private bool IsCommercialProperty(Guid? crmAccountId)
		{
			var crmContext = new CrmDbContext();

			if (crmAccountId == null)
				return false;

			var account = crmContext.Accounts.FirstOrDefault(w => w.AccountId == crmAccountId);

			return (account != null) && account.ParentAccountId.HasValue &&
			       !crmContext.Accounts.Any(w => w.ParentAccountId == account.AccountId);
		}
	}
}