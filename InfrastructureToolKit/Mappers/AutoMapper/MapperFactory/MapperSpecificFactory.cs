using AutoMapper;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.MapperFactory;
using InfrastructureToolKit.Mappers.AutoMapper.Mapper;

namespace InfrastructureToolKit.Mappers.AutoMapper.MapperFactory
{
    public class MapperSpecificFactory<Destiny, Origin> : IMapperSpecificFactory<Destiny, Origin>
        where Destiny : class
        where Origin : class
    {
        public async Task<IMapperSpecific<Destiny, Origin>> Create(string assemblyName, string nameSpaceFilter)
        {
            var profiles = await LoadProfiles(assemblyName, nameSpaceFilter);
            return await Create(profiles);
        }

        public async Task<IMapperSpecific<Destiny, Origin>> Create(IEnumerable<Profile> profiles)
        {
            IMapperSpecific<Destiny, Origin> mapper = new MapperSpecific<Destiny, Origin>(profiles);
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
