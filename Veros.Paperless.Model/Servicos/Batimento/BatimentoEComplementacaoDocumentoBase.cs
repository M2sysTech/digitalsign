namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public abstract class BatimentoEComplementacaoDocumentoBase : IBatimentoEComplementacaoDocumentoServico
    {
        protected readonly IGravaLogDoDocumentoServico gravaLogDocumentoServico;
        protected readonly ICampoRepositorio campoRepositorio;
        protected readonly IIndexacaoRepositorio indexacaoRepositorio;

        protected BatimentoEComplementacaoDocumentoBase(
            ICampoRepositorio campoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDocumentoServico,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
            this.gravaLogDocumentoServico = gravaLogDocumentoServico;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        protected bool PodeInserirCampoNaoReconhecido
        {
            get;
            set;
        }

        public virtual void Execute(Documento documento, ImagemReconhecida imagemReconhecida, List<int> camposBatidosId = null)
        {
            if (this.PodeComplementarIndexacao(documento, imagemReconhecida) == false)
            {
                return;
            }

            var camposDoDocumento = this.campoRepositorio
                .ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id)
                .OrderBy(campo => campo.PodeInserirPeloOcr);

            foreach (var campoDoDocumento in camposDoDocumento)
            {
                var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo == campoDoDocumento);
                var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

                if (this.ExistemValoresReconhecidos(valoresReconhecidos) == false)
                {
                    if (this.CampoPodeSerInseridoMesmoQuandoNaoHaReconhecimento(indexacao, campoDoDocumento))
                    {
                        indexacao = this.AdicionaNovaIndexacaoParaCampoReconhecido(
                            documento,
                            null,
                            campoDoDocumento);

                        this.gravaLogDocumentoServico.Executar(
                            LogDocumento.AcaoDocumentoOcr,
                            documento.Id,
                            string.Format("Campo [{0}] inserido pelo ocr.", campoDoDocumento.Description));

                        this.indexacaoRepositorio.Salvar(indexacao);
                    }

                    continue;
                }

                foreach (var valorReconhecido in valoresReconhecidos)
                {
                    if (string.IsNullOrEmpty(valorReconhecido.Value))
                    {
                        continue;
                    }

                    if (campoDoDocumento.EstaMapeadoPara(valorReconhecido.CampoTemplate, valorReconhecido.TemplateName) == false)
                    {
                        continue;
                    }

                    indexacao = this.AlterarIndexacao(
                        documento,
                        valorReconhecido,
                        campoDoDocumento,
                        indexacao,
                        imagemReconhecida.Palavras);

                    if (indexacao != null)
                    {
                        Log.Application.DebugFormat(
                       "BatimentoEComplementacaoDocumentoBase::Salvou indexacao?? Documento {0} Indexacao {1}",
                       indexacao.Documento.Id,
                       indexacao.Id);

                        this.indexacaoRepositorio.Salvar(indexacao);
                    }
                }
            }
        }

        public bool ExistemValoresReconhecidos(IList<ValorReconhecido> valoresReconhecidos)
        {
            if (valoresReconhecidos == null)
            {
                return false;
            }

            return valoresReconhecidos.Count != 0;
        }

        public bool CampoPodeSerInseridoMesmoQuandoNaoHaReconhecimento(Indexacao indexacao, Campo campoDoDocumento)
        {
            return this.PodeInserirCampoNaoReconhecido && this.PodeInserirCampoReconhecido(campoDoDocumento, indexacao);
        }

        public Indexacao AlterarIndexacao(
            Documento documento, 
            ValorReconhecido valorReconhecido, 
            Campo campoDoDocumento, 
            Indexacao indexacao, 
            IList<PalavraReconhecida> palavras)
        {
            if (this.PodeInserirCampoReconhecido(campoDoDocumento, indexacao))
            {
                indexacao = this.AdicionaNovaIndexacaoParaCampoReconhecido(
                    documento, 
                    valorReconhecido, 
                    campoDoDocumento);

                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr, 
                    documento.Id, 
                    string.Format("Campo [{0}] inserido pelo ocr.", campoDoDocumento.Description));
            }
            else
            {
                if (indexacao == null)
                {
                    return null;
                }

                if (indexacao.ValorFinal.NaoTemConteudo() == false)
                {
                    return indexacao;
                }

                indexacao = this.ModificaIndexacaoDoCampoReconhecido(
                    indexacao, 
                    valorReconhecido,
                    palavras);

                this.gravaLogDocumentoServico.Executar(
                    LogDocumento.AcaoDocumentoOcr, 
                    documento.Id, 
                    string.Format("Campo [{0}] atualizado pelo ocr.", campoDoDocumento.Description));
            }

            return indexacao;
        }

        protected virtual bool PodeComplementarIndexacao(Documento documento, ImagemReconhecida imagemReconhecida)
        {
            if (imagemReconhecida == null)
            {
                return false;
            }

            var valoresReconhecidos = imagemReconhecida.ValoresReconhecidos;

            if (valoresReconhecidos == null)
            {
                return false;
            }

            var camposDoDocumento = this.campoRepositorio.ObterPorTipoDocumentoComMapeamentoOcr(documento.TipoDocumento.Id);

            if (camposDoDocumento == null)
            {
                return false;
            }

            if (camposDoDocumento.Count == 0)
            {
                return false;
            }

            return true;
        }

        protected abstract Indexacao ModificaIndexacaoDoCampoReconhecido(Indexacao indexacao, ValorReconhecido valorReconhecido, IList<PalavraReconhecida> palavras);

        protected abstract Indexacao AdicionaNovaIndexacaoParaCampoReconhecido(Documento documento, ValorReconhecido valorReconhecido, Campo campoDoDocumento);

        private bool PodeInserirCampoReconhecido(Campo campoDoDocumento, Indexacao indexacao)
        {
            return indexacao == null && campoDoDocumento.PodeInserirPeloOcr;
        }
    }
}