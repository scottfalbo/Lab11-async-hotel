using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncHotel.Migrations
{
    public partial class joiningstill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomAmenities",
                columns: new[] { "RoomId", "AmenitiesId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "RoomAmenities",
                columns: new[] { "RoomId", "AmenitiesId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumns: new[] { "RoomId", "AmenitiesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumns: new[] { "RoomId", "AmenitiesId" },
                keyValues: new object[] { 2, 2 });
        }
    }
}
