using System.Globalization;

namespace InfrastructureToolKit.Bases.Dtos
{
    /// <summary>
    /// Classe base para respostas, contendo métodos auxiliares para normalização e formatação de strings.
    /// </summary>
    public record BaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        /// <summary>
        /// Normaliza a entrada removendo espaços em branco e convertendo para minúsculas usando cultura invariante.
        /// </summary>
        /// <param name="input">String de entrada que pode ser nula ou vazia.</param>
        /// <returns>String normalizada (com trim e em minúsculas), ou string vazia se a entrada for nula ou whitespace.</returns>
        protected static string NormalizeInput(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            return input.Trim().ToLowerInvariant();
        }

        /// <summary>
        /// Capitaliza a primeira letra de uma string, respeitando a cultura atual.
        /// </summary>
        /// <param name="value">String de entrada.</param>
        /// <returns>String com a primeira letra em maiúscula e o restante da string inalterado.</returns>
        protected static string CapitalizeFirstLetter(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return char.ToUpper(value[0], CultureInfo.CurrentCulture) + value[1..];
        }
    }
}
