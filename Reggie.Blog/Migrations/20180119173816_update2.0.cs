using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Reggie.Blog.Migrations
{
    public partial class update20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformalEssays_EssayCategory_EssayCategoryId",
                table: "InformalEssays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EssayCategory",
                table: "EssayCategory");

            migrationBuilder.RenameTable(
                name: "EssayCategory",
                newName: "EssayCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EssayCategories",
                table: "EssayCategories",
                column: "EssayCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformalEssays_EssayCategories_EssayCategoryId",
                table: "InformalEssays",
                column: "EssayCategoryId",
                principalTable: "EssayCategories",
                principalColumn: "EssayCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformalEssays_EssayCategories_EssayCategoryId",
                table: "InformalEssays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EssayCategories",
                table: "EssayCategories");

            migrationBuilder.RenameTable(
                name: "EssayCategories",
                newName: "EssayCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EssayCategory",
                table: "EssayCategory",
                column: "EssayCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformalEssays_EssayCategory_EssayCategoryId",
                table: "InformalEssays",
                column: "EssayCategoryId",
                principalTable: "EssayCategory",
                principalColumn: "EssayCategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
