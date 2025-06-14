using AutoMapper;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;

namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.MapperFactory
{
    public interface IMapperSpecificFactory<Destiny, Origin>
        where Destiny : class
        where Origin : class
    {
        // Cria um mapper a partir do nome do assembly e um filtro de namespace.
        Task<IMapperSpecific<Destiny, Origin>> Create(string assemblyName, string nameSpaceFilter);

        // Cria um mapper a partir de uma coleção de perfis AutoMapper.
        Task<IMapperSpecific<Destiny, Origin>> Create(IEnumerable<Profile> profiles);
    }
}
