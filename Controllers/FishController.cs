using AutoMapper;
using L1_Zvejyba.Data.DTOs.Fish;
using L1_Zvejyba.Data.Entities;
using L1_Zvejyba.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace L1_Zvejyba.Controllers
{
    [ApiController]
    [Route("api/cities/{cityName}/bodies/{bodyName}/fish")]
    public class FishController : ControllerBase
    {
        private readonly IBodyRepository _bodyRepository;
        private readonly IMapper _mapper;
        private readonly ICitiesRepository _cityRepository;
        private readonly IFishRepository _fishRepository;

        public FishController(IBodyRepository bodyRepository, IMapper mapper, ICitiesRepository citiesRepository, IFishRepository fishRepository)
        {
            _bodyRepository = bodyRepository;
            _mapper = mapper;
            _cityRepository = citiesRepository;
            _fishRepository = fishRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FishDTO>> GetAllFish(string cityName, string bodyName)
        {
            var fish = await _fishRepository.GetAll(cityName, bodyName);

            return fish.Select(o => _mapper.Map<FishDTO>(o));
        }

        // api/cities/{cityname}/bodies/{bodyName}/fish/{fishId}
        [HttpGet("{fishId}")]
        public async Task<ActionResult<FishDTO>> GetFish(string cityName, string bodyName, int fishId)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityName, bodyName);

            if (body == null) return NotFound();

            var fish = await _fishRepository.Get(cityName, bodyName, fishId);

            if( fish == null) return NotFound();

            return Ok(_mapper.Map<FishDTO>(fish));
        }

        [HttpPost]
        public async Task<ActionResult<FishDTO>> Post(string cityName, string bodyName, CreateFishDTO fishDTO)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityName, bodyName);

            if (body == null) return NotFound();

            var fish = _mapper.Map<Fish>(fishDTO);

            fish.bodyName = bodyName;

            await _fishRepository.Add(fish);

            return Created($"/api/cities/{cityName}/bodies/{bodyName}/fish/{fish.Id}", _mapper.Map<FishDTO>(fish));
        }

        [HttpPut("{fishDTO.Id}")]
        public async Task<ActionResult<FishDTO>> Put(string cityName, string bodyName, UpdateFishDTO fishDTO)
        {
            var city = await _cityRepository.Get(cityName);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityName, bodyName);

            if (body == null) return NotFound();

            var oldFish = await _fishRepository.Get(cityName, bodyName, fishDTO.Id);

            if (oldFish == null) return NotFound();

            _mapper.Map(fishDTO, oldFish);

            await _fishRepository.Update(oldFish);

            return Ok(_mapper.Map<FishDTO>(oldFish));
        }


        [HttpDelete("{fishId}")]
        public async Task<ActionResult> Delete(string cityName, string bodyName, int fishId)
        {
            var fish = await _fishRepository.Get(cityName, bodyName, fishId);

            if (fish == null) return NotFound();

            await _fishRepository.Remove(_mapper.Map<Fish>(fish));

            //204
            return NoContent();
        }
    }
}
