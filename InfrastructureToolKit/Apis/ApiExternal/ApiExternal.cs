using InfrastructureToolKit.Interfaces.Apis.ApiExternal;
using System.Text.Json;

namespace InfrastructureToolKit.Apis
{
    /// <summary>
    /// Implementação genérica para consumo de APIs externas via HTTP.
    /// Permite realizar operações GET, POST, PUT e DELETE utilizando HttpClient.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade a ser deserializada das respostas JSON.</typeparam>
    public class ApiExternal<T> : IApiExternal<T> where T : class
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// Construtor que recebe uma instância de HttpClient via injeção de dependência.
        /// </summary>
        /// <param name="httpClient">HttpClient configurado para realizar as chamadas HTTP.</param>
        public ApiExternal(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Realiza uma requisição GET para a URL relativa e retorna uma lista de objetos do tipo T.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <returns>Lista de objetos do tipo T deserializados do JSON retornado.</returns>
        public virtual async Task<List<T>> GetListAsync(string relativeUrl)
        {
            var result = new List<T>();
            var json = await GetJsonAsync(relativeUrl);
            result.AddRange(JsonSerializer.Deserialize<List<T>>(json));
            return result;
        }

        /// <summary>
        /// Realiza uma requisição GET para a URL relativa e retorna um objeto do tipo T.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <returns>Objeto do tipo T deserializado do JSON retornado.</returns>
        public virtual async Task<T> GetAsync(string relativeUrl)
        {
            var json = await GetJsonAsync(relativeUrl);
            return JsonSerializer.Deserialize<T>(json);
        }

        /// <summary>
        /// Realiza uma requisição GET para a URL relativa e retorna a resposta JSON como string.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <returns>JSON retornado pela API como string.</returns>
        public virtual async Task<string> GetJsonAsync(string relativeUrl)
        {
            var response = await _httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        /// <summary>
        /// Realiza uma requisição POST para a URL relativa com o conteúdo fornecido.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <param name="content">Conteúdo HTTP a ser enviado no corpo da requisição.</param>
        /// <returns>Resposta HTTP da requisição.</returns>
        public virtual async Task<HttpResponseMessage> PostAsync(string relativeUrl, HttpContent content)
        {
            var response = await _httpClient.PostAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Realiza uma requisição PUT para a URL relativa com o conteúdo fornecido.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <param name="content">Conteúdo HTTP a ser enviado no corpo da requisição.</param>
        /// <returns>Resposta HTTP da requisição.</returns>
        public virtual async Task<HttpResponseMessage> PutAsync(string relativeUrl, HttpContent content)
        {
            var response = await _httpClient.PostAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Realiza uma requisição DELETE para a URL relativa com o conteúdo fornecido.
        /// </summary>
        /// <param name="relativeUrl">URL relativa da API a ser chamada.</param>
        /// <param name="content">Conteúdo HTTP a ser enviado no corpo da requisição.</param>
        /// <returns>Resposta HTTP da requisição.</returns>
        public virtual async Task<HttpResponseMessage> DeleteAsync(string relativeUrl, HttpContent content)
        {
            var response = await _httpClient.PostAsync(relativeUrl, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
