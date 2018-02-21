﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Reggie.Blog.Data;
using System;

namespace Reggie.Blog.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20180116114413_update1.3")]
    partial class update13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reggie.Blog.Models.InformalEssay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("UserName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("InformalEssays");
                });

            modelBuilder.Entity("Reggie.Blog.Models.JobExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("EndDateTime")
                        .HasColumnType("Date");

                    b.Property<string>("JobContent")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("Date");

                    b.HasKey("Id");

                    b.ToTable("JobExperiences");
                });

            modelBuilder.Entity("Reggie.Blog.Models.LeaveMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("Reggie.Blog.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("LastUpdateDateTime");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
