using InfrastructureToolKit.DataBase.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using ServerVersion = Microsoft.EntityFrameworkCore.ServerVersion;
namespace InfrastructureToolKit.Util.ContextOptions
{
    /// <summary>
    /// Essa classe possui implementacao dos seguintes drivers
    /// Microsoft.EntityFrameworkCore(8.0.6)
    /// Microsoft.EntityFrameworkCore.SqLite(8.0.16)
    /// Microsoft.EntityFrameworkCore.SqlServer(8.0.16)
    /// Microsoft.EntityFrameworkCore.PostgreSql(8.0.11)
    /// Microsoft.EntityFrameworkCore.MySql(8.0.3)
    /// Para acesso a dos a banco de dados que nao estao disponiveis aqui,
    /// implemente manualmente.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class ContextOptions<TContext> where TContext : BaseContext
    {
        /// <summary>
        /// Esse metodo possui implementacao dos seguintes drivers
        /// Microsoft.EntityFrameworkCore(8.0.6)
        /// Microsoft.EntityFrameworkCore.SqLite(8.0.16)
        /// Microsoft.EntityFrameworkCore.SqServer(8.0.16)
        /// Microsoft.EntityFrameworkCore.PostgreSql(8.0.11)
        /// Microsoft.EntityFrameworkCore.MySql(8.0.3)
        /// Para acesso a dos a banco de dados que nao estao disponiveis aqui,
        /// implemente manualmente.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        public DbContextOptions Create(Provider provider, string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            switch (provider)
            {
                case Provider.sqlserver:
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case Provider.postgresql:
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                case Provider.mysql:
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                    break;
                case Provider.sqlite:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
                default:
                    throw new NotSupportedException($"Provider '{provider}' não suportado.");
            }

            return optionsBuilder.Options;
        }

        public enum Provider
        {
            sqlserver,
            postgresql,
            mysql,
            sqlite
        }
    }
}
