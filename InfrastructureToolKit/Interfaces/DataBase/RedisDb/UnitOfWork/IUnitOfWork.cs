using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Settings.DataBases.RedisDb.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.RedisDb.UnitOfWork
{
    // Interface para Unidade de Trabalho no RedisDb para entidades BaseEntitiesRedisDb
    public interface IUnitOfWork<T> where T : BaseEntitiesRedisDb
    {
        Task<Guid> InsertAsync(CommandSettings<T> commandSettings);
        Task<bool> UpdateAsync(CommandSettings<T> commandSettings);
        Task<bool> DeleteAsync(CommandSettings<T> commandSettings);

        Task<T> GetAsync(CommandSettings<T> commandSettings);
    }
}
