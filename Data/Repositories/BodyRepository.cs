using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace L1_Zvejyba.Data.Repositories
{
    public interface IBodyRepository
    {
        Task Add(Body body);
        Task<Body> Get(string cityName, string bodyName);
        Task<IEnumerable<Body>> GetAll(string cityName);
        Task Remove(Body body);
        Task Update(Body body);
    }

    public class BodyRepository : IBodyRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public BodyRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }



        public async Task<Body> Get(string cityName, string bodyName)
        {
            return await _demoRestContext.Bodies.FirstOrDefaultAsync(o => o.city.Name == cityName && o.Name == bodyName);
        }

        public async Task<IEnumerable<Body>> GetAll(string cityName)
        {
            return await _demoRestContext.Bodies.Where(o => o.city.Name == cityName).ToListAsync();
        }

        public async Task Add(Body body)
        {
            _demoRestContext.Bodies.Add(body);
            await _demoRestContext.SaveChangesAsync();
        }

        public async Task Update(Body body)
        {
            _demoRestContext.Bodies.Update(body);
            await _demoRestContext.SaveChangesAsync();
        }

        public async Task Remove(Body body)
        {
            _demoRestContext.Bodies.Remove(body);
            await _demoRestContext.SaveChangesAsync();
        }
    }
}
