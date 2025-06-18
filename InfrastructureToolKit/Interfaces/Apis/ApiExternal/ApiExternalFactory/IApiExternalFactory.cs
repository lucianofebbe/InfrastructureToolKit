using InfrastructureToolKit.Interfaces.Apis.ApiExternal.ApiExternal;

namespace InfrastructureToolKit.Interfaces.Apis.ApiExternal.ApiExternalFactory
{
    // Interface para fábrica de instâncias da interface IApiExternal<T>
    public interface IApiExternalFactory<T> where T : class
    {
        // Cria uma instância de IApiExternal<T> com HttpClient interno/configuração padrão
        Task<IApiExternal<T>> Create(string baseAddress);
        // Cria uma instância de IApiExternal<T> usando um HttpClient fornecido externamente
        Task<IApiExternal<T>> Create(HttpClient httpClient);
    }
}
