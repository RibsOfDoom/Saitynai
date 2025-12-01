using AutoMapper;
using L1_Zvejyba.Data.DTOs.Fish;
using L1_Zvejyba.Data.Entities;
using L1_Zvejyba.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace L1_Zvejyba.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/bodies/{bodyId}/fish")]
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
        public async Task<IEnumerable<FishDTO>> GetAllFish(int cityId, int bodyId)
        {
            var fish = await _fishRepository.GetAll(cityId, bodyId);

            return fish.Select(o => _mapper.Map<FishDTO>(o));
        }

        // api/cities/{cityname}/bodies/{bodyName}/fish/{fishId}
        [HttpGet("{fishId}")]
        public async Task<ActionResult<FishDTO>> GetFish(int cityId, int bodyId, int fishId)
        {
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityId, bodyId);

            if (body == null) return NotFound();

            var fish = await _fishRepository.Get(cityId, bodyId, fishId);

            if( fish == null) return NotFound();

            return Ok(_mapper.Map<FishDTO>(fish));
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<FishDTO>> Post(int cityId, int bodyId, CreateFishDTO fishDTO)
        {
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound("bad cityID");

            var body = await _bodyRepository.Get(cityId, bodyId);

            if (body == null) return NotFound("bad bodyID");

            var fish = _mapper.Map<Fish>(fishDTO);

            fish.bodyId = bodyId;

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if(userId == null) return Forbid();
            fish.UserId = userId;

            await _fishRepository.Add(fish);

            return Created($"/api/cities/{cityId}/bodies/{bodyId}/fish/{fish.Id}", _mapper.Map<FishDTO>(fish));
        }

        [Authorize(Roles = "User")]
        [HttpPut("{fishDTO.Id}")]
        public async Task<ActionResult<FishDTO>> Put(int cityId, int bodyId, UpdateFishDTO fishDTO)
        {
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityId, bodyId);

            if (body == null) return NotFound();

            var oldFish = await _fishRepository.Get(cityId, bodyId, fishDTO.Id);

            if (oldFish == null) return NotFound();

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Forbid();
            oldFish.lastModifiedBy = userId;

            _mapper.Map(fishDTO, oldFish);

            await _fishRepository.Update(oldFish);

            return Ok(_mapper.Map<FishDTO>(oldFish));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{fishId}")]
        public async Task<ActionResult> Delete(int cityId, int bodyId, int fishId)
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Unauthorized();

            var roles = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            if (!roles.Contains("Admin"))
            {
                return Forbid("You don’t have permission to perform this action.");
            }

            var fish = await _fishRepository.Get(cityId, bodyId, fishId);

            if (fish == null) return NotFound();

            await _fishRepository.Remove(_mapper.Map<Fish>(fish));

            //204
            return NoContent();
        }
    }
}
