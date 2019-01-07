//-----------------------------------------------------------------------
// <copyright file="IService.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    /// <summary>
    /// Contrato serviços que podem ser iniciados e parados
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Inicia serviço
        /// </summary>
        void Start();

        /// <summary>
        /// Para serviço
        /// </summary>
        void Stop();
    }
}
