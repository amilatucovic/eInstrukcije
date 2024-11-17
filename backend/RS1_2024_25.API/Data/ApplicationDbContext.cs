using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data.Models;
using RS1_2024_25.API.Data.Models.Auth;

namespace RS1_2024_25.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<MyAppUser> MyAppUsers { get; set; }
        public DbSet<MyAuthenticationToken> MyAuthenticationTokens { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<MyAppUser>()
                        .HasDiscriminator<string>("UserType")
                        .HasValue<MyAppUser>("User")
                        .HasValue<Admin>("Admin")
                        .HasValue<Tutor>("Tutor")
                        .HasValue<Student>("Student");

           
            modelBuilder.Entity<Review>()
                .HasKey(r => r.ID);

           
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Reviews) 
                .HasForeignKey(r => r.StudentID)
                .OnDelete(DeleteBehavior.NoAction); 

           
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Tutor)
                .WithMany(t => t.Reviews) 
                .HasForeignKey(r => r.TutorID)
                .OnDelete(DeleteBehavior.NoAction);

            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

           
        }
    }
}
