using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperEntitySqlResponse<EntitySql, Response>
        where EntitySql : BaseEntitiesSql
        where Response : BaseResponse
    {
        Task<EntitySql> ResponseToEntitySqlAsync(Response item);
        Task<List<EntitySql>> ResponseToEntitySqlAsync(List<Response> item);
        Task<Response> EntitySqlToResponseAsync(EntitySql item);
        Task<List<Response>> EntitySqlToResponseAsync(List<EntitySql> item);
    }
}
