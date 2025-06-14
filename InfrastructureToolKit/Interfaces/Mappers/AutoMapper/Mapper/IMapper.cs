using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    // Interface genérica para mapeamento entre entidades SQL, MongoDB, Redis,
    // além de DTOs de Request e Response, com suporte a operações assíncronas.
    [Obsolete("Use IMapperSpecific em vez de IMapper.")]
    public interface IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>
        where EntitySql : BaseEntitiesSql
        where EntityMongoDb : BaseEntitiesMongoDb
        where EntityRedisDb : BaseEntitiesRedisDb
        where Request : BaseRequest
        where Response : BaseResponse
    {
        #region EntitySql and Request
        Task<EntitySql> RequestToEntitySqlAsync(Request item);
        Task<List<EntitySql>> RequestToEntitySqlAsync(List<Request> item);
        Task<Request> EntitySqlToRequestAsync(EntitySql item);
        Task<List<Request>> EntitySqlToRequestAsync(List<EntitySql> item);
        #endregion

        #region EntitySql and Response
        Task<EntitySql> ResponseToEntitySqlAsync(Response item);
        Task<List<EntitySql>> ResponseToEntitySqlAsync(List<Response> item);
        Task<Response> EntitySqlToResponseAsync(EntitySql item);
        Task<List<Response>> EntitySqlToResponseAsync(List<EntitySql> item);
        #endregion

        #region EntitySql and EntityMongoDb
        Task<EntityMongoDb> EntitySqlToEntityMongoDbAsync(EntitySql item);
        Task<List<EntityMongoDb>> EntitySqlToEntityMongoDbAsync(List<EntitySql> item);
        Task<EntitySql> EntityMongoDbToEntitySqlAsync(EntityMongoDb item);
        Task<List<EntitySql>> EntityMongoDbToEntitySqlAsync(List<EntityMongoDb> item);
        #endregion

        #region EntitySql and EntityRedisDb
        Task<EntityRedisDb> EntitySqlToEntityRedisDbAsync(EntitySql item);
        Task<List<EntityRedisDb>> EntitySqlToEntityRedisDbAsync(List<EntitySql> item);
        Task<EntitySql> EntityRedisDbToEntitySqlAsync(EntityRedisDb item);
        Task<List<EntitySql>> EntityRedisDbToEntitySqlAsync(List<EntityRedisDb> item);
        #endregion

        #region EntitySql JsonSerializer
        Task<string> EntitySqlToJsonAsync(EntitySql item);
        Task<string> EntitySqlToJsonAsync(List<EntitySql> item);
        Task<EntitySql> JsonToEntitySqlAsync(string item);
        Task<List<EntitySql>> JsonToEntitySqlListAsync(string item);
        #endregion

        #region EntityMongoDb and Request
        Task<EntityMongoDb> RequestToEntityMongoDbAsync(Request item);
        Task<List<EntityMongoDb>> RequestToEntityMongoDbAsync(List<Request> item);
        Task<Request> EntityMongoDbToRequestAsync(EntityMongoDb item);
        Task<List<Request>> EntityMongoDbToRequestAsync(List<EntityMongoDb> item);
        #endregion

        #region EntityMongoDb and Response
        Task<EntityMongoDb> ResponseToEntityMongoDbAsync(Response item);
        Task<List<EntityMongoDb>> ResponseToEntityMongoDbAsync(List<Response> item);
        Task<Response> EntityMongoDbToResponseAsync(EntityMongoDb item);
        Task<List<Response>> EntityMongoDbToResponseAsync(List<EntityMongoDb> item);
        #endregion

        #region EntityMongoDb and EntityRedisDb
        Task<EntityMongoDb> EntityRedisDbToEntityMongoDbAsync(EntityRedisDb item);
        Task<List<EntityMongoDb>> EntityRedisDbToEntityMongoDbAsync(List<EntityRedisDb> item);
        Task<EntityRedisDb> EntityMongoDbToEntityRedisDbAsync(EntityMongoDb item);
        Task<List<EntityRedisDb>> EntityMongoDbToEntityRedisDbAsync(List<EntityMongoDb> item);
        #endregion

        #region EntityMongoDb JsonSerializer
        Task<string> EntityMongoDbToJsonAsync(EntityMongoDb item);
        Task<string> EntityMongoDbToJsonAsync(List<EntityMongoDb> item);
        Task<EntityMongoDb> JsonToEntityMongoDbAsync(string item);
        Task<List<EntityMongoDb>> JsonToEntityMongoDbListAsync(string item);
        #endregion

        #region EntityRedisDb and Request
        Task<EntityRedisDb> RequestToEntityRedisDbAsync(Request item);
        Task<List<EntityRedisDb>> RequestToEntityRedisDbAsync(List<Request> item);
        Task<Request> EntityRedisDbToRequestAsync(EntityRedisDb item);
        Task<List<Request>> EntityRedisDbToRequestAsync(List<EntityRedisDb> item);
        #endregion

        #region EntityRedisDb and Response
        Task<EntityRedisDb> ResponseToEntityRedisDbAsync(Response item);
        Task<List<EntityRedisDb>> ResponseToEntityRedisDbAsync(List<Response> item);
        Task<Response> EntityRedisDbToResponseAsync(EntityRedisDb item);
        Task<List<Response>> EntityRedisDbToResponseAsync(List<EntityRedisDb> item);
        #endregion

        #region EntityRedisDb JsonSerializer
        Task<string> EntityRedisDbToJsonAsync(EntityRedisDb item);
        Task<string> EntityRedisDbToJsonAsync(List<EntityRedisDb> item);
        Task<EntityRedisDb> JsonToEntityRedisDbAsync(string item);
        Task<List<EntityRedisDb>> JsonToEntityRedisDbListAsync(string item);
        #endregion

        #region Request and Response
        Task<Request> ResponseToRequestAsync(Response item);
        Task<List<Request>> ResponseToRequestAsync(List<Response> item);
        Task<Response> RequestToResponseAsync(Request item);
        Task<List<Response>> RequestToResponseAsync(List<Request> item);
        Task<string> RequestToJsonAsync(Request item);
        Task<string> RequestToJsonAsync(List<Request> item);
        Task<Request> JsonToRequestAsync(string item);
        Task<List<Request>> JsonToRequestListAsync(string item);
        Task<string> ResponseToJsonAsync(Response item);
        Task<string> ResponseToJsonAsync(List<Response> item);
        Task<Response> JsonToResponseAsync(string item);
        Task<List<Response>> JsonToResponseListAsync(string item);
        #endregion
    }
}
