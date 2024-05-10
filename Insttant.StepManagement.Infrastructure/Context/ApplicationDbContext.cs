using Insttantt.StepManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insttantt.StepManagement.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Step> step { get; set; }
        public virtual DbSet<StepFields> stepFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Step>().ToTable("Step");
            modelBuilder.Entity<StepFields>().ToTable("StepFields");
        }
    }
}
