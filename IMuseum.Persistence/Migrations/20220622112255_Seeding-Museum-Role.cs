using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMuseum.Persistence.Migrations
{
    public partial class SeedingMuseumRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Museums_MuseumId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Rooms_RoomId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_LoanApplications_ApplicationId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ApplicationId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "LoanApplications",
                newName: "CurrentStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Artworks",
                newName: "CurrentSatus");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanId",
                table: "LoanApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Artworks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MuseumId",
                table: "Artworks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Artworks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Bytes = table.Column<byte[]>(type: "BLOB", nullable: false),
                    FileExtension = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RelatedUsersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RolesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RelatedUsersId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_RelatedUsersId",
                        column: x => x.RelatedUsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_LoanId",
                table: "LoanApplications",
                column: "LoanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Image_ArtworkId",
                table: "Image",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_RolesId",
                table: "RoleUser",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Museums_MuseumId",
                table: "Artworks",
                column: "MuseumId",
                principalTable: "Museums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Rooms_RoomId",
                table: "Artworks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Loans_LoanId",
                table: "LoanApplications",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Museums_MuseumId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Rooms_RoomId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Loans_LoanId",
                table: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_LoanId",
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

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Artworks");

            migrationBuilder.RenameColumn(
                name: "CurrentStatus",
                table: "LoanApplications",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CurrentSatus",
                table: "Artworks",
                newName: "Status");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Roles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Artworks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "MuseumId",
                table: "Artworks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApplicationId",
                table: "Loans",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Museums_MuseumId",
                table: "Artworks",
                column: "MuseumId",
                principalTable: "Museums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Rooms_RoomId",
                table: "Artworks",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_LoanApplications_ApplicationId",
                table: "Loans",
                column: "ApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
