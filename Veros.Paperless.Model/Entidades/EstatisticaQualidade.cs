namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class EstatisticaQualidade : Entidade
    {
        public EstatisticaQualidade()
        {
            this.DataRegistro = DateTime.Today;
        }

        public virtual Usuario Usuario { get; set; }

        public virtual DateTime DataRegistro { get; set; }

        public virtual int TotalOk { get; set; }

        public virtual int TotalNaoOk { get; set; }

        public virtual int Producao
        {
            get
            {
                return this.TotalOk + this.TotalNaoOk;
            }
        }
    }
}