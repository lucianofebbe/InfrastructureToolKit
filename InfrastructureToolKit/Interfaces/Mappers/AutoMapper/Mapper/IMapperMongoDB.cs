using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperMongoDB<EntitySql, EntityMongoDb> 
        where EntitySql : BaseEntitiesSql
        where EntityMongoDb : BaseEntitiesMongoDb
    {
        Task<EntityMongoDb> EntitySqlToEntityMongoDbAsync(EntitySql item);
        Task<List<EntityMongoDb>> EntitySqlToEntityMongoDbAsync(List<EntitySql> item);
        Task<EntitySql> EntityMongoDbToEntitySqlAsync(EntityMongoDb item);
        Task<List<EntitySql>> EntityMongoDbToEntitySqlAsync(List<EntityMongoDb> item);
    }
}
