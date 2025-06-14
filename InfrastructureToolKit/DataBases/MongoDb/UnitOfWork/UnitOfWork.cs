using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBase.MongoDb.Settings;
using InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWork;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InfrastructureToolKit.DataBase.MongoDb.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : BaseEntitiesMongoDb
    {
        // Configurações do MongoDB (string de conexão, banco, coleção)
        private readonly ConnectionSettings config;
        // Instância do banco de dados MongoDB
        private readonly IMongoDatabase database;

        // Construtor que recebe as configurações e inicializa a conexão com o banco
        public UnitOfWork(ConnectionSettings config)
        {
            this.config = config;
            // Obtem o banco de dados de forma síncrona via Result
            database = GetDatabase().Result;
        }

        // Exclui (deleta) um documento baseado no Id do modelo informado
        public virtual async Task<bool> DeleteAsync(CommandSettings<T> CommandSettings)
        {
            var collection = GetCollection().Result;
            FilterDefinition<T> filter;

            if (CommandSettings.Entity.Id == ObjectId.Empty)
                filter = Builders<T>.Filter.Eq("Guid", CommandSettings.Entity.Guid);
            else
                filter = Builders<T>.Filter.Eq("_id", CommandSettings.Entity.Id);

            CommandSettings.FilterDefinition = filter;

            var item = await GetAsync(CommandSettings);
            if (item != null)
            {
                CommandSettings.Entity = item;
                CommandSettings.Entity.Deleted = true;
                var result = await UpdateAsync(CommandSettings);
                return result;
            }
            return false;
        }

        // Retorna o primeiro documento que satisfaça o filtro informado
        public virtual async Task<T> GetAsync(CommandSettings<T> CommandSettings)
        {
            var collection = await GetCollection();
            var documento = collection.Find(CommandSettings.FilterDefinition).FirstOrDefault(CommandSettings.CancellationToken);
            return await Task.FromResult(documento);
        }

        // Retorna todos os documentos que satisfaçam o filtro, ou todos caso filtro seja nulo
        public virtual async Task<List<T>> GetAllAsync(CommandSettings<T> CommandSettings)
        {
            var result = new List<T>();
            var collection = database.GetCollection<T>(config.Collection);

            var query = CommandSettings.FilterDefinition != null
                    ? collection.Find(CommandSettings.FilterDefinition)
                    : collection.Find(FilterDefinition<T>.Empty);

            if (CommandSettings.SortDefinition != null)
                query = query.Sort(CommandSettings.SortDefinition);

            if (CommandSettings.Skip.HasValue)
                query = query.Skip(CommandSettings.Skip);

            if (CommandSettings.Take.HasValue)
                query = query.Limit(CommandSettings.Take);

            result.AddRange(await query.ToListAsync());

            return result;
        }

        // Insere um novo documento na coleção, gerando novo ObjectId para o modelo
        public virtual async Task<T> InsertAsync(CommandSettings<T> CommandSettings)
        {
            var collection = database.GetCollection<T>(config.Collection);
            CommandSettings.Entity.Id = ObjectId.GenerateNewId();
            CommandSettings.Entity.Guid = Guid.NewGuid();
            CommandSettings.Entity.Created = DateTime.UtcNow;
            CommandSettings.Entity.Updated = DateTime.UtcNow;
            collection.InsertOne(CommandSettings.Entity);
            return CommandSettings.Entity;
        }

        // Atualiza um documento existente, identificando pelo Id
        public virtual async Task<bool> UpdateAsync(CommandSettings<T> CommandSettings)
        {
            var collection = database.GetCollection<T>(config.Collection);
            FilterDefinition<T> filter;

            if (CommandSettings.Entity.Id == ObjectId.Empty)
                filter = Builders<T>.Filter.Eq("Guid", CommandSettings.Entity.Guid);
            else
                filter = Builders<T>.Filter.Eq("_id", CommandSettings.Entity.Id);

            CommandSettings.Entity.Updated = DateTime.UtcNow;
            return await Task.FromResult(collection.ReplaceOne(filter, CommandSettings.Entity).IsAcknowledged);
        }

        // Retorna a coleção do tipo T configurada
        private Task<IMongoCollection<T>> GetCollection()
        {
            var collection = database.GetCollection<T>(config.Collection);
            return Task.FromResult(collection);
        }

        // Inicializa o cliente Mongo e retorna o banco de dados configurado
        private Task<IMongoDatabase> GetDatabase()
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            return Task.FromResult(database);
        }
    }
}
