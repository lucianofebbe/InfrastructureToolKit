﻿using InfrastructureToolKit.Bases.Entities;
using InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWork;
using InfrastructureToolKit.Settings.DataBases.MongoDb.Settings;

namespace InfrastructureToolKit.Interfaces.DataBase.MongoDb.UnitOfWorkFactory
{
    // Interface para fábrica de Unidade de Trabalho do MongoDB para entidades BaseEntitiesMongoDb
    public interface IUnitOfWorkFactory<T> where T : BaseEntitiesMongoDb
    {
        // Cria uma unidade de trabalho usando configurações específicas do MongoDB
        Task<IUnitOfWork<T>> Create(ConnectionSettings config);
    }
}
