using Microsoft.EntityFrameworkCore;
using PlatformWellAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformWellAssessment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Well> Wells { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

            return base.SaveChanges();
        }

        public void SetModified(object entity)
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public void SetDeleted(object entity)
        {
            this.Entry(entity).State = EntityState.Deleted;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<Platform>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).ValueGeneratedNever();
            });

            builder.Entity<Well>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Id).ValueGeneratedNever();
            });
        }
    }
}