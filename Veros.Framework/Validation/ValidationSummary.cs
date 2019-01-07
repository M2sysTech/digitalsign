//-----------------------------------------------------------------------
// <copyright file="ValidationSummary.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Veros.Framework;

    /// <summary>
    /// Sumário de erro
    /// </summary>
    [Serializable]
    public class ValidationSummary
    {
        /// <summary>
        /// Erros nas propriedades
        /// </summary>
        private readonly IList<PropertyError> errors = new List<PropertyError>();

        /// <summary>
        /// Gets quantidade de erros
        /// </summary>
        [IgnoreDataMember]
        public int ErrorsCount
        {
            get { return this.errors.Count; }
        }

        /// <summary>
        ///  Gets a value indicating whether se há erro
        /// </summary>
        [IgnoreDataMember]
        public bool HasErrors
        {
            get { return this.ErrorsCount > 0; }
        }

        /// <summary>
        /// Gets erros nas propriedades
        /// </summary>
        [IgnoreDataMember]
        public IList<PropertyError> PropertyErrors
        {
            get { return this.errors; }
        }

        /// <summary>
        /// Gets propriedades com erro
        /// </summary>
        [IgnoreDataMember]
        public IList<string> Properties
        {
            get { return (from error in this.errors select error.Property).ToList(); }
        }

        /// <summary>
        /// Adiciona erro a uma propriedade
        /// </summary>
        /// <param name="property">Nome da propriedade</param>
        /// <param name="error">Mensagem de erro</param>
        public void Add(string property, string error)
        {
            this.errors.Add(new PropertyError { Property = property, Error = error });
        }

        /// <summary>
        /// Retorna os erros de uma propriedade
        /// </summary>
        /// <param name="property">Nome da propriedade</param>
        /// <returns>Erros da propriedade</returns>
        public IList<string> GetErrorsFor(string property)
        {
            return (from error in this.errors 
                where error.Property.Equals(property) 
                select error.Error).ToList();
        }

        /// <summary>
        /// Retorna mensagem de erro formatado
        /// </summary>
        public string GetFormatedMessage()
        {
            var formatedMessage = string.Empty;
            EnumerableExtension.ForEach(this.PropertyErrors, error => formatedMessage += error.Error + Environment.NewLine);
            return this.RemoveLastNewLine(formatedMessage);
        }

        public string GetFormatedMessageForProperty(string propertyName)
        {
            var formatedMessage = string.Empty;
            EnumerableExtension.ForEach(this.PropertyErrors, error =>
            {
                if (error.Property.Equals(propertyName))
                {
                    formatedMessage += error.Error + Environment.NewLine;
                }
            });

            return this.RemoveLastNewLine(formatedMessage);
        }

        private string RemoveLastNewLine(string message)
        {
            if (string.IsNullOrEmpty(message) == false && message.Length > 1)
            {
                return message.RemoveUltimoCaracter().RemoveUltimoCaracter();
            }

            return message;
        }
    }
}
