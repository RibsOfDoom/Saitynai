using AutoMapper;
using L1_Zvejyba.Data.DTOs.Bodies;
using L1_Zvejyba.Data.DTOs.Cities;
using L1_Zvejyba.Data.Entities;
using L1_Zvejyba.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace L1_Zvejyba.Controllers
{
    [ApiController]
    [Route("api/cities/{cityName}/bodies")]
    public class BodiesController : ControllerBase
    {
        private readonly IBodyRepository _bodyRepository;
        private readonly IMapper _mapper;
        private readonly ICitiesRepository _cityRepository;

        public BodiesController(IBodyRepository bodyRepository, IMapper mapper, ICitiesRepository citiesRepository)
        {
            _bodyRepository = bodyRepository;
            _mapper = mapper;
            _cityRepository = citiesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<BodyDTO>> GetAllBodies(string cityName)
        {
            var bodies = await _bodyRepository.GetAll(cityName);

            return bodies.Select(o => _mapper.Map<BodyDTO>(o));
        }
        // api/cities/{cityname}/bodies/{bodyName}
        [HttpGet("{bodyName}")]
        public async Task<ActionResult<BodyDTO>> GetBody(string cityName, string bodyName)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityName, bodyName);

            if (body == null) return NotFound();

            return Ok(_mapper.Map<BodyDTO>(body));
        }

        [HttpPost]
        public async Task<ActionResult<BodyDTO>> Post(string cityName, CreateBodyDTO bodyDTO)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var body = _mapper.Map<Body>(bodyDTO);

            var existingBody = await _bodyRepository.Get(cityName, bodyDTO.Name);
            if (existingBody != null)
                return BadRequest($"A body named '{bodyDTO.Name}' already exists in city '{cityName}'.");

            body.cityName = cityName;

            await _bodyRepository.Add(body);

            return Created($"/api/cities/{cityName}/bodies/{body.Name}", _mapper.Map<BodyDTO>(body));
        }

        [HttpPut("{bodyDTO.Name}")]
        public async Task<ActionResult<BodyDTO>> Put(string cityName, UpdateBodyDTO bodyDTO)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var oldBody = await _bodyRepository.Get(cityName, bodyDTO.Name);

            if (oldBody == null) return NotFound();

            _mapper.Map(bodyDTO, oldBody);

            await _bodyRepository.Update(oldBody);

            return Ok(_mapper.Map<BodyDTO>(oldBody));
        }


        [HttpDelete("{bodyName}")]
        public async Task<ActionResult> Delete(string cityName, string bodyName)
        {
            var body = await _bodyRepository.Get(cityName, bodyName);

            if (body == null) return NotFound();

            await _bodyRepository.Remove(_mapper.Map<Body>(body));

            //204
            return NoContent();
        }
    }
}
