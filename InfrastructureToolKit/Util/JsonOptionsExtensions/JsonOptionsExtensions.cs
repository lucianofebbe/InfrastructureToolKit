using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Json;

namespace InfrastructureToolKit.Util.JsonOptionsExtensions
{
    public static class JsonOptionsExtensions
    {
        public static IServiceCollection AddSdkJsonSettings(this IServiceCollection services, Action<EnumSerializationSettings>? configure = null)
        {
            var settings = new EnumSerializationSettings();
            configure?.Invoke(settings);

            if (settings.SerializeEnumsAsString)
            {
                services.Configure<JsonOptions>(options =>
                {
                    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            }

            return services;
        }

        public record EnumSerializationSettings
        {
            public bool SerializeEnumsAsString { get; set; } = true;
        }
    }
}