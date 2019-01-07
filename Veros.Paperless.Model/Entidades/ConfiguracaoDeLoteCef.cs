namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class ConfiguracaoDeLoteCef : Entidade
    {
        public virtual DateTime? HoraFechamento { get; set; }

        public virtual int QuantidadeMinima { get; set; }

        public virtual int QuantidadeMaxima { get; set; }
    }
}
