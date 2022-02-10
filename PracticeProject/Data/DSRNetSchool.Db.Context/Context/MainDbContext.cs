using Microsoft.EntityFrameworkCore;

namespace DSRNetSchool.Db.Context.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
