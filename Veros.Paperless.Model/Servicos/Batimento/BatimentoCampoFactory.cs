namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using Entidades;
    using Experimental;
    using Experimental.BatimentoPorCampo;
    using Framework;

    public class BatimentoCampoFactory
    {
        /// <summary>
        /// string => campo.referencia_arquivo
        /// </summary>
        private readonly IDictionary<string, IBatimentoCampo> tiposBatimento;

        public BatimentoCampoFactory()
        {
            this.tiposBatimento = new Dictionary<string, IBatimentoCampo>();

            this.tiposBatimento.Add(Campo.ReferenciaDeArquivoNomeTitular, new BatimentoCampo<NomeTitularBatimento>());
            this.tiposBatimento.Add(Campo.ReferenciaDeArquivoNomeMaeCliente, new BatimentoCampo<NomeMaeBatimento>());
            this.tiposBatimento.Add(Campo.ReferenciaDeArquivoNomePaiCliente, new BatimentoCampo<NomePaiBatimento>());
            this.tiposBatimento.Add(Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante, new BatimentoCampo<LogradouroBatimento>());
            this.tiposBatimento.Add(Campo.ReferenciaDeArquivoNumeroDaResidenciaDoParticipante, new BatimentoCampo<NumeroResidencialBatimento>());
        }

        public IBatimentoCampo Criar(Indexacao indexacao)
        {
            if (this.DeveBaterDeFormaPersonalizada(indexacao) == false)
            {
                switch (indexacao.Campo.TipoCampo)
                {
                    case TipoCampo.DataGenerica:
                        return new BatimentoCampo<DataQualquerBatimento>();
                    case TipoCampo.RegistroRg:
                        return new BatimentoCampo<NumeroRegistroBatimento>();
                    default:
                        return new BatimentoCampo<QualquerCampoBatimento>();
                }
            }
            else
            {
                return this.tiposBatimento[indexacao.Campo.ReferenciaArquivo];
            }
        }

        private bool DeveBaterDeFormaPersonalizada(Indexacao indexacao)
        {
            if (indexacao.NaoTemConteudo())
            {
                return false;
            }

            if (indexacao.Campo.ReferenciaArquivo.NaoTemConteudo())
            {
                return false;
            }

            return indexacao.Campo.ReferenciaArquivo.Equals(Campo.ReferenciaDeArquivoNomeTitular) ||
                indexacao.Campo.ReferenciaArquivo.Equals(Campo.ReferenciaDeArquivoNomeMaeCliente) ||
                indexacao.Campo.ReferenciaArquivo.Equals(Campo.ReferenciaDeArquivoNomePaiCliente) ||
                indexacao.Campo.ReferenciaArquivo.Equals(Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante) ||
                indexacao.Campo.ReferenciaArquivo.Equals(Campo.ReferenciaDeArquivoNumeroDaResidenciaDoParticipante);
        }
    }
}