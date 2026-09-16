using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Data
{
    public class StudentDbContext:DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students {  get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

    }
}
