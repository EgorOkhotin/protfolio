using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace protfolio.Data
{
    public class ApplicationContext : DbContext, IUserContext, IProjectContext, ISpheresContext
    {
        public ApplicationContext():base()
        {
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }
        public DbSet<UserSpecializations> UserSpecializations { get; set; }
        public DbSet<Profskills> Profskills { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ProjectShperes> ProjectShperes { get; set; }
        public DbSet<ProjectTags> ProjectTags { get; set; }
        public DbSet<NeedMembers> NeedMembers { get; set; }

        public DbSet<Sphere> Spheres { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<SphereSpecializations> SphereSpecializations { get; set; }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=portfolio;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(x => x.Salt).HasMaxLength(64);
            modelBuilder.ApplyConfiguration(new ParticipantConfig());
            modelBuilder.ApplyConfiguration(new ProjectSpheresConfig());
            modelBuilder.ApplyConfiguration(new SphereSpecializationsConfig());
            modelBuilder.ApplyConfiguration(new UserContactsConfig());
            modelBuilder.ApplyConfiguration(new UserSpecializationsConfig());
            modelBuilder.ApplyConfiguration(new NeedMembersConfig());
            base.OnModelCreating(modelBuilder);
        }

        void IContext.SaveChanges()
        {
            base.SaveChanges();
        }
    }

    public class ParticipantConfig : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.UserId });
        }
    }

    public class ProjectSpheresConfig : IEntityTypeConfiguration<ProjectShperes>
    {
        public void Configure(EntityTypeBuilder<ProjectShperes> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.SphereId });
        }
    }

    public class SphereSpecializationsConfig : IEntityTypeConfiguration<SphereSpecializations>
    {
        public void Configure(EntityTypeBuilder<SphereSpecializations> builder)
        {
            builder.HasKey(x => new { x.SphereId, x.SpecializationId });
        }
    }

    public class UserContactsConfig : IEntityTypeConfiguration<UserContacts>
    {
        public void Configure(EntityTypeBuilder<UserContacts> builder)
        {
            builder.HasKey(x => new { x.ContactId, x.UserId });
        }
    }

    public class UserSpecializationsConfig : IEntityTypeConfiguration<UserSpecializations>
    {
        public void Configure(EntityTypeBuilder<UserSpecializations> builder)
        {
            builder.HasKey(x => new { x.UserId, x.SpecializationId });
        }
    }

    public class NeedMembersConfig : IEntityTypeConfiguration<NeedMembers>
    {
        public void Configure(EntityTypeBuilder<NeedMembers> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.SpecializationId });
        }
    }


}
