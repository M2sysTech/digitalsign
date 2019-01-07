namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class ComparaBio : Entidade
    {
        public virtual string Status { get; set; }

        public virtual string Resultado { get; set; }

        public virtual DateTime? HoraInicio { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual double Percentual { get; set; }

        public virtual Lote Lote { get; set; }

        public virtual Face Face1 { get; set; }

        public virtual Face Face2 { get; set; }
    }
}
