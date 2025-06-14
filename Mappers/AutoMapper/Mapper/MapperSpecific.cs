using AutoMapper;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper;

namespace InfrastructureToolKit.Mappers.AutoMapper.Mapper
{
    public class MapperSpecific<Destiny, Origin> : IMapperSpecific<Destiny, Origin>
        where Destiny : class
        where Origin : class
    {

        private readonly IMapper mapper;

        // Construtor recebe perfis do AutoMapper para configuração das regras de mapeamento
        public MapperSpecific(IEnumerable<Profile> profiles)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            });

            mapper = config.CreateMapper();
        }

        public async Task<Destiny?> MapperAsync(Origin? item)
        {
            return item != null ? mapper.Map<Destiny>(item) : null;
        }

        public async Task<List<Destiny>?> MapperAsync(List<Origin>? item)
        {
            return item != null ? mapper.Map<List<Destiny>>(item) : null;
        }

        public async Task<Origin?> MapperAsync(Destiny? item)
        {
            return item != null ? mapper.Map<Origin>(item) : null;
        }

        public async Task<List<Origin>?> MapperAsync(List<Destiny>? item)
        {
            return item != null ? mapper.Map<List<Origin>>(item) : null;
        }
    }
}
