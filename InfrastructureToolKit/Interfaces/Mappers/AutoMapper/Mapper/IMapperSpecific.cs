namespace InfrastructureToolKit.Interfaces.Mappers.AutoMapper.Mapper
{
    public interface IMapperSpecific<Destiny, Origin>
        where Destiny : class
        where Origin : class
    {
        Task<Destiny> MapperAsync(Origin? item);
        Task<List<Destiny>> MapperAsync(List<Origin>? item);
        Task<Origin> MapperAsync(Destiny? item);
        Task<List<Origin>> MapperAsync(List<Destiny>? item);
    }
}
