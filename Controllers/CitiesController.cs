using AutoMapper;
using L1_Zvejyba.Data.Auth.Model;
using L1_Zvejyba.Data.DTOs.Bodies;
using L1_Zvejyba.Data.DTOs.Cities;
using L1_Zvejyba.Data.Entities;
using L1_Zvejyba.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> Get(int id)
        {
            var city = await _citiesRepository.Get(id);
            if (city == null) return NotFound($"City by the id of {id} was not found.");

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CityDTO>> Post(CreateCityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Unauthorized();

            var roles = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            if (!roles.Contains("Admin"))
            {
                return Forbid("You don’t have permission to perform this action.");
            }

            city.UserId = userId;


            var existingCity = await _citiesRepository.Get(cityDTO.Id);
            if (existingCity != null)
                return BadRequest($"A city named '{cityDTO.Name}' already exists.");

            await _citiesRepository.Create(city);

            /// 201 Created City
            return Created($"/api/cities/{city.Id}", _mapper.Map<CityDTO>(city));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CityDTO>> Put(int id, UpdateCityDTO cityDTO)
        {
            var city = await _citiesRepository.Get(id);
            if (city == null) return NotFound($"City by the id of {id} was not found.");

            city.Description = cityDTO.Description;

            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null) return Unauthorized();
            city.lastModifiedBy = userId;

            var roles = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            if (!roles.Contains("Admin"))
            {
                return Forbid("You don’t have permission to perform this action.");
            }

            await _citiesRepository.Put(city);

            return Ok(_mapper.Map<CityDTO>(city));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CityDTO>> Delete(int id)
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

            var city = await _citiesRepository.Get(id);
            if (city == null) return NotFound($"City by the name of {id} was not found.");

            await _citiesRepository.Delete(city);

            //204
            return NoContent();
        }
    }
}
