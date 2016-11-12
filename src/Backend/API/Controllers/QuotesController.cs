using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Enums;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[RoutePrefix("api/quotes")]
	[ExceptionHandling]
	public class QuotesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _orderRepository;
		private readonly IQuoteRepository _repository;

		public QuotesController(IQuoteRepository quoteRepository, IOrderRepository orderRepository, IMapper mapper)
		{
			_repository = quoteRepository;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public IEnumerable<QuoteData> Get()
		{
			var quoteData = new List<QuoteData>();
			_mapper.Map(_repository.Get(), quoteData);
			return quoteData.OrderBy(x => x.PropertyName).ThenByDescending(x => x.ContractYear);
		}

		[Route("active")]
		public IEnumerable<QuoteData> GetAllActive()
		{
			var quoteData = new List<QuoteData>();
			_mapper.Map(_repository.Get().Where(x => x.Status == QuoteStatus.Active), quoteData);
			return quoteData.OrderBy(x => x.PropertyName).ThenByDescending(x => x.ContractYear);
		}

		public QuoteData Get(int id)
		{
			var quoteData = new QuoteData();
			_mapper.Map(_repository.Get(id), quoteData);
			return quoteData;
		}

		[Route("property/{propertyId}")]
		public IEnumerable<QuoteData> GetByPropertyId(int propertyId)
		{
			var quoteData = new List<QuoteData>();
			_mapper.Map(_repository.Get(), quoteData);
			return
				quoteData.Where(x => x.PropertyId == propertyId).OrderBy(x => x.PropertyName).ThenByDescending(x => x.ContractYear);
		}


		public QuoteData Put([FromBody] QuoteData quoteData)
		{
			var quote = _mapper.Map(quoteData, new Quote());
			return _mapper.Map(_repository.Save(quote), quoteData);
		}

		[Route("new")]
		public QuoteData GetNew()
		{
			var quoteData = new QuoteData();
			_mapper.Map(_repository.New(), quoteData);
			return quoteData;
		}

		[Route("close")]
		public QuoteData PutClosedOrder([FromBody] QuoteData quoteData, [FromUri] bool createOrder)
		{
			var quote = _mapper.Map(quoteData, new Quote());

			if (createOrder)
			{
				// won
				var newOrder = _orderRepository.New();

				// create order from quote
				_mapper.Map(quoteData, newOrder);
				_orderRepository.Save(newOrder);

				// update quote status
				quote.Status = QuoteStatus.Won;
			}
			else
			{
				// lost
				quote.Status = QuoteStatus.Lost;
			}

			return _mapper.Map(_repository.Save(quote), quoteData);
		}
	}
}