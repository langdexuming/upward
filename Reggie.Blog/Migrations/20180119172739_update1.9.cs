using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Reggie.Blog.Migrations
{
    public partial class update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EssayCategoryId",
                table: "InformalEssays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EssayCategory",
                columns: table => new
                {
                    EssayCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EssayCategory", x => x.EssayCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformalEssays_EssayCategoryId",
                table: "InformalEssays",
                column: "EssayCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InformalEssays_EssayCategory_EssayCategoryId",
                table: "InformalEssays",
                column: "EssayCategoryId",
                principalTable: "EssayCategory",
                principalColumn: "EssayCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformalEssays_EssayCategory_EssayCategoryId",
                table: "InformalEssays");

            migrationBuilder.DropTable(
                name: "EssayCategory");

            migrationBuilder.DropIndex(
                name: "IX_InformalEssays_EssayCategoryId",
                table: "InformalEssays");

            migrationBuilder.DropColumn(
                name: "EssayCategoryId",
                table: "InformalEssays");
        }
    }
}
