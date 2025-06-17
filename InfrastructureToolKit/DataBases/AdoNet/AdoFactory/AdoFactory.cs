using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.DataBases.AdoNet.Ado;
using InfrastructureToolKit.Interfaces.DataBase.AdoNet.Ado;
using InfrastructureToolKit.Interfaces.DataBase.AdoNet.AdoFactory;
using InfrastructureToolKit.Settings.DataBases.AdoNet.Settings;

namespace InfrastructureToolKit.DataBases.AdoNet.AdoFactory
{
    public class AdoFactory<T> : IAdoFactory<T> where T : BaseEntitiesSql
    {
         // Método que cria um IAdo<T> usando configurações passadas por parâmetro
        // Inicia transação se configurado para isso
        public virtual async Task<IAdo<T>> Create(ConnectionSettings connectionSettings)
        {
            IAdo<T> ado = new Ado<T>(connectionSettings);
            if (connectionSettings.EnableTransaction)
                await ado.BeginTransactionAsync();
            return ado;
        }
    }
}
