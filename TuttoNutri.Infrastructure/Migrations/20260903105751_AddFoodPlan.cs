using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuttoNutri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Fiber",
                table: "FoodPlan",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    FoodPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meal_FoodPlan_FoodPlanId",
                        column: x => x.FoodPlanId,
                        principalTable: "FoodPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealFoodItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TacoId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Grams = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    KcalPer100g = table.Column<double>(type: "numeric(7,2)", nullable: false),
                    ProteinPer100g = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    FatPer100g = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    CarbohydratePer100g = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    FiberPer100g = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    MealId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFoodItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealFoodItem_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_FoodPlanId",
                table: "Meal",
                column: "FoodPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItem_MealId",
                table: "MealFoodItem",
                column: "MealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealFoodItem");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "FoodPlan");
        }
    }
}
