using AutoMapper;
using L1_Zvejyba.Data.DTOs.Bodies;
using L1_Zvejyba.Data.DTOs.Cities;
using L1_Zvejyba.Data.Entities;
using L1_Zvejyba.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace L1_Zvejyba.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/bodies")]
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
        public async Task<IEnumerable<BodyDTO>> GetAllBodies(int cityId)
        {
            var bodies = await _bodyRepository.GetAll(cityId);

            return bodies.Select(o => _mapper.Map<BodyDTO>(o));
        }
        // api/cities/{cityname}/bodies/{bodyName}
        [HttpGet("{bodyId}")]
        public async Task<ActionResult<BodyDTO>> GetBody(int cityId, int bodyId)
        {
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound();

            var body = await _bodyRepository.Get(cityId, bodyId);

            if (body == null) return NotFound();

            return Ok(_mapper.Map<BodyDTO>(body));
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<BodyDTO>> Post(int cityId, CreateBodyDTO bodyDTO)
        {

            //return BadRequest("Shit hit the fan");
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound("City matching ID was not found");

            var body = _mapper.Map<Body>(bodyDTO);

            var existingBody = await _bodyRepository.Get(cityId, bodyDTO.Id);
            if (existingBody != null)
                return BadRequest($"A body named '{bodyDTO.Id}' already exists in city '{cityId}'.");

            body.cityId = cityId;

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Forbid();
            body.UserId = userId;

            await _bodyRepository.Add(body);

            return Created($"/api/cities/{cityId}/bodies/{body.Id}", _mapper.Map<BodyDTO>(body));
        }

        [Authorize(Roles = "User")]
        [HttpPut("{bodyId}")]
        public async Task<ActionResult<BodyDTO>> Put(int cityId, int bodyId, UpdateBodyDTO bodyDTO)
        {
            var city = await _cityRepository.Get(cityId);

            if (city == null) return NotFound("Bad cityID");

            var oldBody = await _bodyRepository.Get(cityId, bodyId);

            if (oldBody == null) return NotFound("Bad bodyId");

            _mapper.Map(bodyDTO, oldBody);

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Forbid();
            oldBody.lastModifiedBy = userId;

            await _bodyRepository.Update(oldBody);

            return Ok(_mapper.Map<BodyDTO>(oldBody));
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{bodyId}")]
        public async Task<ActionResult> Delete(int cityId, int bodyId)
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

            var body = await _bodyRepository.Get(cityId, bodyId);

            if (body == null) return NotFound();

            await _bodyRepository.Remove(_mapper.Map<Body>(body));

            //204
            return NoContent();
        }
    }
}
