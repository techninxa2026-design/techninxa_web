using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Techninxa;

namespace techninxa.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20261008102844_teammembers")]
public class teammembers : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable("TeamMembers", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			int? num = 150;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("nvarchar(150)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 100;
			OperationBuilder<AddColumnOperation> department = table.Column<string>("nvarchar(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 200;
			OperationBuilder<AddColumnOperation> role = table.Column<string>("nvarchar(200)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			OperationBuilder<AddColumnOperation> image = table.Column<string>("nvarchar(500)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			return new
			{
				Id = id,
				Name = name,
				Department = department,
				Role = role,
				Image = image,
				AltText = table.Column<string>("nvarchar(500)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				DisplayOrder = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				IsActive = table.Column<bool>("bit", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_TeamMembers", x => (object)x.Id);
		}, (string)null);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("TeamMembers", (string)null);
	}

	protected override void BuildTargetModel(ModelBuilder modelBuilder)
	{
		modelBuilder.HasAnnotation("ProductVersion", (object)"10.0.1").HasAnnotation("Relational:MaxIdentifierLength", (object)128);
		SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);
		modelBuilder.Entity("Techninxa.Models.ContactMessage", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Email").IsRequired(true).HasMaxLength(150), "nvarchar(150)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Message").IsRequired(true).HasMaxLength(2000), "nvarchar(2000)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("ProjectType"), "int");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "ContactMessages");
		}));
		modelBuilder.Entity("Techninxa.Models.Project", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Category").IsRequired(true).HasMaxLength(50), "nvarchar(50)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Client").HasMaxLength(150), "nvarchar(150)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("HeroImage").HasMaxLength(500), "nvarchar(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<int?>(b.Property<int?>("NextProjectId"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Overview"), "nvarchar(max)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ProjectUrl").HasMaxLength(500), "nvarchar(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime?>(b.Property<DateTime?>("PublishedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ShortDescription").HasMaxLength(300), "nvarchar(300)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Slug").IsRequired(true).HasMaxLength(200), "nvarchar(200)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Status").IsRequired(true).HasMaxLength(20), "nvarchar(20)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Title").IsRequired(true).HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime?>(b.Property<DateTime?>("UpdatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<int?>(b.Property<int?>("Year"), "int");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "NextProjectId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Projects");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectFeature", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Description"), "nvarchar(max)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("DisplayOrder"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("ProjectId"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Title").IsRequired(true).HasMaxLength(150), "nvarchar(150)");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "ProjectId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "ProjectFeatures");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectMedia", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ContentType").HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("DisplayOrder"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("FilePath").IsRequired(true).HasMaxLength(500), "nvarchar(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<long>(b.Property<long>("FileSize"), "bigint");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("MediaType").IsRequired(true).HasMaxLength(20), "nvarchar(20)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("OriginalFileName").HasMaxLength(255), "nvarchar(255)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("ProjectId"), "int");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "ProjectId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "ProjectMedia");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectTechnology", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("DisplayOrder"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("ProjectId"), "int");
			b.HasKey(new string[1] { "Id" });
			b.HasIndex(new string[1] { "ProjectId" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "ProjectTechnologies");
		}));
		modelBuilder.Entity("techninxa.Models.AdminUser", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Password").IsRequired(true), "nvarchar(max)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Username").IsRequired(true).HasMaxLength(150), "nvarchar(150)");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "User");
		}));
		modelBuilder.Entity("techninxa.Models.ChatMessage", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Message").IsRequired(true), "nvarchar(max)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("SentAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Username").IsRequired(true), "nvarchar(max)");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "ChatMessages");
		}));
		modelBuilder.Entity("techninxa.Models.HolidaySetting", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Day"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsHoliday"), "bit");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "HolidaySettings");
		}));
		modelBuilder.Entity("techninxa.Models.Meeting", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Email").IsRequired(true).HasMaxLength(150), "nvarchar(150)");
			RelationalPropertyBuilderExtensions.HasColumnType<TimeSpan>(b.Property<TimeSpan>("EndTime"), "time");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsCancelled"), "bit");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsRead"), "bit");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("MeetingDate"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Message").HasMaxLength(1000), "nvarchar(1000)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("ProjectType").HasMaxLength(50), "nvarchar(50)");
			RelationalPropertyBuilderExtensions.HasColumnType<TimeSpan>(b.Property<TimeSpan>("StartTime"), "time");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "Meetings");
		}));
		modelBuilder.Entity("techninxa.Models.TeamMember", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("AltText").HasMaxLength(500), "nvarchar(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Department").IsRequired(true).HasMaxLength(100), "nvarchar(100)");
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("DisplayOrder"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Image").HasMaxLength(500), "nvarchar(500)");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsActive"), "bit");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Name").IsRequired(true).HasMaxLength(150), "nvarchar(150)");
			RelationalPropertyBuilderExtensions.HasColumnType<string>(b.Property<string>("Role").IsRequired(true).HasMaxLength(200), "nvarchar(200)");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "TeamMembers");
		}));
		modelBuilder.Entity("techninxa.Models.TimeSlot", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<DateTime>(b.Property<DateTime>("CreatedAt"), "datetime2");
			RelationalPropertyBuilderExtensions.HasColumnType<TimeSpan>(b.Property<TimeSpan>("EndTime"), "time");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsActive"), "bit");
			RelationalPropertyBuilderExtensions.HasColumnType<TimeSpan>(b.Property<TimeSpan>("StartTime"), "time");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "TimeSlots");
		}));
		modelBuilder.Entity("Techninxa.Models.Project", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Techninxa.Models.Project", "NextProject").WithMany((string)null).HasForeignKey(new string[1] { "NextProjectId" })
				.OnDelete((DeleteBehavior)5);
			b.Navigation("NextProject");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectFeature", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Techninxa.Models.Project", "Project").WithMany("Features").HasForeignKey(new string[1] { "ProjectId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("Project");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectMedia", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Techninxa.Models.Project", "Project").WithMany("Media").HasForeignKey(new string[1] { "ProjectId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("Project");
		}));
		modelBuilder.Entity("Techninxa.Models.ProjectTechnology", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.HasOne("Techninxa.Models.Project", "Project").WithMany("Technologies").HasForeignKey(new string[1] { "ProjectId" })
				.OnDelete((DeleteBehavior)3)
				.IsRequired(true);
			b.Navigation("Project");
		}));
		modelBuilder.Entity("Techninxa.Models.Project", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			b.Navigation("Features");
			b.Navigation("Media");
			b.Navigation("Technologies");
		}));
	}
}
