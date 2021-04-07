using Microsoft.EntityFrameworkCore;
using AndreyYahooService;

namespace AndreyYahooService.Data
{
    public class ToUsdContext : DbContext
    {
        public ToUsdContext(DbContextOptions<ToUsdContext> options)
            : base(options)
        {
        }

        public DbSet<RateToUsd> Users { get; set; }

    }
}