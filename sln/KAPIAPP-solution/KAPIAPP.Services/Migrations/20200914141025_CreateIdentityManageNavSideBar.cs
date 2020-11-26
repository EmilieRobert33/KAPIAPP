using Microsoft.EntityFrameworkCore.Migrations;

namespace KAPIAPP.Services.Migrations
{
    public partial class CreateIdentityManageNavSideBar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14d9882f-599b-4099-84c4-060c82509318");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7596496e-f2fa-4804-9ecb-ff5ed4fcbf30");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0031b367-40b9-46d4-b899-91e6457f7304", "d901d6d4-e9f2-4d04-ab23-dded9f6f2b1d", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eeba3670-f13f-49a5-b6c0-2c6b2842b3a2", "42738673-7ef9-4f4c-a0ea-2c21389a674e", "Evaluateur", "EVALUATEUR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0031b367-40b9-46d4-b899-91e6457f7304");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeba3670-f13f-49a5-b6c0-2c6b2842b3a2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14d9882f-599b-4099-84c4-060c82509318", "d7a0b83a-bb81-4dce-b0ad-5365aa6800fb", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7596496e-f2fa-4804-9ecb-ff5ed4fcbf30", "34e34d32-27d1-489e-9dc7-293384118a57", "Evaluateur", "EVALUATEUR" });
        }
    }
}
