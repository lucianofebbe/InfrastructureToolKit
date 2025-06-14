using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperEntitySqlRedisDB<EntitySql, EntityRedisDb>
        where EntitySql : BaseEntitiesSql
        where EntityRedisDb : BaseEntitiesRedisDb
    {
        Task<EntityRedisDb> EntitySqlToEntityRedisDbAsync(EntitySql item);
        Task<List<EntityRedisDb>> EntitySqlToEntityRedisDbAsync(List<EntitySql> item);
        Task<EntitySql> EntityRedisDbToEntitySqlAsync(EntityRedisDb item);
        Task<List<EntitySql>> EntityRedisDbToEntitySqlAsync(List<EntityRedisDb> item);
    }
}
