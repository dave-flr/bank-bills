using Microsoft.EntityFrameworkCore.Migrations;

namespace bank_bills.Migrations
{
    public partial class RenamedRUCField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RUCNumber",
                table: "JuridicPersons",
                newName: "RucNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RucNumber",
                table: "JuridicPersons",
                newName: "RUCNumber");
        }
    }
}
