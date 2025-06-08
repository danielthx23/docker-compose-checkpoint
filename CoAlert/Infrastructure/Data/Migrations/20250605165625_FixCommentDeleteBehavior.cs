using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoAlert.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CA_COMENTARIO_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                table: "CA_COMENTARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_CA_COMENTARIO_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                table: "CA_COMENTARIO",
                column: "ID_COMENTARIO_PARENTE",
                principalTable: "CA_COMENTARIO",
                principalColumn: "ID_COMENTARIO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CA_COMENTARIO_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                table: "CA_COMENTARIO");

            migrationBuilder.AddForeignKey(
                name: "FK_CA_COMENTARIO_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                table: "CA_COMENTARIO",
                column: "ID_COMENTARIO_PARENTE",
                principalTable: "CA_COMENTARIO",
                principalColumn: "ID_COMENTARIO",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
