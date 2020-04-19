using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProtectedFiles.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cms_ProtectedFiles",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(8)", nullable: true),
                    FileSizeInBytes = table.Column<long>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cms_ProtectedFiles", x => x.ItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cms_ProtectedFiles");
        }
    }
}
