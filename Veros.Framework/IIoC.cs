//-----------------------------------------------------------------------
// <copyright file="IIoC.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IIoC
    {
        IEnumerable<T> GetAllInstances<T>();

        IEnumerable GetAllInstance(Type type);

        object Resolve(Type type);

        T Resolve<T>();

        T Resolve<T>(string namedInstance);

        void BuildUp(object obj);

        void RegisterDependencies(
            List<string> assembliesPreffixes,
            List<string> excludes,
            string dependencyPlugin);

        void Inject<T>(object instance);
    }
}
