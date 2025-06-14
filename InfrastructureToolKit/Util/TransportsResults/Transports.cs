namespace InfrastructureToolKit.Util.TransportsResults
{
    public class TransportResult<T>
    {
        public string Message { get; private init; }
        public bool Success { get; private init; }
        public T? Item { get; private init; }
        public List<T>? Items { get; private init; }
        public int Page { get; private init; }
        public int PageSize { get; private init; }
        public int TotalItems { get; private init; }

        public int TotalPages =>
            PageSize > 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;

        private TransportResult() { } 

        public static TransportResult<T> Create(
            T? item,
            string? foundMessage = null,
            string? notFoundMessage = null)
        {
            var found = item != null;
            return new TransportResult<T>
            {
                Item = item,
                Success = found,
                Message = found
                    ? foundMessage ?? "Objeto encontrado com sucesso."
                    : notFoundMessage ?? "Objeto não encontrado."
            };
        }

        public static TransportResult<T> Create(
            List<T>? items,
            int page,
            int pageSize,
            int totalItems,
            string? foundMessage = null,
            string? notFoundMessage = null)
        {
            var found = items != null && items.Any();
            return new TransportResult<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Success = found,
                Message = found
                    ? foundMessage ?? "Página carregada com sucesso."
                    : notFoundMessage ?? "Nenhum item encontrado nesta página."
            };
        }
    }
}