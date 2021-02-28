using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Proiect_Tudose_Baias.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public ICollection<Order> Orders { get; set; }
        public ICollection<Request> Requested { get; set; }
        public ICollection<Request> Received { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext,
            //    Proiect_Tudose_Baias.Migrations.Configuration>("DefaultConnection"));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Review> Reviews { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Product>()
            //    .HasRequired(x => x.Request)
            //    .WithOptional(x => x.Product);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Requested)
                .WithRequired(x => x.Colab);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Received)
                .WithOptional(x => x.Admin)
                .WillCascadeOnDelete(false);
        }

    }
}