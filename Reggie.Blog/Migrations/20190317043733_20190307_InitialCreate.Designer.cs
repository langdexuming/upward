﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reggie.Blog.Data;

namespace Reggie.Blog.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20190317043733_20190307_InitialCreate")]
    partial class _20190307_InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reggie.Blog.Models.ContentFlag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(20);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("ContentFlags");
                });

            modelBuilder.Entity("Reggie.Blog.Models.EssayCategory", b =>
                {
                    b.Property<int>("EssayCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .HasMaxLength(20);

                    b.HasKey("EssayCategoryId");

                    b.ToTable("EssayCategories");
                });

            modelBuilder.Entity("Reggie.Blog.Models.InformalEssay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<int>("EssayCategoryId");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("EssayCategoryId");

                    b.ToTable("InformalEssays");
                });

            modelBuilder.Entity("Reggie.Blog.Models.JobExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("Date");

                    b.Property<string>("JobContent")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.ToTable("JobExperiences");
                });

            modelBuilder.Entity("Reggie.Blog.Models.LeaveMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LeaveMessages");
                });

            modelBuilder.Entity("Reggie.Blog.Models.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("LastUpdateDateTime");

                    b.Property<string>("SourceUrl")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.Property<string>("ViewUrl")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Samples");
                });

            modelBuilder.Entity("Reggie.Blog.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("LastUpdateDateTime");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Reggie.Blog.Models.SwitchFlag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsVaild");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("Remark")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("SwitchFlags");
                });

            modelBuilder.Entity("Reggie.Blog.Models.InformalEssay", b =>
                {
                    b.HasOne("Reggie.Blog.Models.EssayCategory", "EssayCategoryItem")
                        .WithMany("InformalEssays")
                        .HasForeignKey("EssayCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
