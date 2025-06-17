using Quartz;

namespace InfrastructureToolKit.Settings.Schedulers.Quartz.Settings
{
    // Interface que representa uma tarefa de job com agendamento por expressão Cron
    public interface IJobTaskSettings : IJob
    {
        // Expressão Cron que define a frequência de execução do job
        string CronExpression { get; }
    }
}
