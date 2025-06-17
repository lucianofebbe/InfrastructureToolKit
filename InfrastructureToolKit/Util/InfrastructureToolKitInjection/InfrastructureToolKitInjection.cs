using InfrastructureToolKit.Apis.ApiExternalFactory;
using InfrastructureToolKit.Bases.Dtos;
using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Cryptographies.CryptographyFactory;
using InfrastructureToolKit.Emails.EmailFactory;
using InfrastructureToolKit.Interfaces.Apis.ApiExternalFactory;
using InfrastructureToolKit.Interfaces.Cryptographies.CryptographyFactory;
using InfrastructureToolKit.Interfaces.Emails.EmailFactory;
using InfrastructureToolKit.Interfaces.Logs.LogFactory;
using InfrastructureToolKit.Interfaces.Mappers.AutoMapper.MapperFactory;
using InfrastructureToolKit.Interfaces.Messengers.kafkaMessageQueuing.Factory;
using InfrastructureToolKit.Interfaces.Messengers.RabbitMessageQueuing.Factory;
using InfrastructureToolKit.Interfaces.Schedulers.Quartz.SchedulerFactory;
using InfrastructureToolKit.Interfaces.Telemetrys.OpenTelemetry.TelemetryFactory;
using InfrastructureToolKit.Logs.LogFactory;
using InfrastructureToolKit.Mappers.AutoMapper.MapperFactory;
using InfrastructureToolKit.Messengers.kafkaMessageQueuing.kafkaMQFactory;
using InfrastructureToolKit.Messengers.RabbitMessageQueuing.RabbitMQFactory;
using InfrastructureToolKit.Schedulers.Quartz.SchedulerFactory;
using InfrastructureToolKit.Telemetrys.OpenTelemetry.TelemetryFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using InfrastructureToolKit.DataBases.RedisDb.UnitOfWorkFactory;
using InfrastructureToolKit.DataBases.EntityFramework.UnitOfWorkFactory;
using InfrastructureToolKit.DataBases.Dapper.UnitOfWorkFactory;
using InfrastructureToolKit.DataBases.MongoDb.UnitOfWorkFactory;


namespace InfrastructureToolKit.Util.InfrastructureToolKitInjection
{
    public static class InfrastructureToolKitInjection
    {
        public static IServiceCollection AddInfrastructureToolKitInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IApiExternalFactory<>), typeof(ApiExternalFactory<>));
            services.AddScoped(typeof(ICryptographyFactory), typeof(CryptographyFactory));
            services.AddScoped(typeof(Interfaces.DataBase.Dapper.UnitOfWorkFactory.IUnitOfWorkFactory<>), typeof(DataBases.Dapper.UnitOfWorkFactory.UnitOfWorkFactory<>));
            services.AddScoped(typeof(Interfaces.DataBase.EntityFramework.UnitOfWorkFactory.IUnitOfWorkFactory<>), typeof(DataBases.EntityFramework.UnitOfWorkFactory.UnitOfWorkFactory<>));
            services.AddScoped(typeof(Interfaces.DataBase.MongoDb.UnitOfWorkFactory.IUnitOfWorkFactory<>), typeof(DataBases.MongoDb.UnitOfWorkFactory.UnitOfWorkFactory<>));
            services.AddScoped(typeof(Interfaces.DataBase.RedisDb.UnitOfWorkFactory.IUnitOfWorkFactory<>), typeof(DataBases.RedisDb.UnitOfWorkFactory.UnitOfWorkFactory<>));
            services.AddScoped(typeof(IEmailFactory), typeof(EmailFactory));
            services.AddScoped(typeof(ILogFactory), typeof(LogFactory));
            services.AddScoped(typeof(IMapperFactory<BaseEntitiesSql, BaseEntitiesMongoDb, BaseEntitiesRedisDb, BaseRequest, BaseResponse>), typeof(MapperFactory<BaseEntitiesSql, BaseEntitiesMongoDb, BaseEntitiesRedisDb, BaseRequest, BaseResponse>));
            services.AddScoped(typeof(IMapperSpecificFactory<Object, Object>), typeof(MapperSpecificFactory<Object, Object>));
            services.AddSingleton(typeof(IkafkaMQFactory), typeof(kafkaMQFactory));
            services.AddSingleton(typeof(IRabbitMQFactory), typeof(RabbitMQFactory));
            services.AddSingleton(typeof(ISchedulerFactory), typeof(SchedulerFactory));
            services.AddSingleton(typeof(ITelemetryBuilderFactory), typeof(TelemetryBuilderFactory));

            return services;
        }
    }
}
