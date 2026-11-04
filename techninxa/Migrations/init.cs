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
[Migration("20260926050121_init")]
public class init : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable("ContactMessages", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			int? num = 100;
			OperationBuilder<AddColumnOperation> name = table.Column<string>("nvarchar(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 150;
			OperationBuilder<AddColumnOperation> email = table.Column<string>("nvarchar(150)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			OperationBuilder<AddColumnOperation> projectType = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 2000;
			return new
			{
				Id = id,
				Name = name,
				Email = email,
				ProjectType = projectType,
				Message = table.Column<string>("nvarchar(2000)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_ContactMessages", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("HolidaySettings", (ColumnsBuilder table) => new
		{
			Id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1"),
			Day = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
			IsHoliday = table.Column<bool>("bit", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_HolidaySettings", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("Projects", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			int? num = 100;
			OperationBuilder<AddColumnOperation> title = table.Column<string>("nvarchar(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 50;
			OperationBuilder<AddColumnOperation> category = table.Column<string>("nvarchar(50)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 150;
			OperationBuilder<AddColumnOperation> client = table.Column<string>("nvarchar(150)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			OperationBuilder<AddColumnOperation> year = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			OperationBuilder<AddColumnOperation> projectUrl = table.Column<string>("nvarchar(500)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 300;
			OperationBuilder<AddColumnOperation> shortDescription = table.Column<string>("nvarchar(300)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			OperationBuilder<AddColumnOperation> overview = table.Column<string>("nvarchar(max)", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 200;
			OperationBuilder<AddColumnOperation> slug = table.Column<string>("nvarchar(200)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 20;
			OperationBuilder<AddColumnOperation> status = table.Column<string>("nvarchar(20)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			return new
			{
				Id = id,
				Title = title,
				Category = category,
				Client = client,
				Year = year,
				ProjectUrl = projectUrl,
				ShortDescription = shortDescription,
				Overview = overview,
				Slug = slug,
				Status = status,
				HeroImage = table.Column<string>("nvarchar(500)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				NextProjectId = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				UpdatedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				PublishedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_Projects", x => (object)x.Id);
			table.ForeignKey("FK_Projects_Projects_NextProjectId", x => (object)x.NextProjectId, "Projects", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)0);
		}, (string)null);
		migrationBuilder.CreateTable("User", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			int? num = 150;
			return new
			{
				Id = id,
				Username = table.Column<string>("nvarchar(150)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				Password = table.Column<string>("nvarchar(max)", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_User", x => (object)x.Id);
		}, (string)null);
		migrationBuilder.CreateTable("ProjectFeatures", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			OperationBuilder<AddColumnOperation> projectId = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 150;
			return new
			{
				Id = id,
				ProjectId = projectId,
				Title = table.Column<string>("nvarchar(150)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				Description = table.Column<string>("nvarchar(max)", (bool?)null, (int?)null, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				DisplayOrder = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_ProjectFeatures", x => (object)x.Id);
			table.ForeignKey("FK_ProjectFeatures_Projects_ProjectId", x => (object)x.ProjectId, "Projects", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("ProjectMedia", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			OperationBuilder<AddColumnOperation> projectId = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 20;
			OperationBuilder<AddColumnOperation> mediaType = table.Column<string>("nvarchar(20)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 500;
			OperationBuilder<AddColumnOperation> filePath = table.Column<string>("nvarchar(500)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 255;
			OperationBuilder<AddColumnOperation> originalFileName = table.Column<string>("nvarchar(255)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			num = 100;
			return new
			{
				Id = id,
				ProjectId = projectId,
				MediaType = mediaType,
				FilePath = filePath,
				OriginalFileName = originalFileName,
				ContentType = table.Column<string>("nvarchar(100)", (bool?)null, num, false, (string)null, true, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				FileSize = table.Column<long>("bigint", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				DisplayOrder = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				CreatedAt = table.Column<DateTime>("datetime2", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_ProjectMedia", x => (object)x.Id);
			table.ForeignKey("FK_ProjectMedia_Projects_ProjectId", x => (object)x.ProjectId, "Projects", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateTable("ProjectTechnologies", (ColumnsBuilder table) =>
		{
			OperationBuilder<AddColumnOperation> id = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null).Annotation("SqlServer:Identity", (object)"1, 1");
			OperationBuilder<AddColumnOperation> projectId = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null);
			int? num = 100;
			return new
			{
				Id = id,
				ProjectId = projectId,
				Name = table.Column<string>("nvarchar(100)", (bool?)null, num, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null),
				DisplayOrder = table.Column<int>("int", (bool?)null, (int?)null, false, (string)null, false, (object)null, (string)null, (string)null, (bool?)null, (string)null, (string)null, (int?)null, (int?)null, (bool?)null)
			};
		}, (string)null, table =>
		{
			table.PrimaryKey("PK_ProjectTechnologies", x => (object)x.Id);
			table.ForeignKey("FK_ProjectTechnologies_Projects_ProjectId", x => (object)x.ProjectId, "Projects", "Id", (string)null, (ReferentialAction)0, (ReferentialAction)2);
		}, (string)null);
		migrationBuilder.CreateIndex("IX_ProjectFeatures_ProjectId", "ProjectFeatures", "ProjectId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_ProjectMedia_ProjectId", "ProjectMedia", "ProjectId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_Projects_NextProjectId", "Projects", "NextProjectId", (string)null, false, (string)null, (bool[])null);
		migrationBuilder.CreateIndex("IX_ProjectTechnologies_ProjectId", "ProjectTechnologies", "ProjectId", (string)null, false, (string)null, (bool[])null);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable("ContactMessages", (string)null);
		migrationBuilder.DropTable("HolidaySettings", (string)null);
		migrationBuilder.DropTable("ProjectFeatures", (string)null);
		migrationBuilder.DropTable("ProjectMedia", (string)null);
		migrationBuilder.DropTable("ProjectTechnologies", (string)null);
		migrationBuilder.DropTable("User", (string)null);
		migrationBuilder.DropTable("Projects", (string)null);
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
		modelBuilder.Entity("techninxa.Models.HolidaySetting", (Action<EntityTypeBuilder>)((EntityTypeBuilder b) =>
		{
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Id").ValueGeneratedOnAdd(), "int");
			SqlServerPropertyBuilderExtensions.UseIdentityColumn<int>(b.Property<int>("Id"), 1L, 1);
			RelationalPropertyBuilderExtensions.HasColumnType<int>(b.Property<int>("Day"), "int");
			RelationalPropertyBuilderExtensions.HasColumnType<bool>(b.Property<bool>("IsHoliday"), "bit");
			b.HasKey(new string[1] { "Id" });
			RelationalEntityTypeBuilderExtensions.ToTable(b, "HolidaySettings");
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
