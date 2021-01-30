using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncHotel.Migrations
{
    public partial class joininghotelroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "HotelId", "RoomId", "PetFriendly", "Rate", "RoomNumber" },
                values: new object[,]
                {
                    { 1, 1, false, 0m, 0 },
                    { 2, 2, false, 0m, 0 },
                    { 3, 3, false, 0m, 0 }
                });

            migrationBuilder.InsertData(
                table: "RoomAmenities",
                columns: new[] { "RoomId", "AmenitiesId" },
                values: new object[] { 3, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumns: new[] { "HotelId", "RoomId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumns: new[] { "HotelId", "RoomId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "HotelRooms",
                keyColumns: new[] { "HotelId", "RoomId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "RoomAmenities",
                keyColumns: new[] { "RoomId", "AmenitiesId" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
