using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class tablesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marka",
                columns: table => new
                {
                    MarkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAdı = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marka", x => x.MarkaId);
                });

            migrationBuilder.CreateTable(
                name: "Urun",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urun", x => x.UrunId);
                    table.ForeignKey(
                        name: "FK_Urun_Marka_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Marka",
                        principalColumn: "MarkaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fiyat",
                columns: table => new
                {
                    FiyatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiyat", x => x.FiyatId);
                    table.ForeignKey(
                        name: "FK_Fiyat_Urun_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urun",
                        principalColumn: "UrunId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fiyat_UrunId",
                table: "Fiyat",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_Urun_MarkaId",
                table: "Urun",
                column: "MarkaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fiyat");

            migrationBuilder.DropTable(
                name: "Urun");

            migrationBuilder.DropTable(
                name: "Marka");
        }
    }
}
