using InfrastructureToolKit.Interfaces.Schedulers.Quartz.Scheduler;
using InfrastructureToolKit.Schedulers.Quartz.Settings;

namespace InfrastructureToolKit.Interfaces.Schedulers.Quartz.SchedulerFactory
{
    // Interface para fábrica que cria instâncias de IScheduler
    public interface ISchedulerFactory
    {
        // Cria um IScheduler com configurações padrão
        Task<InterfaceScheduler> Create();

        // Cria um IScheduler com configurações específicas de persistência
        Task<InterfaceScheduler> Create(PersistenceSettings persistenceSettings);
    }
}
