using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuttoNutri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConsultation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Nutritionist_NutritionistId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_Consultation_ConsultationId",
                table: "MedicalRecord");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecord_ConsultationId",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "MinuteDuration",
                table: "Consultation");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Consultation");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Consultation",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_ConsultationId",
                table: "MedicalRecord",
                column: "ConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Nutritionist_NutritionistId",
                table: "Consultation",
                column: "NutritionistId",
                principalTable: "Nutritionist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_Consultation_ConsultationId",
                table: "MedicalRecord",
                column: "ConsultationId",
                principalTable: "Consultation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultation_Nutritionist_NutritionistId",
                table: "Consultation");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_Consultation_ConsultationId",
                table: "MedicalRecord");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecord_ConsultationId",
                table: "MedicalRecord");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Consultation",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "MinuteDuration",
                table: "Consultation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Consultation",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_ConsultationId",
                table: "MedicalRecord",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultation_Nutritionist_NutritionistId",
                table: "Consultation",
                column: "NutritionistId",
                principalTable: "Nutritionist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_Consultation_ConsultationId",
                table: "MedicalRecord",
                column: "ConsultationId",
                principalTable: "Consultation",
                principalColumn: "Id");
        }
    }
}
