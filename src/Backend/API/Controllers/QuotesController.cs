using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[RoutePrefix("api/quotes")]
	[ExceptionHandling]
	public class QuotesController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IQuoteRepository _repository;

		public QuotesController(IQuoteRepository quoteRepository, IMapper mapper)
		{
			_repository = quoteRepository;
			_mapper = mapper;
		}

		public IEnumerable<QuoteData> Get()
		{
			var quoteData = new List<QuoteData>();
			_mapper.Map(_repository.Get(), quoteData);
			return quoteData.OrderBy(x => x.PropertyName).ThenByDescending(x => x.ContractYear);
		}

		public QuoteData Get(int id)
		{
			var quoteData = new QuoteData();
			_mapper.Map(_repository.Get(id), quoteData);
			return quoteData;
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
		public QuoteData Close([FromBody] QuoteData quoteData, [FromUri] bool createOrder)
		{
			if (createOrder)
			{
				// create order with work orders
			}
			var quote = _mapper.Map(quoteData, new Quote());
			return _mapper.Map(_repository.Save(quote), quoteData);
		}
	}
}