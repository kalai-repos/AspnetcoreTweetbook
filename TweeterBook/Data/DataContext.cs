using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TweeterBook.Domain;

namespace TweeterBook.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<EmployeeTag> EmployeeTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            
            base.OnModelCreating(builder);

            builder.Entity<EmployeeTag>().Ignore(xx => xx.Employee).HasKey(x => new { x.EmpId, x.TagName });
        }
    }
}
