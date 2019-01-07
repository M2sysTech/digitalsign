//-----------------------------------------------------------------------
// <copyright file="IExecutable.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Service
{
    /// <summary>
    /// Contrato para classes que executam uma tarefa
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Executa tarefa
        /// </summary>
        void Execute();
    }
}
