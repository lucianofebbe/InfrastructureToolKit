namespace InfrastructureToolKit.Bases.Entities
{
    /// <summary>
    /// Classe base para entidades armazenadas no Redis, estendendo BaseEntities.
    /// </summary>
    public class BaseEntitiesRedisDb : BaseEntities
    {
        /// <summary>
        /// Identificador único da entidade no Redis, usando Guid.
        /// </summary>
        public Guid Guid { get; set; }
    }
}
