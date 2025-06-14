using System.Globalization;

namespace InfrastructureToolKit.Bases.Entities
{
    /// <summary>
    /// Classe base para entidades do domínio, com propriedades comuns e métodos auxiliares para manipulação de strings.
    /// </summary>
    public class BaseEntities
    {
        /// <summary>
        /// Data e hora de criação da entidade.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Data e hora da última atualização da entidade.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Indica se a entidade foi marcada como deletada (soft delete).
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Normaliza uma string de entrada removendo espaços em branco e convertendo para minúsculas, usando cultura invariante.
        /// </summary>
        /// <param name="input">String que pode ser nula ou vazia.</param>
        /// <returns>String normalizada ou string vazia caso a entrada seja nula ou contenha apenas espaços.</returns>
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
