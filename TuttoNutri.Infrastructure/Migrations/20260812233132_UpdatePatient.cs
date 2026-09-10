using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuttoNutri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Patient_PatientId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Nutritionist_NutritionistId",
                table: "Patient");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Patient",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Patient_PatientId",
                table: "Consultation",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Nutritionist_NutritionistId",
                table: "Patient",
                column: "NutritionistId",
                principalTable: "Nutritionist",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Patient_PatientId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Nutritionist_NutritionistId",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patient");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Patient_PatientId",
                table: "Consultation",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Nutritionist_NutritionistId",
                table: "Patient",
                column: "NutritionistId",
                principalTable: "Nutritionist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
