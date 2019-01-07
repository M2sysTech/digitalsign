//-----------------------------------------------------------------------
// <copyright file="IDatabaseProvider.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data
{
    using FluentNHibernate.Cfg.Db;

    public interface IDatabaseProvider
    {
        string Name
        {
            get;
        }

        bool ShouldExtractDriver
        {
            get;
        }

        bool SupportBatch
        {
            get;
        }

        string ParameterChar
        {
            get;
        }

        string True();

        string False();

        IPersistenceConfigurer GetHibernateConfiguration();
        
        void GetDriver();

        ConnectionString ParseConnectionString(string connectionString);

        string GetDateTimeQuery();
    }
}
