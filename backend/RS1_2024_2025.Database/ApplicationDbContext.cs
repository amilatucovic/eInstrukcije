using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RS1_2024_2025.Database.Seed;
using RS1_2024_2025.Domain.Entities;
using RS1_2024_2025.Domain.Entities.Models.Auth;

namespace RS1_2024_2025.Database
{
	public partial class ApplicationDbContext : DbContext
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
		public DbSet<TutorSubject> TutorsSubjects { get; set; }
		public DbSet<StudentSubject> StudentsSubjects { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Lesson> Lessons { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<SubjectCategory> SubjectsCategories { get; set; }
		public DbSet<ReservationPayment> ReservationsPayments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);



			modelBuilder.Entity<MyAppUser>()
											 .Property(e => e.UserType)
											 .HasConversion(
											  v => v.ToString(),
											  v => (UserType)Enum.Parse(typeof(UserType), v));


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

			modelBuilder.Entity<TutorSubject>()
										.HasKey(ts => new { ts.TutorID, ts.SubjectID });

			modelBuilder.Entity<TutorSubject>()
				.HasOne(ts => ts.Tutor)
				.WithMany(t => t.TutorSubjects)
				.HasForeignKey(ts => ts.TutorID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<TutorSubject>()
				.HasOne(ts => ts.Subject)
				.WithMany(s => s.TutorSubjects)
				.HasForeignKey(ts => ts.SubjectID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<StudentSubject>()
												 .HasKey(ss => new { ss.StudentID, ss.SubjectID });


			modelBuilder.Entity<StudentSubject>()
				.HasOne(ss => ss.Student)
				.WithMany(s => s.StudentSubjects)
				.HasForeignKey(ss => ss.StudentID)
				.OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<StudentSubject>()
				.HasOne(ss => ss.Subject)
				.WithMany(sub => sub.StudentSubjects)
				.HasForeignKey(ss => ss.SubjectID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<SubjectCategory>().HasKey(ss => new { ss.SubjectID, ss.CategoryID });

			modelBuilder.Entity<SubjectCategory>()
												  .HasOne(sc => sc.Subject)
												  .WithMany(s => s.SubjectCategories)
												  .HasForeignKey(sc => sc.SubjectID)
												  .OnDelete(DeleteBehavior.Cascade);


			modelBuilder.Entity<SubjectCategory>()
				.HasOne(sc => sc.Category)
				.WithMany(c => c.SubjectCategories)
				.HasForeignKey(sc => sc.CategoryID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Attendance>()
											 .HasKey(a => new { a.LessonID, a.StudentID });


			modelBuilder.Entity<Attendance>()
				.HasOne(a => a.Lesson)
				.WithMany(l => l.Attendances)
				.HasForeignKey(a => a.LessonID)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Attendance>()
				.HasOne(a => a.Student)
				.WithMany(s => s.Attendances)
				.HasForeignKey(a => a.StudentID)
				.OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Message>()
								 .HasOne(m => m.Sender)
								 .WithMany(m => m.SentMessages)
								 .HasForeignKey(m => m.SenderID)
								 .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany(m => m.ReceivedMessages)
				.HasForeignKey(m => m.ReceiverID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Reservation>(entity =>
			{
				entity.HasKey(r => r.ID);
				entity.HasOne(r => r.Student)
				.WithMany(s => s.Reservations)
				.HasForeignKey(r => r.StudentID)
				.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(r => r.Tutor)
				.WithMany(t => t.Reservations)
				.HasForeignKey(r => r.TutorID)
				.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(r => r.Lesson)
						.WithOne(l => l.Reservation)
						.HasForeignKey<Reservation>(r => r.LessonID)
						.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<ReservationPayment>(entity =>
			{
				entity.HasKey(rp => rp.ID);


				entity.HasOne(rp => rp.Reservation)
					  .WithMany(r => r.ReservationPayments)
					  .HasForeignKey(rp => rp.ReservationID)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.Property(rp => rp.TutorExpencesAmount)
								.HasColumnType("decimal(18, 2)");

				entity.Property(rp => rp.Amount)
					  .IsRequired()
					  .HasColumnType("decimal(18, 2)");

				entity.Property(rp => rp.PaymentDate)
					  .IsRequired();
			});

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.NoAction;
			}

			modelBuilder.Entity<City>().SeedData();
			modelBuilder.Entity<MyAppUser>().SeedData();
			modelBuilder.Entity<Student>().SeedData();
			modelBuilder.Entity<Tutor>().SeedData();
			modelBuilder.Entity<Admin>().SeedData();
			modelBuilder.Entity<Subject>().SeedData();
			modelBuilder.Entity<Category>().SeedData();
			modelBuilder.Entity<SubjectCategory>().SeedData();
			modelBuilder.Entity<TutorSubject>().SeedData();
			modelBuilder.Entity<StudentSubject>().SeedData();
			modelBuilder.Entity<Lesson>().SeedData();

			OnModelCreatingPartial(modelBuilder);
				
	}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
