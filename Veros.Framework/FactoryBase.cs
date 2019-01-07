//-----------------------------------------------------------------------
// <copyright file="FactoryBase.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;

    public abstract class FactoryBase<T>
    {
        protected static T DefaultUnconfiguredState()
        {
            throw new Exception(typeof(T).Name + " not configured.");
        }
    }
}
