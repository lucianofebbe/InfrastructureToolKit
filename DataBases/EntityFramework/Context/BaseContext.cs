using InfrastructureToolKit.Interfaces.DataBase.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureToolKit.DataBase.EntityFramework.Context
{
    // Contexto base para EF Core, implementa interface IContext
    public class BaseContext : DbContext, IContext
    {
        // Construtor protegido recebe opções de configuração do DbContext
        protected BaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
