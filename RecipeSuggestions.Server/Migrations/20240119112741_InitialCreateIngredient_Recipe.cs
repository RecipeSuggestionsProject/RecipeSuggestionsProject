using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSuggestions.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateIngredient_Recipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient_Recipe",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    QuantityType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient_Recipe", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipe_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Recipe_IngredientId",
                table: "Ingredient_Recipe",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredient_Recipe");
        }
    }
}
