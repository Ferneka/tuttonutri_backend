using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuttoNutri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEvaluationDateColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EvaluationDate",
                table: "MedicalRecord",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EvaluationDate",
                table: "MedicalRecord",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
