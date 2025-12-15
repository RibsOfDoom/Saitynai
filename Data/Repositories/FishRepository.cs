using L1_Zvejyba.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace L1_Zvejyba.Data.Repositories
{
    public interface IFishRepository
    {
        Task Add(Fish fish);
        Task<Fish> Get(int cityId, int bodyId, int fishId);
        Task<IEnumerable<Fish>> GetAll(int cityId, int bodyId);
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



        /// <summary>
        /// Doesnt check cityID rn, it should though, so does getall
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="bodyId"></param>
        /// <param name="fishId"></param>
        /// <returns></returns>
        public async Task<Fish> Get(int cityId, int bodyId, int fishId)
        {
            return await _demoRestContext.Fish.Where(o => o.Id == fishId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Fish>> GetAll(int cityId, int bodyId)
        {
            return await _demoRestContext.Fish.Where(o => o.body.Id == bodyId).ToListAsync();
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
