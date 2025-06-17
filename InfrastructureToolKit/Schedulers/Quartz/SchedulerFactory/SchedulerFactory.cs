using InfrastructureToolKit.Interfaces.Schedulers.Quartz.Scheduler;
using InfrastructureToolKit.Interfaces.Schedulers.Quartz.SchedulerFactory;
using InfrastructureToolKit.Settings.Schedulers.Quartz.Settings;

namespace InfrastructureToolKit.Schedulers.Quartz.SchedulerFactory
{
    // Fábrica responsável por criar instâncias do agendador (Scheduler)
    public class SchedulerFactory : ISchedulerFactory
    {
        // Cria uma instância padrão do Scheduler (em memória)
        public async Task<InterfaceScheduler> Create()
        {
            InterfaceScheduler fac = new Scheduler.Scheduler();
            return fac;
        }

        // Cria uma instância do Scheduler com configurações de persistência personalizadas
        public async Task<InterfaceScheduler> Create(PersistenceSettings persistenceSettings)
        {
            InterfaceScheduler fac = new Scheduler.Scheduler(persistenceSettings);
            return fac;
        }
    }
}
