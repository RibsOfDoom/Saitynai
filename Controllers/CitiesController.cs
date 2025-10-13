using L1_Zvejyba.Data.Repositories;
using L1_Zvejyba.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using L1_Zvejyba.Data.DTOs.Cities;
using L1_Zvejyba.Data.DTOs.Bodies;

namespace L1_Zvejyba.Controllers
{
    /*
    cities
    /api/cities GET ALL 200
    /api/cities/{name} GET 200
    /api/cities POST 200
    /api/cities/{name} PUT 200
    /api/cities/{name} DELETE 200/204
     */
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesRepository _citiesRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICitiesRepository citiesRepository, IMapper mapper)
        {
            _citiesRepository = citiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CityDTO>> GetAll()
        {
            return (await _citiesRepository.GetAll()).Select(o => _mapper.Map<CityDTO>(o));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityDTO>> Get(string name)
        {
            var city = await _citiesRepository.Get(name);
            if (city == null) return NotFound($"City by the name of {name} was not found.");

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> Post(CreateCityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);

            await _citiesRepository.Create(city);

            /// 201 Created City
            return Created($"/api/cities/{city.Name}", _mapper.Map<CityDTO>(city));
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<CityDTO>> Put(string name, UpdateCityDTO cityDTO)
        {
            var city = await _citiesRepository.Get(name);
            if (city == null) return NotFound($"City by the name of {name} was not found.");

            city.Description = cityDTO.Description;

            await _citiesRepository.Put(city);

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<CityDTO>> Delete(string name)
        {
            var city = await _citiesRepository.Get(name);
            if (city == null) return NotFound($"City by the name of {name} was not found.");

            await _citiesRepository.Delete(city);

            //204
            return NoContent();
        }
    }
}
