using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.Cap7.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertaAndColetaAndResiduoAndPontoDescarte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontosDeDescarte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QuantidadeAtualKg = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    CapacidadeMaximaKg = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosDeDescarte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residuos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    Peso = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DataColeta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residuos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coletas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataHora = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Quantidade = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    PontoDeDescarteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ResiduoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coletas_PontosDeDescarte_PontoDeDescarteId",
                        column: x => x.PontoDeDescarteId,
                        principalTable: "PontosDeDescarte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coletas_Residuos_ResiduoId",
                        column: x => x.ResiduoId,
                        principalTable: "Residuos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resolvido = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    PontoDeDescarteId = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ColetaId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Coletas_ColetaId",
                        column: x => x.ColetaId,
                        principalTable: "Coletas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Alertas_PontosDeDescarte_PontoDeDescarteId",
                        column: x => x.PontoDeDescarteId,
                        principalTable: "PontosDeDescarte",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_ColetaId",
                table: "Alertas",
                column: "ColetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_PontoDeDescarteId",
                table: "Alertas",
                column: "PontoDeDescarteId");

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_PontoDeDescarteId",
                table: "Coletas",
                column: "PontoDeDescarteId");

            migrationBuilder.CreateIndex(
                name: "IX_Coletas_ResiduoId",
                table: "Coletas",
                column: "ResiduoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Coletas");

            migrationBuilder.DropTable(
                name: "PontosDeDescarte");

            migrationBuilder.DropTable(
                name: "Residuos");
        }
    }
}
