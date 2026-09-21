using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Api.Models;

namespace StudentManagementSystem.Api.Data
{
    public class StudentDbContext:IdentityDbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students {  get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Mark> Marks { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

    }
}
