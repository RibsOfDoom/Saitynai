using L1_Zvejyba.Data.DTOs;
using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace L1_Zvejyba.Data.Repositories
{
    public interface ICitiesRepository
    {
        Task<City> Create(City city);
        Task Delete(City city);
        Task<City> Get(int id);
        Task<IEnumerable<City>> GetAll();
        Task<City> Put(City city);
    }

    public class CitiesRepository : ICitiesRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public CitiesRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }
        public async Task<IEnumerable<City>> GetAll()
        {
            return await _demoRestContext.Cities.ToListAsync();
        }

        public async Task<City> Get(int id)
        {
            return await _demoRestContext.Cities.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<City> Put(City city)
        {
            _demoRestContext.Cities.Update(city);
            await _demoRestContext.SaveChangesAsync();

            return city;
        }

        public async Task<City> Create(City city)
        {
            _demoRestContext.Cities.Add(city);
            await _demoRestContext.SaveChangesAsync();

            return city;
        }

        public async Task Delete(City city)
        {
            _demoRestContext.Remove(city);
            await _demoRestContext.SaveChangesAsync();
        }
    }
}
