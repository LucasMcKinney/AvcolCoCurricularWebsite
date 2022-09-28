﻿namespace AvcolCoCurricularWebsite.Migrations;

[DbContext(typeof(AvcolCoCurricularWebsiteContext))]

partial class AvcolCoCurricularWebsiteContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "3.1.28")
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Activity", b =>
            {
                b.Property<int>("ActivityID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("ActivityName")
                    .IsRequired()
                    .HasColumnType("nvarchar(35)")
                    .HasMaxLength(35);

                b.Property<string>("RoomNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(3)")
                    .HasMaxLength(3);

                b.Property<string>("SignUpForm")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("StaffID")
                    .HasColumnType("int");

                b.HasKey("ActivityID");

                b.HasIndex("StaffID");

                b.ToTable("Activity");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Club", b =>
            {
                b.Property<int>("ClubID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("ClubID");

                b.HasIndex("ActivityID");

                b.ToTable("Club");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Music", b =>
            {
                b.Property<int>("MusicID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("MusicID");

                b.HasIndex("ActivityID");

                b.ToTable("Music");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.PerformingArt", b =>
            {
                b.Property<int>("PerformingArtID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("PerformingArtID");

                b.HasIndex("ActivityID");

                b.ToTable("PerformingArt");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.PersonalInformation", b =>
            {
                b.Property<int>("StaffID")
                    .HasColumnType("int");

                b.Property<string>("Address")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("CitizenStatus")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("DateOfBirth")
                    .HasColumnType("datetime2");

                b.Property<string>("ECPhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(10)")
                    .HasMaxLength(10);

                b.Property<string>("ECRelationship")
                    .IsRequired()
                    .HasColumnType("nvarchar(56)")
                    .HasMaxLength(56);

                b.Property<string>("EmailAddress")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Ethnicity")
                    .IsRequired()
                    .HasColumnType("nvarchar(56)")
                    .HasMaxLength(56);

                b.Property<string>("PhoneNumber")
                    .IsRequired()
                    .HasColumnType("nvarchar(10)")
                    .HasMaxLength(10);

                b.HasKey("StaffID");

                b.ToTable("PersonalInformation");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.ScholarshipTutorial", b =>
            {
                b.Property<int>("ScholarshipTutorialID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("ScholarshipTutorialID");

                b.HasIndex("ActivityID");

                b.ToTable("ScholarshipTutorial");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Sport", b =>
            {
                b.Property<int>("SportID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("SportID");

                b.HasIndex("ActivityID");

                b.ToTable("Sport");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Staff", b =>
            {
                b.Property<int>("StaffID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(35)")
                    .HasMaxLength(35);

                b.Property<DateTime>("HireDate")
                    .HasColumnType("datetime2");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("nvarchar(35)")
                    .HasMaxLength(35);

                b.Property<string>("TeacherCode")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("StaffID");

                b.ToTable("Staff");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.SubjectTutorial", b =>
            {
                b.Property<int>("SubjectTutorialID")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("ActivityID")
                    .HasColumnType("int");

                b.Property<int>("Day")
                    .HasColumnType("int");

                b.Property<DateTime>("EndTime")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("StartTime")
                    .HasColumnType("datetime2");

                b.HasKey("SubjectTutorialID");

                b.HasIndex("ActivityID");

                b.ToTable("SubjectTutorial");
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Activity", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Staff", "Staff")
                    .WithMany()
                    .HasForeignKey("StaffID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Club", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Music", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.PerformingArt", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.PersonalInformation", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Staff", "Staff")
                    .WithOne("PersonalInformation")
                    .HasForeignKey("AvcolCoCurricularWebsite.Models.PersonalInformation", "StaffID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.ScholarshipTutorial", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.Sport", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        modelBuilder.Entity("AvcolCoCurricularWebsite.Models.SubjectTutorial", b =>
            {
                b.HasOne("AvcolCoCurricularWebsite.Models.Activity", "Activity")
                    .WithMany()
                    .HasForeignKey("ActivityID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
    }
}