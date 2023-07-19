using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduHome.App.Context
{
    public class EduHomeDbContext:DbContext
    {
        public DbSet<Slider> Slides { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<WelcomeEdu> WelcomeEdus { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherDegree> TeacherDegrees { get; set; }
        public DbSet<TeacherHobby> TeacherHobbies { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }
        public DbSet<TeacherSocial> TeacherSocials { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options):base(options)
        {
            
        }
    
    }
}
