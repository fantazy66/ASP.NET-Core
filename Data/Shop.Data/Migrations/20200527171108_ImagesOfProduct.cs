using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class ImagesOfProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "ArtProducts");

            migrationBuilder.CreateTable(
                name: "ImageofProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(nullable: true),
                    ArtProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageofProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageofProducts_ArtProducts_ArtProductId",
                        column: x => x.ArtProductId,
                        principalTable: "ArtProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageofProducts_ArtProductId",
                table: "ImageofProducts",
                column: "ArtProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageofProducts");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "ArtProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
