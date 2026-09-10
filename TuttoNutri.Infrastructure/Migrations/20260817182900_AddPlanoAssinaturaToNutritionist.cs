using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuttoNutri.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanoAssinaturaToNutritionist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExpiracao",
                table: "Nutritionist",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlanoAtivo",
                table: "Nutritionist",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExpiracao",
                table: "Nutritionist");

            migrationBuilder.DropColumn(
                name: "PlanoAtivo",
                table: "Nutritionist");
        }
    }
}
