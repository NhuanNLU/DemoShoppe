using Microsoft.EntityFrameworkCore;

namespace DemoShoppe.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
    }
}
