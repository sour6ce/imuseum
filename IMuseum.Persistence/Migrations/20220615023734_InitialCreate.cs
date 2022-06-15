using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMuseum.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendMuseums",
                columns: table => new
                {
                    FriendMuseumId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendMuseums", x => x.FriendMuseumId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Account = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AddDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Period = table.Column<string>(type: "TEXT", nullable: false),
                    Assessment = table.Column<decimal>(type: "TEXT", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_Artwork_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "ArtworkInPosess ",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkInPosess ", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_ArtworkInPosess _Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplication",
                columns: table => new
                {
                    LoanApplicationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MuseumFriendMuseumId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplication", x => x.LoanApplicationId);
                    table.ForeignKey(
                        name: "FK_LoanApplication_ArtworkInPosess _ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "ArtworkInPosess ",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanApplication_FriendMuseums_MuseumFriendMuseumId",
                        column: x => x.MuseumFriendMuseumId,
                        principalTable: "FriendMuseums",
                        principalColumn: "FriendMuseumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlasticArt ",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlasticArt ", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_PlasticArt _ArtworkInPosess _ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "ArtworkInPosess ",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restorations",
                columns: table => new
                {
                    RestorationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restorations", x => x.RestorationId);
                    table.ForeignKey(
                        name: "FK_Restorations_ArtworkInPosess _ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "ArtworkInPosess ",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ApplicationLoanApplicationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_LoanApplication_ApplicationLoanApplicationId",
                        column: x => x.ApplicationLoanApplicationId,
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Painting ",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Painting ", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_Painting _PlasticArt _ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "PlasticArt ",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sculpture ",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sculpture ", x => x.ArtworkId);
                    table.ForeignKey(
                        name: "FK_Sculpture _PlasticArt _ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "PlasticArt ",
                        principalColumn: "ArtworkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_RoomId",
                table: "Artwork",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_ArtworkId",
                table: "LoanApplication",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplication_MuseumFriendMuseumId",
                table: "LoanApplication",
                column: "MuseumFriendMuseumId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApplicationLoanApplicationId",
                table: "Loans",
                column: "ApplicationLoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Restorations_ArtworkId",
                table: "Restorations",
                column: "ArtworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Painting ");

            migrationBuilder.DropTable(
                name: "Restorations");

            migrationBuilder.DropTable(
                name: "Sculpture ");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LoanApplication");

            migrationBuilder.DropTable(
                name: "PlasticArt ");

            migrationBuilder.DropTable(
                name: "FriendMuseums");

            migrationBuilder.DropTable(
                name: "ArtworkInPosess ");

            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
