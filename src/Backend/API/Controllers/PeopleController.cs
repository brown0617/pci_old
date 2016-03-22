using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	public class PeopleController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IPersonRepository _repository;

		public PeopleController(IPersonRepository personRepository, IMapper mapper)
		{
			_repository = personRepository;
			_mapper = mapper;
		}

		public IEnumerable<PersonData> Get()
		{
			var personData = new List<PersonData>();
			_mapper.Map(_repository.Get(), personData);
			return personData.OrderBy(x => x.LastName).ThenBy(y => y.FirstName);
		}

		public PersonData Get(int id)
		{
			var personData = new PersonData();
			_mapper.Map(_repository.Get(id), personData);
			return personData;
		}

		public PersonData New()
		{
			var personData = new PersonData();
			_mapper.Map(_repository.New(), personData);
			return personData;
		}

		public void Put([FromBody] PersonData personData)
		{
			var person = _mapper.Map(personData, new Person());
			_repository.Save(person);
		}
	}
}