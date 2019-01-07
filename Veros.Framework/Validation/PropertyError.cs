//-----------------------------------------------------------------------
// <copyright file="PropertyError.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System;

    /// <summary>
    /// Erro de validação em propriedade
    /// </summary>
    [Serializable]
    public class PropertyError
    {
        /// <summary>
        /// Gets or sets propriedade
        /// </summary>
        public string Property
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets erro
        /// </summary>
        public string Error
        {
            get;
            set;
        }
    }
}
