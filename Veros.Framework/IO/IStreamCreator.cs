//-----------------------------------------------------------------------
// <copyright file="IStreamCreator.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    using System.IO;

    /// <summary>
    /// Criador de stream
    /// </summary>
    public interface IStreamCreator
    {
        /// <summary>
        /// Cria o stream reader
        /// </summary>
        /// <param name="fileStream">Stream original</param>
        /// <returns>Um stream reader</returns>
        StreamReader CreateReader(System.IO.Stream fileStream);

        /// <summary>
        /// Cria o stream writer
        /// </summary>
        /// <param name="fileStream">Stream original</param>
        /// <returns>Um stream writer</returns>
        StreamWriter CreateWriter(System.IO.Stream fileStream);
    }
}