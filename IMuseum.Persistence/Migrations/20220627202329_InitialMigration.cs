using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMuseum.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Museums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Museums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IncorporatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Period = table.Column<string>(type: "TEXT", nullable: false),
                    Assessment = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentSatus = table.Column<int>(type: "INTEGER", nullable: false),
                    MuseumId = table.Column<int>(type: "INTEGER", nullable: true),
                    Images = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: true),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artworks_Museums_MuseumId",
                        column: x => x.MuseumId,
                        principalTable: "Museums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artworks_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApplicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtworkId = table.Column<int>(type: "INTEGER", nullable: false),
                    MuseumId = table.Column<int>(type: "INTEGER", nullable: true),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Museums_MuseumId",
                        column: x => x.MuseumId,
                        principalTable: "Museums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Paintings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Media = table.Column<string>(type: "TEXT", nullable: false),
                    Style = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paintings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paintings_Artworks_Id",
                        column: x => x.Id,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restorations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ArtworkId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restorations_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sculptures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Material = table.Column<string>(type: "TEXT", nullable: false),
                    Style = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sculptures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sculptures_Artworks_Id",
                        column: x => x.Id,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    LoanApplicationId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7473), false, null, "Louvre Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7518), false, null, "British Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 3, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7521), false, null, "London Arqueology Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 4, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7522), false, null, "New York Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 5, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7524), false, null, "Vatican City Museum" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 6, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7526), false, null, "Kiko's Gallery" });

            migrationBuilder.InsertData(
                table: "Museums",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 7, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7528), false, null, "LeTize Museum" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7658), false, null, "Director" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7662), false, null, "Restaurator Sheef" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 3, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7664), false, null, "Catalog Manager" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 4, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7666), false, null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 5, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7668), false, null, "Visiter" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8211), false, null, "Davinci" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8213), false, null, "Gallery" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Name" },
                values: new object[] { 3, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8215), false, null, "Galileo" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7944), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 2, "The tapice 1" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7973), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 3, "The tapice 2" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 3, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7986), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 1, "The tapice 3" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 4, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7999), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 2, "The tapice 4" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 5, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8011), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 3, "The tapice 5" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 6, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8024), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 1, "The tapice 6" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 7, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8035), 10m, "Unknown", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, null, "It is just biuriful.", "", new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ooold", 2, "The tapice 7" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 10, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7929), 1m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:331/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Grant-Wood-American-Gothic.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Renacence", 2, "American Gothic" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 11, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7914), 2m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:592/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Georges-Seurat-A-Sunday-Afternoon-on-the-Island-of-La-Grande-Jatte.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Renacence", 3, "A Sunday Afternoon on the Island of La Grande Jatte" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 12, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7833), 3m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:413/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Claude-Monet-Water-Lilies.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Renacence", 1, "Water Lilies" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 13, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7822), 4m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:398/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Gustav-Klimt-The-Kiss-Bacio-.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Renacence", 2, "The Kiss (Bacio)" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 14, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7811), 5m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:492/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Rembrandt_Van_Rijn-Night_Watch_.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 3, "Night Watch" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 15, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7799), 6m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:347/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Jan-Vermeer-The-Girl-with-a-Pearl-Earring.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Renacence", 1, "The Girl with a Pearl Earring" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 16, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7787), 7m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:443/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/PabloPicasso-ThreeMusicians.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Renacence", 2, "Three Musicians" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 17, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7776), 8m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:316/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Edvard-Munch-The-Scream.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Renacence", 3, "The Scream" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 18, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7764), 9m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:500/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Vincent-Van-Gogh-Starry-Night.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Renacence", 1, "Starry-Night" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 19, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7745), 10m, "Author", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "https://mljecheddetu.i.optimole.com/r7ifq_I.35uE~1b08/w:265/h:400/q:mauto/https://www.justincanvas.com/wp-content/uploads/2020/03/Leonardo-Da-Vinci-Mona-Lisa.jpg", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Renacence", 2, "Mona Lisa (La Gioconda)" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 20, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8051), 1m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 3, "Monalisa20" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 21, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8067), 2m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 1, "Monalisa21" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 22, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8079), 3m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 2, "Monalisa22" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 23, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8091), 4m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 3, "Monalisa23" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 24, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8102), 5m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 1, "Monalisa24" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 25, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8115), 6m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 2, "Monalisa25" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 26, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8126), 7m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 3, "Monalisa26" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 27, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8173), 8m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 1, "Monalisa27" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 28, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8186), 9m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 2, "Monalisa28" });

            migrationBuilder.InsertData(
                table: "Artworks",
                columns: new[] { "Id", "AddTime", "Assessment", "Author", "CreationDate", "CurrentSatus", "Deleted", "DeletedTime", "Description", "Images", "IncorporatedDate", "MuseumId", "Period", "RoomId", "Title" },
                values: new object[] { 29, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8199), 10m, "Leonardo da Vinci", new DateTime(1503, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, false, null, "Estalin Disima", "", new DateTime(2002, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Renacence", 3, "Monalisa29" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8227), false, null, "example@gmail.com", "admin.psw123//", 4, "Foreman Administrator" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8230), false, null, "example@gmail.com", "manager.psw123//", 3, "Lorena Manager" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 3, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8233), false, null, "example@gmail.com", "restaurator.psw123//", 2, "Juan Restaurator" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 4, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8235), false, null, "example@gmail.com", "director.psw123//", 1, "Harvey Director" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 5, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8237), false, null, "example@gmail.com", "restaurator.psw123//", 2, "Pablo Restaurator" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "Email", "Password", "RoleId", "Username" },
                values: new object[] { 6, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(8239), false, null, "example@gmail.com", "manager.psw123//", 3, "Dorian Manager" });

            migrationBuilder.InsertData(
                table: "LoanApplications",
                columns: new[] { "Id", "AddTime", "ApplicationDate", "ArtworkId", "CurrentStatus", "Deleted", "DeletedTime", "Duration", "MuseumId" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7708), new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, false, null, 10, 1 });

            migrationBuilder.InsertData(
                table: "LoanApplications",
                columns: new[] { "Id", "AddTime", "ApplicationDate", "ArtworkId", "CurrentStatus", "Deleted", "DeletedTime", "Duration", "MuseumId" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7712), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, false, null, 10, 1 });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 10, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 11, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 12, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 13, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 14, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 15, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 16, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 17, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 18, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Paintings",
                columns: new[] { "Id", "Media", "Style" },
                values: new object[] { 19, "address", "Renacence" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 20, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 21, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 22, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 23, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 24, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 25, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 26, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 27, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 28, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Sculptures",
                columns: new[] { "Id", "Material", "Style" },
                values: new object[] { 29, "Gold", "Style" });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "LoanApplicationId", "PaymentAmount", "StartDate" },
                values: new object[] { 1, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7685), false, null, 1, 10m, new DateTime(2022, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "AddTime", "Deleted", "DeletedTime", "LoanApplicationId", "PaymentAmount", "StartDate" },
                values: new object[] { 2, new DateTime(2022, 6, 27, 16, 23, 28, 614, DateTimeKind.Local).AddTicks(7691), false, null, 2, 20m, new DateTime(2022, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_MuseumId",
                table: "Artworks",
                column: "MuseumId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_RoomId",
                table: "Artworks",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ArtworkId",
                table: "LoanApplications",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_MuseumId",
                table: "LoanApplications",
                column: "MuseumId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_LoanApplicationId",
                table: "Loans",
                column: "LoanApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restorations_ArtworkId",
                table: "Restorations",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Paintings");

            migrationBuilder.DropTable(
                name: "Restorations");

            migrationBuilder.DropTable(
                name: "Sculptures");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "Museums");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
