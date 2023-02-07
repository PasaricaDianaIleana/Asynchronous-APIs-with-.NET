using AsynchronousAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsynchronousAPI.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ListingRequests> ListingRequests => Set<ListingRequests>();
    }
}
