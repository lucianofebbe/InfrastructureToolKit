using InfrastructureToolKit.Interfaces.Schedulers.Quartz.Scheduler;
using InfrastructureToolKit.Schedulers.Quartz.Settings;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace InfrastructureToolKit.Schedulers.Quartz
{
    // Implementação do agendador de tarefas utilizando o Quartz.NET
    public class Scheduler : InterfaceScheduler
    {
        // Instância interna do agendador do Quartz
        private readonly IScheduler scheduler;

        // Propriedade pública que expõe a instância do agendador
        public IScheduler SchedulerPublic => scheduler;

        // Construtor que configura o agendador com persistência personalizada
        public Scheduler(PersistenceSettings persistenceSettings)
        {
            // Define as propriedades de configuração para o Quartz
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = persistenceSettings.InstanceName,
                ["quartz.jobStore.type"] = persistenceSettings.Type,
                ["quartz.jobStore.useProperties"] = persistenceSettings.UseProperties.ToString(),
                ["quartz.jobStore.dataSource"] = persistenceSettings.DataSource,
                ["quartz.jobStore.tablePrefix"] = persistenceSettings.TablePrefix,
                ["quartz.dataSource.default.connectionString"] = persistenceSettings.ConnectionString,
                ["quartz.dataSource.default.provider"] = persistenceSettings.Provider,
                ["quartz.serializer.type"] = persistenceSettings.SerializerType
            };

            // Cria a fábrica de agendadores com as configurações definidas
            var schedulerFactory = new StdSchedulerFactory(properties);

            // Obtém a instância do agendador de forma assíncrona
            scheduler = schedulerFactory.GetScheduler().Result;
        }

        // Construtor padrão que cria o agendador com configuração padrão (em memória)
        public Scheduler()
        {
            var schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler().Result;
        }

        // Inicia os jobs fornecidos com suas respectivas expressões Cron
        public virtual async Task StartJobAsync(IEnumerable<IJobTaskSettings> jobs)
        {
            foreach (var job in jobs)
            {
                var jobType = job.GetType();

                // Cria a definição do job com base no tipo
                var jobDetail = JobBuilder.Create(jobType)
                    .WithIdentity(jobType.Name)
                    .Build();

                // Cria o gatilho com base na expressão Cron do job
                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobType.Name}.trigger")
                    .WithCronSchedule(job.CronExpression)
                    .Build();

                // Agenda o job no Quartz
                await scheduler.ScheduleJob(jobDetail, trigger);
            }

            // Inicia o agendador
            await scheduler.Start();
        }

        // Remove um job agendado com base no nome
        public async Task StopJobAsync(string jobName)
        {
            var jobKey = new JobKey(jobName);
            await scheduler.DeleteJob(jobKey);
        }

        // Pausa a execução de um job com base no nome
        public async Task PauseJobAsync(string jobName)
        {
            var jobKey = new JobKey(jobName);
            await scheduler.PauseJob(jobKey);
        }

        // Retoma a execução de um job pausado com base no nome
        public async Task ResumeJobAsync(string jobName)
        {
            var jobKey = new JobKey(jobName);
            await scheduler.ResumeJob(jobKey);
        }
    }
}
