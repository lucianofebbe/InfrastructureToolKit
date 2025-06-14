using AutoMapper;
using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.MapperFactory
{
    // Interface genérica para fábrica de mapeadores que cria instâncias de IMapper
    // com base em configurações dinâmicas, como assembly, namespace ou perfis AutoMapper.
    [Obsolete("Use IMapperSpecificFactory em vez de IMapperFactory.")]
    public interface IMapperFactory<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>
        where EntitySql : BaseEntitiesSql
        where EntityMongoDb : BaseEntitiesMongoDb
        where EntityRedisDb : BaseEntitiesRedisDb
        where Request : BaseRequest
        where Response : BaseResponse
    {
        // Cria um mapper a partir do nome do assembly e um filtro de namespace.
        Task<IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>> Create(string assemblyName, string namespaceFilter);

        // Cria um mapper a partir de uma coleção de perfis AutoMapper.
        Task<IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>> Create(IEnumerable<Profile> profiles);
    }
}
