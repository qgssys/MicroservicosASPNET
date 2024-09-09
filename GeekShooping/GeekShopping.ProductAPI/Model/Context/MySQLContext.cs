using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(){}

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options){}

        // DbSet adiciona a tabela ao contexto
        public DbSet<Product> Products { get; set;}

        ///Para adicionar no banco de dados execute o migrations no package manager console executando o comando:
        ///add-migration AddProductDataTableOnDB  (comando mais nome da migration)


    }
}
