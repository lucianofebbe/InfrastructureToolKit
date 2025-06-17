using Microsoft.EntityFrameworkCore;

namespace InfrastructureToolKit.Settings.DataBases.EntityFramework.Settings
{
    public record ConnectionSettings
    {
        public DbContext Context { get; set; }
        public bool BeginTransactionAsync { get; set; }
    }
}
