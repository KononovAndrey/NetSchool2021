using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSRNetSchool.Db.Context.Migrations
{
    public partial class _02_authornulable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "test_table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field1 = table.Column<int>(type: "int", nullable: false),
                    Field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_table", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_test_table_Uid",
                table: "test_table",
                column: "Uid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "test_table");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
