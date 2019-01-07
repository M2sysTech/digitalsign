//-----------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Data.Validation
{
    using Castle.Components.Validator;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Representa um model viewmodel
    /// </summary>
    [Serializable]
    public abstract class ViewModel : Validable
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        public virtual bool IsNew
        {
            get { return this.Id.Equals(0); }
        }

        public override string ToString()
        {
            var @return = this.GetType().Name + "#" + this.Id;
            return this.Id.Equals(0) ? @return + "@" + this.GetHashCode() : @return;
        }

        public override bool Validate()
        {
            return base.Validate(this.IsNew ? RunWhen.Insert : RunWhen.Update);
        }
    }
}
