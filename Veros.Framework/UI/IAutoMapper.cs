//-----------------------------------------------------------------------
// <copyright file="IAutoMapper.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.UI
{
    using System;

    /// <summary>
    /// Contrato para mapeador de objetos
    /// </summary>
    public interface IAutoMapper
    {
        object Map(Type sourceType, Type destinationType, object objectToMap);
    }
}