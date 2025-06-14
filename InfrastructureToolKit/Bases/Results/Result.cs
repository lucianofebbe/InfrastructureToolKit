using InfrastructureToolKit.Bases.Dtos;

namespace InfrastructureToolKit.Bases.Results
{
    /// <summary>
    /// Representa o resultado de uma operação, contendo sucesso/fracasso, erros e um valor do tipo T.
    /// T deve herdar de BaseResponse.
    /// </summary>
    /// <typeparam name="T">Tipo da resposta esperada, derivado de BaseResponse.</typeparam>
    public record Result<T>(bool isSuccess, List<string> errors, T Value) where T : BaseResponse
    {
        /// <summary>
        /// Cria um resultado de sucesso com o valor fornecido e sem erros.
        /// </summary>
        /// <param name="value">Valor do resultado.</param>
        /// <returns>Instância de Result representando sucesso.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(true, new List<string>(), value);
        }

        /// <summary>
        /// Cria um resultado de falha com uma lista de erros fornecida como array de strings.
        /// </summary>
        /// <param name="errors">Erros que causaram a falha.</param>
        /// <returns>Instância de Result representando falha.</returns>
        public static Result<T> Failure(params string[] errors) =>
            new(false, errors.ToList(), default!);

        /// <summary>
        /// Cria um resultado de falha com uma lista de erros fornecida.
        /// </summary>
        /// <param name="errors">Lista de erros que causaram a falha.</param>
        /// <returns>Instância de Result representando falha.</returns>
        public static Result<T> Failure(List<string> errors) =>
            new(false, errors, default!);
    }
}
