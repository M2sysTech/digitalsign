namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    public class LoteCef : Entidade
    {
        public LoteCef()
        {
            this.Lotes = new List<Lote>();
        }

        public virtual DateTime DataCriacao { get; set; }

        public virtual DateTime? DataFim { get; set; }

        public virtual LoteCefStatus Status { get; set; }

        public virtual bool Visivel { get; set; }

        public virtual int QuantidadeDeLotes { get; set; }

        public virtual DateTime? DataAprovacao { get; set; }

        public virtual DateTime? DataGeracaoCertificado { get; set; }

        public virtual DateTime? DataAssinaturaCertificado { get; set; }

        public virtual Usuario UsuarioAprovou { get; set; }

        public virtual Usuario UsuarioGerou { get; set; }

        public virtual List<Lote> Lotes { get; set; }

        public static LoteCef Novo()
        {
            return new LoteCef
            {
                DataCriacao = DateTime.Now,
                Status = LoteCefStatus.Aberto,
            };
        }

        public virtual bool PodeSerFechado(ConfiguracaoDeLoteCef configuracao)
        {
            return DateTime.Now.Hour > configuracao.HoraFechamento.GetValueOrDefault().Hour && 
                this.QuantidadeDeLotes >= configuracao.QuantidadeMinima;
        }
    }
}
