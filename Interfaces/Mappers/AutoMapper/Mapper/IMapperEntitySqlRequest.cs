using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperEntitySqlRequest<EntitySql, Request>
        where EntitySql : BaseEntitiesSql
        where Request : BaseRequest
    {
        Task<EntitySql> RequestToEntitySqlAsync(Request item);
        Task<List<EntitySql>> RequestToEntitySqlAsync(List<Request> item);
        Task<Request> EntitySqlToRequestAsync(EntitySql item);
        Task<List<Request>> EntitySqlToRequestAsync(List<EntitySql> item);
    }
}
