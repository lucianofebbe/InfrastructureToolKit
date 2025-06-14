using InfrastructureToolKit.DataBase.EntityFramework.Context;

namespace InfrastructureToolKit.DataBase.EntityFramework.Settings
{
    public record ConnectionSettings
    {
        public BaseContext Context { get; set; }
        public bool BeginTransactionAsync { get; set; }
    }
}
