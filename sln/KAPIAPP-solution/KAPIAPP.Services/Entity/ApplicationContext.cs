using KAPIAPP.Services.Entity.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace KAPIAPP.Services.Entity
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Boutique> Boutiques { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

           // model.ApplyConfiguration(new EvaluateurSeed());
            model.ApplyConfiguration(new BoutiqueSeed());
            model.ApplyConfiguration(new RoleSeed());

            model.Entity<Evaluation>()
                //.HasKey(k => new { k.BoutiqueId, k.UserId });
                .HasKey(k => k.Id);

            model.Entity<Evaluation>()
                .HasOne(u => u.User)
                .WithMany(e => e.Evaluations)
                .HasForeignKey(fk => fk.UserId)               
                .OnDelete(DeleteBehavior.Restrict);

            model.Entity<Evaluation>()
                .HasOne(u => u.Boutique)
                .WithMany(e => e.Evaluations)                
                .HasForeignKey(fk => fk.BoutiqueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
