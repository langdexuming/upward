using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Reggie.Blog.Migrations
{
    public partial class update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "JobExperiences",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "JobExperiences",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "JobExperiences",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "JobExperiences",
                newName: "EndDateTime");
        }
    }
}
