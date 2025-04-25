using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendas_Site.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao)" +
                "VALUES('Normal','Lanche feito com ingredientes normais')");

            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao)" +
                "VALUES('Natural','Lanche feito com ingredientes naturais')");

            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao)" +
                "VALUES('Bebidas','Bebidas e refrescos')");

            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao)" +
                "VALUES('Doces','bolos, doces, pães doces e outras sobremesas')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
