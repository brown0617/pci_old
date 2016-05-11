using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IQuoteRepository : IRepository<Quote, int>
	{
		Quote Close(Quote quote, bool createOrder);
	}
}