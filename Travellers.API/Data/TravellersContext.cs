using Microsoft.EntityFrameworkCore;
using Travellers.API.Model;

namespace Travellers.API.Data
{

    public class TravellersContext : DbContext
    {
        public TravellersContext (DbContextOptions<TravellersContext> options)
            : base(options)
        {
        }

        public DbSet<TravellerValue> TravellerValue { get; set; }
    }
}
