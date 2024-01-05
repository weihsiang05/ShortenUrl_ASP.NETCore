using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShortenURL.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OriginalUrl",
                columns: table => new
                {
                    OriginalLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OriginalLink = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OriginalUrl", x => x.OriginalLinkId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NewUrl",
                columns: table => new
                {
                    NewLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NewLink = table.Column<string>(type: "longtext", nullable: false),
                    OriginalLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUrl", x => x.NewLinkId);
                    table.ForeignKey(
                        name: "FK_NewUrl_OriginalUrl_OriginalLinkId",
                        column: x => x.OriginalLinkId,
                        principalTable: "OriginalUrl",
                        principalColumn: "OriginalLinkId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NewUrl_OriginalLinkId",
                table: "NewUrl",
                column: "OriginalLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_OriginalUrl_OriginalLink",
                table: "OriginalUrl",
                column: "OriginalLink",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewUrl");

            migrationBuilder.DropTable(
                name: "OriginalUrl");
        }
    }
}
