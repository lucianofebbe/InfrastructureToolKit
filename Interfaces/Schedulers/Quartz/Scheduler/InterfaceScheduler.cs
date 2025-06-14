using InfrastructureToolKit.Schedulers.Quartz.Settings;

namespace InfrastructureToolKit.Interfaces.Schedulers.Quartz.Scheduler
{
    // Interface para controle de agendamentos Quartz.NET
    public interface InterfaceScheduler
    {
        // Inicia e agenda uma coleção de jobs
        Task StartJobAsync(IEnumerable<IJobTaskSettings> jobs);

        // Para um job específico pelo nome
        Task StopJobAsync(string jobName);

        // Pausa a execução de um job específico pelo nome
        Task PauseJobAsync(string jobName);

        // Retoma a execução de um job pausado pelo nome
        Task ResumeJobAsync(string jobName);
    }
}
