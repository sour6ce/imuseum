using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMuseum.Persistence.Migrations
{
    public partial class aditionaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "LoanAplicationId", "PaymentAmount", "StartDate" },
                values: new object[] { 1, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9400), false, null, 1, 10m, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "LoanAplicationId", "PaymentAmount", "StartDate" },
                values: new object[] { 2, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9405), false, null, 2, 20m, new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9223));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9259));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9261));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9264));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9266));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9268));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9368));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9371));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9373));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9380));

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9455), false, null, "Davinci" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 2, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9457), false, null, "Gallery" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 3, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9459), false, null, "Galileo" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 1, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9439), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "It is just a simple tapice.", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 1, "The tapice" });

            migrationBuilder.InsertData(
                table: "LoanApplications",
                columns: new[] { "Id", "AddTime", "ApplicationDate", "ArtworkId", "CurrentStatus", "Deleted", "DeletedTime", "Duration", "LoanId", "MuseumId" },
                values: new object[] { 1, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9417), new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, false, null, 10, 1, 1 });

            migrationBuilder.InsertData(
                table: "LoanApplications",
                columns: new[] { "Id", "AddTime", "ApplicationDate", "ArtworkId", "CurrentStatus", "Deleted", "DeletedTime", "Duration", "LoanId", "MuseumId" },
                values: new object[] { 2, new DateTime(2022, 6, 23, 21, 14, 26, 475, DateTimeKind.Local).AddTicks(9421), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, false, null, 10, 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoanApplications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LoanApplications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Artworks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6126));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6128));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6129));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6133));

            migrationBuilder.UpdateData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6230));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6234));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 11,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6237));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 12,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6239));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 13,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 14,
                column: "AddTime",
                value: new DateTime(2022, 6, 23, 20, 39, 3, 471, DateTimeKind.Local).AddTicks(6242));
        }
    }
}
