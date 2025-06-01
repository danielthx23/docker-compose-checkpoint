using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoAlert.Migrations
{
    /// <inheritdoc />
    public partial class OracleDatabaseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CA_CATEGORIA_DESASTRE",
                columns: table => new
                {
                    ID_CATEGORIA_DESASTRE = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITULO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DS_CATEGORIA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_TIPO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_CATEGORIA_DESASTRE", x => x.ID_CATEGORIA_DESASTRE);
                });

            migrationBuilder.CreateTable(
                name: "CA_LOCALIZACAO",
                columns: table => new
                {
                    ID_LOCALIZACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_BAIRRO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_LOGRADOURO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NR_NUMERO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NM_CIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_ESTADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NR_CEP = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_PAIS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DS_COMPLEMENTO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_LOCALIZACAO", x => x.ID_LOCALIZACAO);
                });

            migrationBuilder.CreateTable(
                name: "CA_USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_USUARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NR_SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "CA_POSTAGEM",
                columns: table => new
                {
                    ID_POSTAGEM = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NM_TITULO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NM_CONTEUDO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DT_ENVIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    NR_LIKES = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CA_USUARIO_ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    CA_LOCALIZACAO_ID_LOCALIZACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_POSTAGEM", x => x.ID_POSTAGEM);
                    table.ForeignKey(
                        name: "FK_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                        column: x => x.CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE,
                        principalTable: "CA_CATEGORIA_DESASTRE",
                        principalColumn: "ID_CATEGORIA_DESASTRE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_POSTAGEM_CA_LOCALIZACAO_CA_LOCALIZACAO_ID_LOCALIZACAO",
                        column: x => x.CA_LOCALIZACAO_ID_LOCALIZACAO,
                        principalTable: "CA_LOCALIZACAO",
                        principalColumn: "ID_LOCALIZACAO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_POSTAGEM_CA_USUARIO_CA_USUARIO_ID_USUARIO",
                        column: x => x.CA_USUARIO_ID_USUARIO,
                        principalTable: "CA_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CA_COMENTARIO",
                columns: table => new
                {
                    ID_COMENTARIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_COMENTARIO_PARENTE = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    NM_CONTEUDO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DT_ENVIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    NR_LIKES = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    CA_POSTAGEM_ID_POSTAGEM = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_COMENTARIO", x => x.ID_COMENTARIO);
                    table.ForeignKey(
                        name: "FK_CA_COMENTARIO_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                        column: x => x.ID_COMENTARIO_PARENTE,
                        principalTable: "CA_COMENTARIO",
                        principalColumn: "ID_COMENTARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CA_COMENTARIO_CA_POSTAGEM_CA_POSTAGEM_ID_POSTAGEM",
                        column: x => x.CA_POSTAGEM_ID_POSTAGEM,
                        principalTable: "CA_POSTAGEM",
                        principalColumn: "ID_POSTAGEM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_COMENTARIO_CA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "CA_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CA_LIKE",
                columns: table => new
                {
                    ID_LIKE = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_POSTAGEM = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    ID_COMENTARIO = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    DT_LIKE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_LIKE", x => x.ID_LIKE);
                    table.CheckConstraint("CK_Like_PostOrComment", "(ID_POSTAGEM IS NULL AND ID_COMENTARIO IS NOT NULL) OR (ID_POSTAGEM IS NOT NULL AND ID_COMENTARIO IS NULL)");
                    table.ForeignKey(
                        name: "FK_CA_LIKE_CA_COMENTARIO_ID_COMENTARIO",
                        column: x => x.ID_COMENTARIO,
                        principalTable: "CA_COMENTARIO",
                        principalColumn: "ID_COMENTARIO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_LIKE_CA_POSTAGEM_ID_POSTAGEM",
                        column: x => x.ID_POSTAGEM,
                        principalTable: "CA_POSTAGEM",
                        principalColumn: "ID_POSTAGEM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CA_LIKE_CA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "CA_USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CA_COMENTARIO_CA_POSTAGEM_ID_POSTAGEM",
                table: "CA_COMENTARIO",
                column: "CA_POSTAGEM_ID_POSTAGEM");

            migrationBuilder.CreateIndex(
                name: "IX_CA_COMENTARIO_ID_COMENTARIO_PARENTE",
                table: "CA_COMENTARIO",
                column: "ID_COMENTARIO_PARENTE");

            migrationBuilder.CreateIndex(
                name: "IX_CA_COMENTARIO_ID_USUARIO",
                table: "CA_COMENTARIO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_CA_LIKE_ID_COMENTARIO",
                table: "CA_LIKE",
                column: "ID_COMENTARIO");

            migrationBuilder.CreateIndex(
                name: "IX_CA_LIKE_ID_POSTAGEM",
                table: "CA_LIKE",
                column: "ID_POSTAGEM");

            migrationBuilder.CreateIndex(
                name: "IX_CA_LIKE_ID_USUARIO",
                table: "CA_LIKE",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_CA_POSTAGEM_CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE",
                table: "CA_POSTAGEM",
                column: "CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE");

            migrationBuilder.CreateIndex(
                name: "IX_CA_POSTAGEM_CA_LOCALIZACAO_ID_LOCALIZACAO",
                table: "CA_POSTAGEM",
                column: "CA_LOCALIZACAO_ID_LOCALIZACAO");

            migrationBuilder.CreateIndex(
                name: "IX_CA_POSTAGEM_CA_USUARIO_ID_USUARIO",
                table: "CA_POSTAGEM",
                column: "CA_USUARIO_ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CA_LIKE");

            migrationBuilder.DropTable(
                name: "CA_COMENTARIO");

            migrationBuilder.DropTable(
                name: "CA_POSTAGEM");

            migrationBuilder.DropTable(
                name: "CA_CATEGORIA_DESASTRE");

            migrationBuilder.DropTable(
                name: "CA_LOCALIZACAO");

            migrationBuilder.DropTable(
                name: "CA_USUARIO");
        }
    }
}
