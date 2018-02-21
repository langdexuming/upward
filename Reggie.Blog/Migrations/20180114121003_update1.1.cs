using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Reggie.Blog.Migrations
{
    public partial class update11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Skills",
                newName: "CreateDateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JobExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: true),
                    JobContent = table.Column<string>(maxLength: 500, nullable: true),
                    Position = table.Column<string>(maxLength: 50, nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobExperiences", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobExperiences");

            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "Skills",
                newName: "StartDateTime");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
