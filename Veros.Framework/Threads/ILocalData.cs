//-----------------------------------------------------------------------
// <copyright file="ILocalData.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Threads
{
    /// <summary>
    /// Contrato para armazenar dados de threads
    /// </summary>
    public interface ILocalData
    {
        int Count
        {
            get;
        }

        object this[string key]
        {
            get;
            set;
        }

        void Clear();

        bool ContainsKey(string key);
    }
}