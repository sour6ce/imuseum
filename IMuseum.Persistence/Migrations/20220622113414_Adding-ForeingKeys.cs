using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMuseum.Persistence.Migrations
{
    public partial class AddingForeingKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Museums_RelatedMuseumId",
                table: "LoanApplications");

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("03b3a8ff-64c7-4479-97f1-c969f911c0e4"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("1bc1d70e-68e9-40d1-82fb-f6ae3102d277"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("1f2b9e19-12f0-48dd-8195-0461f8f1c85e"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("3ab3aac9-eceb-4fa4-a237-aaab8fec8e65"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("88c9f40e-0cea-4429-9a59-c6ddab2b1c9a"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("9d482011-47e9-4b22-9c64-f12531b3f309"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("b116223d-f86a-4716-98ca-c72db7967338"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("05fae288-f85a-4819-832d-94ef6fcdb0cb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("19679ee9-7236-4d68-a0f6-ac44ea1f7499"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4d81b283-eeda-4074-86a9-c588d7288b2b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("750f1c9f-686e-499d-9d47-c11908b7148d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("82097ae9-7cc6-46ef-b222-8f257920098a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8ff89b90-7276-4d48-a4fb-a9699e63d8df"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d73b35cb-d5b6-47a2-bffb-7226c3abc8a9"));

            migrationBuilder.RenameColumn(
                name: "RelatedMuseumId",
                table: "LoanApplications",
                newName: "MuseumId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplications_RelatedMuseumId",
                table: "LoanApplications",
                newName: "IX_LoanApplications_MuseumId");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanAplicationId",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("0abd4e03-0f0f-479a-adf8-8367925e0609"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3029), false, null, "Kiko's Gallery" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("19de75f6-06a4-4e5b-b1d5-fb820825302e"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(2910), false, null, "Louvre Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("96d7e326-9e23-411c-840c-66589a001952"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3032), false, null, "LeTize Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("9aa06adb-b590-42b1-9301-aae7f599f42c"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(2984), false, null, "London Arqueology Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("ab27caf7-d060-4736-a7c7-d568acaa6e85"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3026), false, null, "Vatican City Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("bcb63fb6-0cc0-4e65-85ca-2fa07fbc4e45"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(2980), false, null, "British Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("c8f4bc33-e646-4feb-8ff3-27fc19f4acff"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(2986), false, null, "New York Museum" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("1d9cde8f-5014-44c4-b66d-1acce9066272"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3171), false, null, "Role 2" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("47eb910a-26cc-40e7-933b-4f421c4f53f4"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3177), false, null, "Role 4" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("6a3cea16-99c3-403a-b8d4-6380de99ecc2"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3174), false, null, "Role 3" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("b891fb2d-fd53-4d09-816e-ef6562d6af2f"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3155), false, null, "Role 1" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("c691b205-77dc-4cba-a5a3-4cc344dd0ab3"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3184), false, null, "Role 7" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("e8ce3c0f-7314-417e-856e-65c76a615dc7"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3179), false, null, "Role 5" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("f17acb1c-501e-44e2-9f51-54a0d6923ba5"), new DateTime(2022, 6, 22, 13, 34, 14, 118, DateTimeKind.Local).AddTicks(3182), false, null, "Role 6" });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Museums_MuseumId",
                table: "LoanApplications",
                column: "MuseumId",
                principalTable: "Museums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Museums_MuseumId",
                table: "LoanApplications");

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("0abd4e03-0f0f-479a-adf8-8367925e0609"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("19de75f6-06a4-4e5b-b1d5-fb820825302e"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("96d7e326-9e23-411c-840c-66589a001952"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("9aa06adb-b590-42b1-9301-aae7f599f42c"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("ab27caf7-d060-4736-a7c7-d568acaa6e85"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("bcb63fb6-0cc0-4e65-85ca-2fa07fbc4e45"));

            migrationBuilder.DeleteData(
                table: "Museums",
                keyColumn: "Id",
                keyValue: new Guid("c8f4bc33-e646-4feb-8ff3-27fc19f4acff"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1d9cde8f-5014-44c4-b66d-1acce9066272"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("47eb910a-26cc-40e7-933b-4f421c4f53f4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6a3cea16-99c3-403a-b8d4-6380de99ecc2"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b891fb2d-fd53-4d09-816e-ef6562d6af2f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c691b205-77dc-4cba-a5a3-4cc344dd0ab3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e8ce3c0f-7314-417e-856e-65c76a615dc7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f17acb1c-501e-44e2-9f51-54a0d6923ba5"));

            migrationBuilder.DropColumn(
                name: "LoanAplicationId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MuseumId",
                table: "LoanApplications",
                newName: "RelatedMuseumId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanApplications_MuseumId",
                table: "LoanApplications",
                newName: "IX_LoanApplications_RelatedMuseumId");

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("03b3a8ff-64c7-4479-97f1-c969f911c0e4"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5805), false, null, "Vatican City Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("1bc1d70e-68e9-40d1-82fb-f6ae3102d277"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5726), false, null, "Louvre Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("1f2b9e19-12f0-48dd-8195-0461f8f1c85e"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5810), false, null, "LeTize Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("3ab3aac9-eceb-4fa4-a237-aaab8fec8e65"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5803), false, null, "New York Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("88c9f40e-0cea-4429-9a59-c6ddab2b1c9a"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5808), false, null, "Kiko's Gallery" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("9d482011-47e9-4b22-9c64-f12531b3f309"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5800), false, null, "London Arqueology Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("b116223d-f86a-4716-98ca-c72db7967338"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5797), false, null, "British Museum" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("05fae288-f85a-4819-832d-94ef6fcdb0cb"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5937), false, null, "Role 1" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("19679ee9-7236-4d68-a0f6-ac44ea1f7499"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(6002), false, null, "Role 6" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("4d81b283-eeda-4074-86a9-c588d7288b2b"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5955), false, null, "Role 3" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("750f1c9f-686e-499d-9d47-c11908b7148d"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5957), false, null, "Role 4" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("82097ae9-7cc6-46ef-b222-8f257920098a"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5960), false, null, "Role 5" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("8ff89b90-7276-4d48-a4fb-a9699e63d8df"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(5952), false, null, "Role 2" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { new Guid("d73b35cb-d5b6-47a2-bffb-7226c3abc8a9"), new DateTime(2022, 6, 22, 13, 22, 54, 774, DateTimeKind.Local).AddTicks(6005), false, null, "Role 7" });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Museums_RelatedMuseumId",
                table: "LoanApplications",
                column: "RelatedMuseumId",
                principalTable: "Museums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
