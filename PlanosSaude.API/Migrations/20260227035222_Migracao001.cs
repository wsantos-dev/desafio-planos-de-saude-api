using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanosSaude.API.Migrations
{
    /// <inheritdoc />
    public partial class Migracao001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "beneficiarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    data_nascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    telefone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    is_ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_criacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_beneficiarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    custo_mensal = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    cobertura = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_planos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contratacoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    beneficiario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plano_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_inicio = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    data_fim = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contratacoes", x => x.id);
                    table.ForeignKey(
                        name: "fk_contratacoes_beneficiarios_beneficiario_id",
                        column: x => x.beneficiario_id,
                        principalTable: "beneficiarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contratacoes_planos_plano_id",
                        column: x => x.plano_id,
                        principalTable: "planos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_beneficiarios_cpf",
                table: "beneficiarios",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contratacoes_beneficiario_id",
                table: "contratacoes",
                column: "beneficiario_id");

            migrationBuilder.CreateIndex(
                name: "ix_contratacoes_plano_id",
                table: "contratacoes",
                column: "plano_id");

            migrationBuilder.CreateIndex(
                name: "ix_planos_codigo",
                table: "planos",
                column: "codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contratacoes");

            migrationBuilder.DropTable(
                name: "beneficiarios");

            migrationBuilder.DropTable(
                name: "planos");
        }
    }
}
