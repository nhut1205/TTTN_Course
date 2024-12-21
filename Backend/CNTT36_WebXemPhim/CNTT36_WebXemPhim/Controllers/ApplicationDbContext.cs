using Microsoft.EntityFrameworkCore;
namespace CNTT36_WebXemPhim.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
