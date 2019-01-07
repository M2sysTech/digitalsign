//-----------------------------------------------------------------------
// <copyright file="EnviromentExtension.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework
{
    using System;

    /// <summary>
    /// Extensão da classe Enviroment
    /// </summary>
    public class EnviromentExtension
    {
        /// <summary>
        /// Retorna caminho onde está o .NET Framework instalado
        /// </summary>
        /// <returns>caminho onde está o .NET Framework instalado</returns>
        public static string GetFrameworkPath()
        {
            return System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        }
    }
}
