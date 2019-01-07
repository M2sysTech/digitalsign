namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    /// <summary>
    /// TODO: refatorar
    /// </summary>
    public class Faturamento : Entidade
    {
        public const int TipoFaturamentoPorArquivo = 0;
        public const int TipoFaturamentoDiario = 1;
        public const int TipoFaturamentoMensal = 2;

        public const int StatusFaturamentoPendente = 0;
        public const int StatusFaturamentoOk = 1;

        public Faturamento()
        {
            this.Status = Faturamento.StatusFaturamentoPendente;
        }

        public virtual DateTime? DataFaturamento
        {
            get; 
            set;
        }

        public virtual DateTime? EnvioFornecedor
        {
            get; 
            set;
        }

        public virtual DateTime? RecepcaoFornecedor
        {
            get; 
            set;
        }

        public virtual int TipoDeFaturamento
        {
            get; 
            set;
        }

        public virtual int TotalContasRecepcionadas
        {
            get; 
            set;
        }

        public virtual int ContasTratadasDentroDoSla2Horas
        {
            get; 
            set;
        }

        public virtual int ContasTratadasDentroDoSla19Horas
        {
            get;
            set;
        }

        public virtual int ContasTratadasDentroDoSlaApos17Horas
        {
            get;
            set;
        }

        public virtual double PercentualContasTratadasDentroDoSla
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasDentroDoSla2Horas);
            }
        }

        public virtual int ContasTratadasForaDoSla
        {
            get; 
            set;
        }

        public virtual double PercentualContasTratadasForaDoSla
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasForaDoSla);
            }
        }

        public virtual int ContasNaoTratadas
        {
            get; 
            set;
        }

        public virtual double PercentualContasNaoTratadas
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasNaoTratadas);
            }
        }

        public virtual int ContasTratadasComReconhecimentoAutomatico
        {
            get; 
            set;
        }

        public virtual double PercentualContasComReconhecimentoAutomatico
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasComReconhecimentoAutomatico);
            }
        }

        public virtual int ContasTratadasComAnaliseManual
        {
            get; 
            set;
        }

        public virtual double PercentualContasTratadasComAnaliseManual
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasComAnaliseManual);
            }
        }

        public virtual int ContasTratadasComIndicativoDeAlteracaoCadastral
        {
            get; 
            set;
        }

        public virtual double PercentualContasTratadasComIndicativoDeAlteracaoCadastral
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasComIndicativoDeAlteracaoCadastral);
            }
        }

        public virtual int ContasTratadasComAtuacaoDoBanco
        {
            get; 
            set;
        }

        public virtual double PercentualContasTratadasComAtuacaoDoBanco
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasTratadasComAtuacaoDoBanco);
            }
        }

        public virtual int ContasLiberadasDiretamente
        {
            get; 
            set;
        }

        public virtual double PercentualContasLiberadasDiretamente
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasLiberadasDiretamente);
            }
        }

        public virtual int ContasDevolvidas
        {
            get; 
            set;
        }

        public virtual double PercentualContasDevolvidas
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasDevolvidas);
            }
        }

        public virtual int ContasComIndicioDeFraude
        {
            get; 
            set;
        }

        public virtual double PercentualContasComIndicioDeFraude
        {
            get
            {
                return this.RetornaPercentualDoCampo(this.ContasComIndicioDeFraude);
            }
        }

        public virtual int Status
        {
            get; 
            set;
        }

        public virtual string NomeArquivo
        {
            get; 
            set;
        }

        private double RetornaPercentualDoCampo(double campo)
        {
            return campo == 0.0 ? 0 : (campo / this.TotalContasRecepcionadas) * 100;
        }
    }
}
