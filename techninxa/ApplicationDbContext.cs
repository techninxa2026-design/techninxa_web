using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Techninxa.Models;
using techninxa.Models;

namespace Techninxa;

public class ApplicationDbContext : DbContext
{
	public DbSet<TeamMember> TeamMembers { get; set; }

	public DbSet<ChatMessage> ChatMessages { get; set; }

	public DbSet<Meeting> Meetings { get; set; }

	public DbSet<TimeSlot> TimeSlots { get; set; }

	public DbSet<ContactMessage> ContactMessages { get; set; }

	public DbSet<AdminUser> User { get; set; }

	public DbSet<Project> Projects { get; set; }

	public DbSet<ProjectMedia> ProjectMedia { get; set; }

	public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }

	public DbSet<ProjectFeature> ProjectFeatures { get; set; }

	public DbSet<HolidaySetting> HolidaySettings { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base((DbContextOptions)(object)options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Project>().HasMany<ProjectMedia>((Expression<Func<Project, IEnumerable<ProjectMedia>>>)((Project p) => p.Media)).WithOne((Expression<Func<ProjectMedia, Project>>)((ProjectMedia m) => m.Project))
			.HasForeignKey((Expression<Func<ProjectMedia, object>>)((ProjectMedia m) => m.ProjectId))
			.OnDelete((DeleteBehavior)3);
		modelBuilder.Entity<Project>().HasMany<ProjectTechnology>((Expression<Func<Project, IEnumerable<ProjectTechnology>>>)((Project p) => p.Technologies)).WithOne((Expression<Func<ProjectTechnology, Project>>)((ProjectTechnology t) => t.Project))
			.HasForeignKey((Expression<Func<ProjectTechnology, object>>)((ProjectTechnology t) => t.ProjectId))
			.OnDelete((DeleteBehavior)3);
		modelBuilder.Entity<Project>().HasMany<ProjectFeature>((Expression<Func<Project, IEnumerable<ProjectFeature>>>)((Project p) => p.Features)).WithOne((Expression<Func<ProjectFeature, Project>>)((ProjectFeature f) => f.Project))
			.HasForeignKey((Expression<Func<ProjectFeature, object>>)((ProjectFeature f) => f.ProjectId))
			.OnDelete((DeleteBehavior)3);
		modelBuilder.Entity<Project>().HasOne<Project>((Expression<Func<Project, Project>>)((Project p) => p.NextProject)).WithMany((string)null)
			.HasForeignKey((Expression<Func<Project, object>>)((Project p) => p.NextProjectId))
			.OnDelete((DeleteBehavior)5);
	}
}
