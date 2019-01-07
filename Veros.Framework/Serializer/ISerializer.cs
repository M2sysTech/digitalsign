//-----------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Serializer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contrato para serializadores
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializa um objeto para string
        /// </summary>
        /// <param name="value">Objeto a ser serializado</param>
        /// <returns>Objeto serializado em string</returns>
        string Serialize(object value);

        /// <summary>
        /// Deserializa uma string para objeto
        /// </summary>
        /// <param name="value">String a ser deserializada</param>
        /// <typeparam name="T">Tipo do objeto a ser deserializado</typeparam>
        /// <returns>Objeto deserializado</returns>
        T Deserialize<T>(string value);

        /// <summary>
        /// Deserializa uma string para objeto
        /// </summary>
        /// <param name="value">String a ser deserializada</param>
        /// <param name="type">Tipo do objeto</param>
        /// <returns>Objeto deserializado</returns>
        object Deserialize(string value, Type type);
    }
}
