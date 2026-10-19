using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lucas.Migrations
{
    /// <inheritdoc />
    public partial class Folha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folhas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Mes = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    HorasTrabalhadas = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorHora = table.Column<int>(type: "INTEGER", nullable: false),
                    SalarioBruto = table.Column<float>(type: "REAL", nullable: false),
                    ImpostoRenda = table.Column<float>(type: "REAL", nullable: false),
                    Inss = table.Column<float>(type: "REAL", nullable: false),
                    Fgts = table.Column<float>(type: "REAL", nullable: false),
                    SalarioLiquido = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folhas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folhas");
        }
    }
}
