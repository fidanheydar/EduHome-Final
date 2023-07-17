using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Context
{
    public class EduHomeDbContext:DbContext
    {
        public DbSet<Slider> Slides { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<WelcomeEdu> WelcomeEdus { get; set; }
        public DbSet<Person> People { get; set; }
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options):base(options)
        {
            
        }
    }
}
