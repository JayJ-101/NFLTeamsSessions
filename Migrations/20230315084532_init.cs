using Microsoft.EntityFrameworkCore.Migrations;

namespace NFLTeamsSessions.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    ConferenceID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.ConferenceID);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    DivisionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.DivisionID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConferenceID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DivisionID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_Teams_Conferences_ConferenceID",
                        column: x => x.ConferenceID,
                        principalTable: "Conferences",
                        principalColumn: "ConferenceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "DivisionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Conferences",
                columns: new[] { "ConferenceID", "Name" },
                values: new object[,]
                {
                    { "afc", "AFC" },
                    { "nfc", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "DivisionID", "Name" },
                values: new object[,]
                {
                    { "north", "North" },
                    { "south", "South" },
                    { "east", "East" },
                    { "west", "West" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamID", "ConferenceID", "DivisionID", "Name" },
                values: new object[,]
                {
                    { "bal", "afc", "north", "Baltimore Ravens" },
                    { "oak", "afc", "west", "Oakland Raiders" },
                    { "lar", "nfc", "west", "Los Angeles Rams" },
                    { "lac", "afc", "west", "Los Angeles Chargers" },
                    { "kc", "afc", "west", "Kansas City Chiefs" },
                    { "den", "afc", "west", "Denver Broncos" },
                    { "ari", "nfc", "west", "Arizona Cardinals" },
                    { "was", "nfc", "east", "Washington Redskins" },
                    { "phi", "nfc", "east", "Philadelphia Eagles" },
                    { "nyj", "afc", "east", "New York Jets" },
                    { "nyg", "nfc", "east", "New York Giants" },
                    { "ne", "afc", "east", "New England Patriots" },
                    { "mia", "afc", "east", "Miami Dolphins" },
                    { "dal", "nfc", "east", "Dallas Cowboys" },
                    { "buf", "afc", "east", "Buffalo Bills" },
                    { "ten", "afc", "south", "Tennessee Titans" },
                    { "tb", "nfc", "south", "Tampa Bay Buccaneers" },
                    { "no", "nfc", "south", "New Orleans Saints" },
                    { "jax", "afc", "south", "Jacksonville Jaguars" },
                    { "ind", "afc", "south", "Indianapolis Colts" },
                    { "hou", "afc", "south", "Houston Texans" },
                    { "car", "nfc", "south", "Carolina Panthers" },
                    { "atl", "nfc", "south", "Atlanta Falcons" },
                    { "pit", "afc", "north", "Pittsburgh Steelers" },
                    { "min", "nfc", "north", "Minnesota Vikings" },
                    { "gb", "nfc", "north", "Green Bay Packers" },
                    { "det", "nfc", "north", "Detroit Lions" },
                    { "cle", "afc", "north", "Cleveland Browns" },
                    { "cin", "afc", "north", "Cincinnati Bengals" },
                    { "chi", "nfc", "north", "Chicago Bears" },
                    { "sea", "nfc", "west", "Seattle Seahawks" },
                    { "sf", "nfc", "west", "San Francisco 49ers" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ConferenceID",
                table: "Teams",
                column: "ConferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DivisionID",
                table: "Teams",
                column: "DivisionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Divisions");
        }
    }
}
