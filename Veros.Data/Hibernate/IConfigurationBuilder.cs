//-----------------------------------------------------------------------
// <copyright file="IConfigurationBuilder.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    public interface IConfigurationBuilder
    {
        IHibernateConfiguration Build();
    }
}
