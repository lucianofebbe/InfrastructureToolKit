using InfrastructureToolKit.Bases.Entities;
using System.Linq.Expressions;

namespace InfrastructureToolKit.DataBase.EntityFramework.Settings
{
    public record CommandSettings<T> where T : BaseEntitiesSql
    {
        public T Entity { get; set; }
        public Expression<Func<T, bool>> Predicate { get; set; }
        public bool NoTracking { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool? Deleteds { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
