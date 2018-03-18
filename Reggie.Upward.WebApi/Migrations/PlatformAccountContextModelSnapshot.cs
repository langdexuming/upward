﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Reggie.Upward.WebApi.Areas.PlatformAccount.Data;
using System;

namespace Reggie.Upward.WebApi.Migrations
{
    [DbContext(typeof(PlatformAccountContext))]
    partial class PlatformAccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reggie.Upward.WebApi.Areas.PlatformAccount.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Remark");

                    b.HasKey("Id");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("Reggie.Upward.WebApi.Areas.PlatformAccount.Models.PlatformAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(256);

                    b.Property<int>("PlatformId");

                    b.Property<string>("Remark")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("PlatformId");

                    b.ToTable("PlatformAccounts");
                });

            modelBuilder.Entity("Reggie.Upward.WebApi.Areas.PlatformAccount.Models.PlatformAccount", b =>
                {
                    b.HasOne("Reggie.Upward.WebApi.Areas.PlatformAccount.Models.Platform", "PlatformItem")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
