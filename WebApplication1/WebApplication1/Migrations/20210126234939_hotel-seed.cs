using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncHotel.Migrations
{
    public partial class hotelseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAdress",
                table: "Hotels",
                newName: "StreetAddress");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "City", "Country", "Name", "Phone", "State", "StreetAddress" },
                values: new object[] { 1, "Estes Park", "United States", "The Overlook", "833-888-0237", "Colorado", "333 Wonderview Avenue" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Hotels",
                newName: "StreetAdress");

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
