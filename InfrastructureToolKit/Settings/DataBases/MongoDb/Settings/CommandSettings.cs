using MongoDB.Driver;

namespace InfrastructureToolKit.Settings.DataBases.MongoDb.Settings
{
    public record CommandSettings<T> where T : Bases.Entities.BaseEntitiesMongoDb
    {
        public T Entity { get; set; }
        public FilterDefinition<T>? FilterDefinition  { get; set; }
        public SortDefinition<T>? SortDefinition { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
