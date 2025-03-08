using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCookbook.Migrations
{
    public partial class UpdateDatabaseWithAllChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ✅ Zmiana Rating na int i ustawienie wartości domyślnej 1
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldPrecision: 2,
                oldScale: 1);

            // ✅ Dodanie UserId do Recipes
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true); // UserId może być NULL

            // ✅ Tworzenie indeksu dla UserId w Recipes
            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            // ✅ Dodanie klucza obcego UserId -> AspNetUsers
            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ❌ Usunięcie klucza obcego
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes");

            // ❌ Usunięcie indeksu UserId
            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes");

            // ❌ Usunięcie kolumny UserId
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recipes");

            // ❌ Cofnięcie zmiany Rating na decimal
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Recipes",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
