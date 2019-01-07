namespace Veros.Paperless.Model.Consultas
{
    using System;
    using Entidades;

    public class ContaLiberadaSemAprovacao
    {
        public string Conta
        {
            get;
            set;
        }

        public string Agencia
        {
            get;
            set;
        }

        public int PacoteId
        {
            get;
            set;
        }

        public int LoteId
        {
            get;
            set;
        }

        public DateTime LoteData
        {
            get;
            set;
        }

        public string ExtensaoRemessa
        {
            get;
            set;
        }

        public string DescricaoRetornoBanco
        {
            get
            {
                return this.ExtensaoRemessa != null ? RetornoBanco.ObterDescricao(this.ExtensaoRemessa) : string.Empty;
            }
        }
    }
}