using Microsoft.EntityFrameworkCore.Migrations;

namespace KAPIAPP.Services.Migrations
{
    public partial class CreateIdentityManageProfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d443aca-061a-4208-971b-3826b0f7407f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f1d2546-a43c-4d06-9272-29b34fa04745");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14d9882f-599b-4099-84c4-060c82509318", "d7a0b83a-bb81-4dce-b0ad-5365aa6800fb", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7596496e-f2fa-4804-9ecb-ff5ed4fcbf30", "34e34d32-27d1-489e-9dc7-293384118a57", "Evaluateur", "EVALUATEUR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "9f1d2546-a43c-4d06-9272-29b34fa04745", "40cc3d61-7d8a-496f-80fe-1bf179540bb6", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5d443aca-061a-4208-971b-3826b0f7407f", "7b8a1d15-7b9a-40b7-b87d-0e71e4ca2892", "Evaluateur", "EVALUATEUR" });
        }
    }
}
