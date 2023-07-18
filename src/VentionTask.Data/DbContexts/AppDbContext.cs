using Microsoft.EntityFrameworkCore;
using Ventiontask.Domain.Entities;

namespace Ventiontask.Data.DbContexts;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<Subject> Subjects { get; set; }
	public DbSet<GroupSubject> GroupSubjects { get; set; }

	protected override void OnModelCreating( ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Group>()
			.HasMany(e => e.Users)
			.WithOne(e => e.Group)
			.HasForeignKey(e => e.GroupId);

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Group)
            .WithMany(g => g.GroupSubjects)
            .HasForeignKey(gs => gs.GroupId);

        modelBuilder.Entity<GroupSubject>()
            .HasOne(gs => gs.Subject)
            .WithMany(s => s.GroupSubjects)
            .HasForeignKey(gs => gs.SubjectId);

		modelBuilder.Entity<Group>().HasData(
			new Group() { Id = 1, Name = "10-a", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Group() { Id = 2, Name = "10-b", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Group() { Id = 3, Name = "10-c", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Group() { Id = 4, Name = "11-a", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Group() { Id = 5, Name = "11-a", CreatedAt = DateTime.UtcNow, UpdatedAt = null }
			);

		modelBuilder.Entity<User>().HasData(
			new User() { Id = 1, FirstName = "Komron", LastName = "Rakhmonov", Phone = "+998910082052", Password = "12345", GroupId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new User() { Id = 2, FirstName = "Diyor", LastName = "Rakhmonov", Phone = "+998910082052", Password = "12345", GroupId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new User() { Id = 3, FirstName = "Sokhib", LastName = "Rakhmonov", Phone = "+998910082052", Password = "12345", GroupId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = null }
			);

		modelBuilder.Entity<Subject>().HasData(
			new Subject() { Id = 1, Name = "History", Description = "For adults", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Subject() { Id = 2, Name = "Math", Description = "For adults", CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new Subject() { Id = 3, Name = "English", Description = "For adults", CreatedAt = DateTime.UtcNow, UpdatedAt = null }
			);

		modelBuilder.Entity<GroupSubject>().HasData(
			new GroupSubject() { Id = 1, GroupId = 1, SubjectId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new GroupSubject() { Id = 2, GroupId = 2, SubjectId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = null },
			new GroupSubject() { Id = 3, GroupId = 2, SubjectId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = null }
			);
    }

}
