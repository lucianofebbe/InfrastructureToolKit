using MongoDB.Bson;

namespace InfrastructureToolKit.Bases.Entities
{
    /// <summary>
    /// Classe base para entidades armazenadas no MongoDB, estendendo BaseEntities.
    /// </summary>
    public class BaseEntitiesMongoDb : BaseEntities
    {
        /// <summary>
        /// Identificador único do documento no MongoDB.
        /// </summary>
        public ObjectId Id { get; set; }
        public Guid Guid { get; set; }
    }
}
