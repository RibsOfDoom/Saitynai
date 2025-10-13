using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace L1_Zvejyba.Data.Repositories
{
    public interface IFishRepository
    {
        Task Add(Fish fish);
        Task<Fish> Get(string cityName, string bodyName, int fishId);
        Task<IEnumerable<Fish>> GetAll(string cityName, string bodyName);
        Task Remove(Fish fish);
        Task Update(Fish fish);
    }

    public class FishRepository : IFishRepository
    {
        private readonly DemoRestContext _demoRestContext;

        public FishRepository(DemoRestContext demoRestContext)
        {
            _demoRestContext = demoRestContext;
        }



        public async Task<Fish> Get(string cityName, string bodyName, int fishId)
        {
            return await _demoRestContext.Fish.Where(o => o.Id == fishId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Fish>> GetAll(string cityName, string bodyName)
        {
            return await _demoRestContext.Fish.Where(o => o.body.Name == bodyName).ToListAsync();
        }

        public async Task Add(Fish fish)
        {
            _demoRestContext.Fish.Add(fish);
            await _demoRestContext.SaveChangesAsync();
            
        }

        public async Task Update(Fish fish)
        {
            _demoRestContext.Fish.Update(fish);
            await _demoRestContext.SaveChangesAsync();
        }

        public async Task Remove(Fish fish)
        {
            _demoRestContext.Fish.Remove(fish);
            await _demoRestContext.SaveChangesAsync();
        }
    }
}
