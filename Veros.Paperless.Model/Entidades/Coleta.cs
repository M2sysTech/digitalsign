namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;
    using Iesi.Collections.Generic;
    using System.Collections.Generic;

    public class Coleta : Entidade
    {
        public Coleta()
        {
            this.Pacotes = new List<Pacote>();
        }

        public virtual DateTime Data { get; set; }

        public virtual string Endereco { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Periodo { get; set; }

        public virtual int QuantidadeDeCaixasTipo1 { get; set; }

        public virtual int QuantidadeDeCaixasTipo2 { get; set; }

        public virtual Usuario UsuarioCadastro { get; set; }

        public virtual DateTime DataCadastro { get; set; }

        public virtual ColetaStatus Status { get; set; }

        public virtual Usuario UsuarioRealizaColeta { get; set; }

        public virtual DateTime? DataColetaRealizada { get; set; }

        public virtual DateTime? DataDevolucao { get; set; }

        public virtual List<Pacote> Pacotes { get; set; }

        public virtual DateTime? DataRecepcao { get; set; }

        public virtual Usuario UsuarioRecepcao { get; set; }

        public virtual Transportadora Transportadora { get; set; }

        public virtual string Uf { get; set; }

        public virtual string Arquivo { get; set; }

        public virtual int TransportadoraId
        {
            get
            {
                return this.Transportadora != null ? this.Transportadora.Id : 0;
            }

            set
            {
                if (value > 0)
                {
                    this.Transportadora = new Transportadora { Id = value };
                }
            }
        }

        public virtual string Identificacao()
        {
            return string.Format("{0} - {1} ({2})", this.Id, this.Descricao, this.Data.ToString("dd/MM/yyyy"));
        }

        public virtual int TotalDossies()
        {
            return this.QuantidadeDeCaixasTipo1 + this.QuantidadeDeCaixasTipo2;
        }

        public virtual string ColetasPendentes()
        {
            return string.Format("{0} - {1}", this.Id, this.Data.ToString("dd/MM/yyyy"));
        }
    }
}
