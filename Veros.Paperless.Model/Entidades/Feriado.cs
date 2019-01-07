namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class Feriado : Entidade
    {
        public virtual DateTime? Data
        {
            get; 
            set;
        }

        public virtual string Status
        {
            get; 
            set;
        }

        public virtual bool EhFeriado(DateTime data)
        {
            return data >= this.Data.GetValueOrDefault() && 
                data < this.Data.GetValueOrDefault().AddDays(1);
        }
    }
}