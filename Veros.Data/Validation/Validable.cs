//-----------------------------------------------------------------------
// <copyright file="Validable.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using System;
    using System.Runtime.Serialization;
    using Framework.Validation;
    using Veros.Framework;
    using Castle.Components.Validator;

    /// <summary>
    /// Base para validação
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class Validable : IValidable
    {
        static Validable()
        {
            Validable.ValidationRunner = IoC.Current.Resolve<IValidationRunner>();
        }

        /// <summary>
        /// Initializes a new instance of the Validable class
        /// </summary>
        protected Validable()
        {
            this.ValidationSummary = new ValidationSummary();
        }

        public static IValidationRunner ValidationRunner
        {
            get;
            set;
        }

        /// <summary>
        /// Gets resumo dos erros de validação
        /// TODO: auto mapper ignorar esta proprieadade automaticamente. Convention?
        /// </summary>
        [IgnoreDataMember]
        public virtual ValidationSummary ValidationSummary
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether é válido
        /// </summary>
        [IgnoreDataMember]
        public virtual bool IsValid
        {
            get { return this.ValidationSummary.ErrorsCount == 0; }
        }

        /// <summary>
        /// Gets a value indicating whether não é válido
        /// </summary>
        [IgnoreDataMember]
        public virtual bool IsNotValid
        {
            get { return this.IsValid == false; }
        }

        /// <summary>
        /// Valida o estado da entidade
        /// </summary>
        /// <returns>True se está válido</returns>
        public virtual bool Validate()
        {
            return this.Validate(RunWhen.Everytime);
        }

        public virtual bool Validate(RunWhen runWhen)
        {
            this.ThrowIfRunnerNotSet();
            this.RunValidation(runWhen);

            return this.ValidationSummary.HasErrors == false;
        }

        /// <summary>
        /// Abertura para entidades fazerem validações customizadas
        /// </summary>
        protected virtual void CustomValidation()
        {
        }

        private void RunValidation(RunWhen runWhen)
        {
            this.ValidationSummary = new ValidationSummary();

            if (Validable.ValidationRunner.IsValid(this, runWhen) == false)
            {
                this.ValidationSummary = Validable.ValidationRunner.GetSummary(this);
            }

            this.CustomValidation();
        }

        private void ThrowIfRunnerNotSet()
        {
            if (Validable.ValidationRunner == null)
            {
                Validable.ValidationRunner = new CastleValidationRunner();
                ////throw new InvalidOperationException("Não foi setado runner para validação. Para setar, instancie e preencha a propriedade ValidationRunnerProvider.Current");
            }
        }
    }
}