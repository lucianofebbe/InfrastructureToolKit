using System.Globalization;

namespace InfrastructureToolKit.Bases.Dtos
{
    /// <summary>
    /// Classe base para requisições, contendo métodos auxiliares para normalização e formatação de strings.
    /// </summary>
    public record BaseRequest
    {
        /// <summary>
        /// Normaliza a entrada removendo espaços em branco e convertendo para minúsculas invariant culture.
        /// </summary>
        /// <param name="input">String de entrada que pode ser nula ou vazia.</param>
        /// <returns>String normalizada (trimada e em minúsculas), ou string vazia se a entrada for nula ou whitespace.</returns>
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
        /// <returns>String com a primeira letra em maiúsculo e o restante mantido.</returns>
        protected static string CapitalizeFirstLetter(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return char.ToUpper(value[0], CultureInfo.CurrentCulture) + value[1..];
        }
    }
}
