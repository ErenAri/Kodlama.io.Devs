using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistance.Contexts
{
    public class BaseDbContexts : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Account> Accounts { get; set; }


        public BaseDbContexts(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Accounts);

            });

            modelBuilder.Entity<Account>(a =>
            {
                a.ToTable("Accounts").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.UserId).HasColumnName("UserId");
                a.Property(p => p.ProfileUrl).HasColumnName("ProfilUrl");
                a.HasOne(p => p.Language);

            });

            Language[] languageEntitySeeds = { new(1, "Java"), new(2, "C#"), new(3, "Kotlin") };
            modelBuilder.Entity<Language>().HasData(languageEntitySeeds);


            Account[] accountEntitySeeds =
            {
                new(1,1,"https://github.com/ErenAri"),
            };

            modelBuilder.Entity<Account>().HasData(accountEntitySeeds);
        }
    }
}
