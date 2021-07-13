using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<JobReferenceJobType>().HasNoKey();
        //}
        public DbSet<Image> Images { get; set; }
        public DbSet<JobReference> JobReferences { get; set; }
        public DbSet<JobReferenceJobType> JobReferenceJobTypes { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
    }
}