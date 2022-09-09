using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AvcolCoCurricularWebsite.Models;

namespace AvcolCoCurricularWebsite.Data
{
    public class AvcolCoCurricularWebsiteContext : DbContext
    {
        public AvcolCoCurricularWebsiteContext (DbContextOptions<AvcolCoCurricularWebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<AvcolCoCurricularWebsite.Models.Activity> Activity { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.Club> Club { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.Music> Music { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.PerformingArt> PerformingArt { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.PersonalInformation> PersonalInformation { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.ScholarshipTutorial> ScholarshipTutorial { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.Sport> Sport { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.Staff> Staff { get; set; }
        public DbSet<AvcolCoCurricularWebsite.Models.SubjectTutorial> SubjectTutorial { get; set; }

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
}