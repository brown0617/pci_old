using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	[RoutePrefix("api/materials")]
	public class MaterialController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IMaterialRepository _repository;

		public MaterialController(IMaterialRepository materialRepository, IMapper mapper)
		{
			_repository = materialRepository;
			_mapper = mapper;
		}

		[Route("{materialName}")]
		public IEnumerable<MaterialData> GetByName(string materialName)
		{
			var materialData = new List<MaterialData>();
			_mapper.Map(_repository.FilterByName(materialName), materialData);
			return materialData;
		}
	}
}