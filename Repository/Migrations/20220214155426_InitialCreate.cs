using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationLongitude = table.Column<double>(type: "float", nullable: false),
                    LocationLatitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "DateEmail_Index",
                table: "Words",
                columns: new[] { "AddTime", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "DateText_Index",
                table: "Words",
                columns: new[] { "AddTime", "Text" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
