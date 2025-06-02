using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoAlert.Migrations
{
    /// <inheritdoc />
    public partial class FixingFKNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CA_COMENTARIO_CA_POSTAGEM_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_LOCALIZACAO_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_USUARIO_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM");

            migrationBuilder.RenameColumn(
                name: "CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM",
                newName: "ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                newName: "ID_LOCALIZACAO");

            migrationBuilder.RenameColumn(
                name: "CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                newName: "ID_CATEGORIA_DESASTRE");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_ID_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_ID_LOCALIZACAO");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_ID_CATEGORIA_DESASTRE");

            migrationBuilder.RenameColumn(
                name: "CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                newName: "ID_POSTAGEM");

            migrationBuilder.RenameIndex(
                name: "IX_CA_COMENTARIO_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                newName: "IX_CA_COMENTARIO_ID_POSTAGEM");

            migrationBuilder.AddForeignKey(
                name: "FK_CA_COMENTARIO_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                column: "ID_POSTAGEM",
                principalTable: "CA_POSTAGEM",
                principalColumn: "ID_POSTAGEM",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                column: "ID_CATEGORIA_DESASTRE",
                principalTable: "CA_CATEGORIA_DESASTRE",
                principalColumn: "ID_CATEGORIA_DESASTRE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                column: "ID_LOCALIZACAO",
                principalTable: "CA_LOCALIZACAO",
                principalColumn: "ID_LOCALIZACAO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM",
                column: "ID_USUARIO",
                principalTable: "CA_USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CA_COMENTARIO_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM");

            migrationBuilder.DropForeignKey(
                name: "FK_CA_POSTAGEM_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM");

            migrationBuilder.RenameColumn(
                name: "ID_USUARIO",
                table: "CA_POSTAGEM",
                newName: "CA_USUARIO_ID_USUARIO");

            migrationBuilder.RenameColumn(
                name: "ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                newName: "CA_LOCALIZACAO_ID_LOCALIZACAO");

            migrationBuilder.RenameColumn(
                name: "ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                newName: "CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_ID_USUARIO",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_CA_USUARIO_ID_USUARIO");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_CA_LOCALIZACAO_ID_LOCALIZACAO");

            migrationBuilder.RenameIndex(
                name: "IX_CA_POSTAGEM_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                newName: "IX_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE");

            migrationBuilder.RenameColumn(
                name: "ID_POSTAGEM",
                table: "CA_COMENTARIO",
                newName: "CA_POSTAGEM_ID_POSTAGEM");

            migrationBuilder.RenameIndex(
                name: "IX_CA_COMENTARIO_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                newName: "IX_CA_COMENTARIO_CA_POSTAGEM_ID_POSTAGEM");

            migrationBuilder.AddForeignKey(
                name: "FK_CA_COMENTARIO_CA_POSTAGEM_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                column: "CA_POSTAGEM_ID_POSTAGEM",
                principalTable: "CA_POSTAGEM",
                principalColumn: "ID_POSTAGEM",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                column: "CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                principalTable: "CA_CATEGORIA_DESASTRE",
                principalColumn: "ID_CATEGORIA_DESASTRE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_LOCALIZACAO_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                column: "CA_LOCALIZACAO_ID_LOCALIZACAO",
                principalTable: "CA_LOCALIZACAO",
                principalColumn: "ID_LOCALIZACAO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CA_POSTAGEM_CA_USUARIO_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM",
                column: "CA_USUARIO_ID_USUARIO",
                principalTable: "CA_USUARIO",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
