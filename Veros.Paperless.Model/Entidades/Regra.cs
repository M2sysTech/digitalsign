namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using Framework.Modelo;

    public class Regra : Entidade
    {
        //// TODO: remover essas constantes, criar enum
        public const string FaseFormalistica = "B";
        public const string FaseAprovacao = "C";
        public const string FaseValidacao = "9";
        public const string FaseProvaZero = "A";
        
        public const string ClassificacaoErro = "E";
        public const string ClassificacaoAlerta = "W";

        public const string ConectivoLogicoE = "E";
        public const string ConectivoLogicoOu = "OU";

        public const string IdentificadorRegraDeDatasDoComprovanteDeRenda = "11";
        
        public const int CodigoRegraQualidadeM2 = 1;
        public const int CodigoRegraQualidadeCef = 2;
        public const int CodigoRegraDocumentoComProblemaNaClassificacao = 3;
        public const int CodigoRegraVerificacaoDeFace = 457;

        public virtual string Descricao
        {
            get; 
            set;
        }

        public virtual string Fase
        {
            get;
            set;
        }
        
        ////TODO: Acredito que esse atributo não deve existir, o correto seria utilizar a collection abaixo.
        public virtual TratamentoRegra Tratamento
        {
            get;
            set;
        }

        public virtual IEnumerable<TratamentoRegra> Tratamentos
        {
            get;
            set;
        }

        public virtual string Identificador
        {
            get;
            set;
        }

        public virtual string Vinculo
        {
            get;
            set;
        }

        public virtual string Classificacao
        {
            get; 
            set;
        }

        public virtual string Ativada
        {
            get;
            set;
        }

        public virtual ConectivoLogico ConectivoLogico
        {
            get;
            set;
        }

        public virtual bool RegraDeFraude
        {
            get;
            set;
        }

        public virtual int TipoProcessoCode
        {
            get;
            set;
        }

        public virtual bool RegraDeRevisao
        {
            get;
            set;
        }

        public virtual bool RegraDeErro
        {
            get
            {
                return this.Classificacao == Regra.ClassificacaoErro;
            }
        }

        public virtual string ProcessarNoMotor
        {
            get; 
            set;
        }

        public virtual string DescricaoDaFraude
        {
            get; 
            set;
        }

        public virtual bool RegraGenerica()
        {
            return this.Identificador == "RGEN";
        }

        public virtual string Log(string faseAtual, string status)
        {
            return string.Format(
                "Regra [{0}]: {1} {2} - Fase: [{3}] - [{4}]",
                this.Identificador,
                this.Descricao,
                this.DescricaoDaFraude,
                faseAtual,
                status);
        }

        public virtual bool TemVinculo()
        {
            return string.IsNullOrEmpty(this.Vinculo) == false;
        }

        public virtual bool DeveProcessarNoMotor()
        {
            return string.IsNullOrEmpty(this.ProcessarNoMotor) == false && this.ProcessarNoMotor == "S";
        }

        public virtual bool RegraDeAtribuicao()
        {
            return this.Classificacao == "A";
        }
    }
}