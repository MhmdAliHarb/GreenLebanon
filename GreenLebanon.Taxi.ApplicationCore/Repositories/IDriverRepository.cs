using GreenLebanon.Taxi.ApplicationCore.Entities;

namespace GreenLebanon.Taxi.ApplicationCore.Repositories
{
    public interface IDriverRepository
    {
        Task<IQueryable<ApplicationUser>> GetAllDriversAsync(string driverId = null);

        Task<Driver> GetAvailableDriverAsync(string region);
    }
}
