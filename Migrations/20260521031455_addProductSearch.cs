using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace InventorySystem.Migrations
{
    /// <inheritdoc />
    public partial class addProductSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVerctor",
                table: "products",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name", "sku" });

            migrationBuilder.CreateIndex(
                name: "IX_products_SearchVerctor",
                table: "products",
                column: "SearchVerctor")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_SearchVerctor",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SearchVerctor",
                table: "products");
        }
    }
}
