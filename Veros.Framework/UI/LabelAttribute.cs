//-----------------------------------------------------------------------
// <copyright file="LabelAttribute.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//     Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.UI
{
    using System;

    /// <summary>
    /// Atributo para indicar nome do label de um campo em um formulário
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LabelAttribute : Attribute
    {
        /// <summary>
        /// Valor do label
        /// </summary>
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the LabelAttribute class
        /// </summary>
        /// <param name="value">Valor do label</param>
        public LabelAttribute(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets valor do label
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }
    }
}