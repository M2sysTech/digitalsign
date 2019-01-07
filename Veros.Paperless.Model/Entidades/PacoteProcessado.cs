namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class PacoteProcessado : Entidade
    {
        public PacoteProcessado()
        {
            this.Lotes = new List<Lote>();
        }

        public virtual string Arquivo
        {
            get; 
            set;
        }

        public virtual DateTime? ArquivoRecebidoEm
        {
            get; 
            set; 
        }

        public virtual DateTime? EnviadoEm
        {
            get; 
            set;
        }

        public virtual DateTime? ArquivoImportadoEm
        {
            get; 
            set;
        }

        public virtual int ContaRecepcionadas
        {
            get; 
            set;
        }

        public virtual StatusPacote StatusPacote
        {
            get; 
            set;
        }

        public virtual PacoteProcessadoFaturado Faturado
        {
            get;
            set;
        }

        public virtual DateTime? FimRecepcao
        {
            get;
            set;
        }

        public virtual DateTime? UltimoArquivoRecebido
        {
            get;
            set;
        }

        public virtual List<Lote> Lotes
        {
            get;
            set;
        }

        public virtual string Ativado
        {
            get;
            set;
        }

        public virtual string ExibirNaQualidadeCef
        {
            get;
            set;
        }

        public virtual string DescricaoStatusPacote()
        {
            switch (this.StatusPacote)
            {
                case StatusPacote.Processado:
                    return "Processado";
                case StatusPacote.Cancelado:
                    return "Cancelado";
                case StatusPacote.Pendente:
                    return "Pendente";
            }

            return "Em processamento";
        }

        public virtual bool DeveExibirNaQualidadeCef()
        {
            return this.ExibirNaQualidadeCef == "S";
        }
    }
}