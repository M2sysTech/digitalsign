//-----------------------------------------------------------------------
// <copyright file="HibernateRepositoryConvention.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Hibernate
{
    using System;
    using Framework.Modelo;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using StructureMap;
    using StructureMap.Graph.Scanning;

    /// <summary>
    /// Mapeia a dependencia das interfaces IRepository para 
    /// repositórios utilizando Hibernate
    /// </summary>
    public class HibernateRepositoryConvention : IRegistrationConvention
    {
        /// <summary>
        /// Processa mapeamento
        /// </summary>
        /// <param name="type">Tipo sendo mapeado</param>
        /// <param name="registry">Grafo de objetos do assembly 'scaneado'</param>
        public void Process(Type type, Registry registry)
        {
            if (type.IsAbstract)
            {
                return;
            }

            var interfaceName = this.GetInterfaceName(type);
            var interfaceType = type.GetInterface(interfaceName);

            if (interfaceType != null)
            {
                registry.AddType(interfaceType, type);
            }
        }

        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
            {
                if (type.IsAbstract)
                {
                    return;
                }

                var interfaceName = this.GetInterfaceName(type);
                var interfaceType = type.GetInterface(interfaceName);

                if (interfaceType != null)
                {
                    registry.AddType(interfaceType, type);
                }
            }
        }

        private string GetInterfaceName(Type type)
        {
            if (type == typeof(IRepositorioUnico<>))
            {
                return "I" + type.Name.Replace("Hibernate", string.Empty);
            }

            return "I" + type.Name.Replace("Hibernate", string.Empty);
        }
    }
}