using InfrastructureToolKit.Interfaces.Apis.ApiExternal;
using InfrastructureToolKit.Interfaces.Apis.ApiExternalFactory;

namespace InfrastructureToolKit.Apis.ApiExternalFactory
{
    /// <summary>
    /// Fábrica genérica para criação de instâncias de IApiExternal<T>.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade que a API externa manipula.</typeparam>
    public class ApiExternalFactory<T> : IApiExternalFactory<T> where T : class
    {
         /// <summary>
        /// Cria uma instância de IApiExternal<T> usando o HttpClient já configurado na fábrica.
        /// </summary>
        /// <returns>Instância de IApiExternal<T>.</returns>
        public virtual async Task<IApiExternal<T>> Create(string baseAddress)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
            IApiExternal<T> api = new ApiExternal<T>(httpClient);
            return api;
        }

        /// <summary>
        /// Cria uma instância de IApiExternal<T> usando o HttpClient fornecido no parâmetro.
        /// </summary>
        /// <param name="httpClient">HttpClient a ser usado pela instância criada.</param>
        /// <returns>Instância de IApiExternal<T>.</returns>
        public virtual async Task<IApiExternal<T>> Create(HttpClient httpClient)
        {
            IApiExternal<T> api = new ApiExternal<T>(httpClient);
            return api;
        }
    }
}
