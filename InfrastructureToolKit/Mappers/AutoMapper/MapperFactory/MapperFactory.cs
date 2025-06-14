using AutoMapper;
using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.MapperFactory;

namespace InfrastructureToolKit.Mappers.AutoMapper.MapperFactory
{
    // Fábrica genérica para criar mapeadores AutoMapper com perfis personalizados
    [Obsolete("Use MapperSpecificFactory em vez de MapperFactory.")]
    public class MapperFactory<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response> : IMapperFactory<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>
        where EntitySql : BaseEntitiesSql
        where EntityMongoDb : BaseEntitiesMongoDb
        where EntityRedisDb : BaseEntitiesRedisDb
        where Request : BaseRequest
        where Response : BaseResponse
    {
        // Cria o mapper carregando perfis via assembly e filtro de namespace
        public virtual async Task<IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>> Create(string assemblyName, string nameSpaceFilter)
        {
            var profiles = await LoadProfiles(assemblyName, nameSpaceFilter);
            return await Create(profiles);
        }

        // Cria o mapper a partir de uma lista de perfis
        public virtual async Task<IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>> Create(IEnumerable<Profile> profiles)
        {
            IMapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response> mapper =
                new Mapper.Mapper<EntitySql, EntityMongoDb, EntityRedisDb, Request, Response>(profiles);
            return mapper;
        }

        // Método privado para carregar perfis de uma assembly filtrando por namespace
        private async Task<List<Profile>> LoadProfiles(string assemblyName, string namespaceFilter)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                  .FirstOrDefault(a => a.GetName().Name == assemblyName);

            if (assembly == null)
                throw new Exception($"Assembly '{assemblyName}' não foi encontrado.");

            var profiles = assembly.GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t)
                            && t.IsClass
                            && !t.IsAbstract
                            && (namespaceFilter == null || t.Namespace?.Contains(namespaceFilter) == true))
                .Select(t => (Profile)Activator.CreateInstance(t))
                .Cast<Profile>()
                .ToList();

            return profiles;
        }
    }
}
