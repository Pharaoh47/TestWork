using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BirthDay = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDay", "Name" },
                values: new object[] { 1, new DateTime(2008, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An first test name" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDay", "Name" },
                values: new object[] { 2, new DateTime(2012, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "An second test name" });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Number", "PersonId" },
                values: new object[] { 1, "+79999999999", 1 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Number", "PersonId" },
                values: new object[] { 2, "+77777777777", 1 });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Number", "PersonId" },
                values: new object[] { 3, "+73333333333", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Phones_PersonId",
                table: "Phones",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
