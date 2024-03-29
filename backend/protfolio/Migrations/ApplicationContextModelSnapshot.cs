﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using protfolio.Data;

namespace protfolio.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("protfolio.Data.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("protfolio.Data.NeedMembers", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("SpecializationId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("SphereId");

                    b.HasKey("ProjectId", "SpecializationId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("SphereId");

                    b.ToTable("NeedMembers");
                });

            modelBuilder.Entity("protfolio.Data.Participant", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("UserId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsOwner");

                    b.Property<string>("Role");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("protfolio.Data.Profskills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profskills");
                });

            modelBuilder.Entity("protfolio.Data.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Rate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("protfolio.Data.ProjectShperes", b =>
                {
                    b.Property<int>("ProjectId");

                    b.Property<int>("SphereId");

                    b.HasKey("ProjectId", "SphereId");

                    b.HasIndex("SphereId");

                    b.ToTable("ProjectShperes");
                });

            modelBuilder.Entity("protfolio.Data.ProjectTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTags");
                });

            modelBuilder.Entity("protfolio.Data.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("protfolio.Data.Sphere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Spheres");
                });

            modelBuilder.Entity("protfolio.Data.SphereSpecializations", b =>
                {
                    b.Property<int>("SphereId");

                    b.Property<int>("SpecializationId");

                    b.HasKey("SphereId", "SpecializationId");

                    b.HasIndex("SpecializationId");

                    b.ToTable("SphereSpecializations");
                });

            modelBuilder.Entity("protfolio.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<string>("Image");

                    b.Property<DateTime>("LastVisit");

                    b.Property<string>("MiddleName");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("ReadyToWork");

                    b.Property<DateTime>("RegisterDate");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("SecondName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("protfolio.Data.UserContacts", b =>
                {
                    b.Property<int>("ContactId");

                    b.Property<int>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("ContactId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("protfolio.Data.UserSpecializations", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("SpecializationId");

                    b.Property<int>("SphereId");

                    b.HasKey("UserId", "SpecializationId", "SphereId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("SphereId");

                    b.ToTable("UserSpecializations");
                });

            modelBuilder.Entity("protfolio.Data.NeedMembers", b =>
                {
                    b.HasOne("protfolio.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.Participant", b =>
                {
                    b.HasOne("protfolio.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.Profskills", b =>
                {
                    b.HasOne("protfolio.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.ProjectShperes", b =>
                {
                    b.HasOne("protfolio.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.ProjectTags", b =>
                {
                    b.HasOne("protfolio.Data.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.SphereSpecializations", b =>
                {
                    b.HasOne("protfolio.Data.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.UserContacts", b =>
                {
                    b.HasOne("protfolio.Data.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("protfolio.Data.UserSpecializations", b =>
                {
                    b.HasOne("protfolio.Data.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.Sphere", "Sphere")
                        .WithMany()
                        .HasForeignKey("SphereId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("protfolio.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
