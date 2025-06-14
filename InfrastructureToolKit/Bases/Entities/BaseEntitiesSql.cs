namespace InfrastructureToolKit.Bases.Entities
{
    /// <summary>
    /// Classe base para entidades SQL, estendendo BaseEntities.
    /// Contém identificadores típicos para bancos relacionais: Id inteiro e Guid.
    /// </summary>
    public class BaseEntitiesSql : BaseEntities
    {
        /// <summary>
        /// Identificador numérico incremental da entidade no banco SQL.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador único global (GUID) da entidade.
        /// </summary>
        public Guid Guid { get; set; }
    }
}
