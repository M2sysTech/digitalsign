//-----------------------------------------------------------------------
// <copyright file="EnviromentExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework
{
    using System;

    /// <summary>
    /// Extens�o da classe Enviroment
    /// </summary>
    public class EnviromentExtension
    {
        /// <summary>
        /// Retorna caminho onde est� o .NET Framework instalado
        /// </summary>
        /// <returns>caminho onde est� o .NET Framework instalado</returns>
        public static string GetFrameworkPath()
        {
            return System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        }
    }
}
