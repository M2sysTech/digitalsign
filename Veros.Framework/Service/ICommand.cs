//-----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Service
{
    /// <summary>
    /// Interface para contrato de objetos que
    /// implementam o Command Pattern
    /// http://en.wikipedia.org/wiki/Command_pattern
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executa ação
        /// </summary>
        void Execute();
    }
}