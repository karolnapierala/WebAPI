using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "PostalCode", "Street" },
                values: new object[] { 1, "Kraków", 30001, "Długa 5" });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "Description", "Name", "Price", "RestaurantId" },
                values: new object[] { 1, "dsffgsdh", "Hot Chicken", 10.30m, 0 });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "AddressId", "Category", "ContactEmail", "ContactNumber", "Descripton", "HasDelivery", "Name" },
                values: new object[] { 1, 1, "Fast Food", "contact@kfc.com", "123456789", "shgkfsdgisdfughdfiugh h uisaogiuoagag riugisgosg", true, "KFC" });
        }
    }
}
