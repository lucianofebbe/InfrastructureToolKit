namespace InfrastructureToolKit.Interfaces.Apis.ApiExternal.ApiExternal
{
    // Interface genérica para consumo de APIs externas
    public interface IApiExternal<T> where T : class
    {
        // Realiza uma requisição GET e retorna uma lista de objetos do tipo T
        Task<List<T>> GetListAsync(string relativeUrl);

        // Realiza uma requisição GET e retorna um único objeto do tipo T
        Task<T> GetAsync(string relativeUrl);

        // Realiza uma requisição GET e retorna a resposta em formato JSON (string)
        Task<string> GetJsonAsync(string relativeUrl);

        // Realiza uma requisição POST com o conteúdo especificado
        Task<HttpResponseMessage> PostAsync(string relativeUrl, HttpContent content);

        // Realiza uma requisição PUT com o conteúdo especificado
        Task<HttpResponseMessage> PutAsync(string relativeUrl, HttpContent content);

        // Realiza uma requisição DELETE com o conteúdo especificado
        Task<HttpResponseMessage> DeleteAsync(string relativeUrl, HttpContent content);
    }
}
