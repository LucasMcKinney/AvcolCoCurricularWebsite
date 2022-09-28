namespace AvcolCoCurricularWebsite.Data;

public class AvcolCoCurricularWebsiteContext : DbContext
{
    public AvcolCoCurricularWebsiteContext (DbContextOptions<AvcolCoCurricularWebsiteContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activity { get; set; }
    public DbSet<Club> Club { get; set; }
    public DbSet<Music> Music { get; set; }
    public DbSet<PerformingArt> PerformingArt { get; set; }
    public DbSet<PersonalInformation> PersonalInformation { get; set; }
    public DbSet<ScholarshipTutorial> ScholarshipTutorial { get; set; }
    public DbSet<Sport> Sport { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<SubjectTutorial> SubjectTutorial { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>().ToTable("Activity");
        modelBuilder.Entity<Club>().ToTable("Club");
        modelBuilder.Entity<Music>().ToTable("Music");
        modelBuilder.Entity<PerformingArt>().ToTable("PerformingArt");
        modelBuilder.Entity<PersonalInformation>().ToTable("PersonalInformation");
        modelBuilder.Entity<ScholarshipTutorial>().ToTable("ScholarshipTutorial");
        modelBuilder.Entity<Sport>().ToTable("Sport");
        modelBuilder.Entity<Staff>().ToTable("Staff");
        modelBuilder.Entity<SubjectTutorial>().ToTable("SubjectTutorial");
    }
}