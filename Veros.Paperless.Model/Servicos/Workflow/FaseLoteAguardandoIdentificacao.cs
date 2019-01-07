namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class FaseLoteAguardandoIdentificacao : FaseDeWorkflow<Lote, LoteStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public FaseLoteAguardandoIdentificacao(IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = LoteStatus.AguardandoIdentificacao;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaControleQualidadeM2;
        }

        protected override void ProcessarFase(Lote lote)
        {
            ////if (this.TemDocumentoNaoIdentificado(lote))
            ////{
            ////    //// a linha abaixo foi comentada. Isso foi feito na fase setaIdentificacao
            ////    ////this.DefinirNovosStatus(lote, ProcessoStatus.AguardandoIdentificacao, DocumentoStatus.AguardandoIdentificacao);
            ////    return;
            ////}
            if (this.TemDocumentoPendenteFormalistica(lote))
            {
                return;
            }

            this.SetaDocumentoJaIdentificado(lote);

            if (this.TemDocumentoComProblema(lote))
            {
                lote.Status = LoteStatus.AguardandoAjustes;
                lote.StatusAuxiliar = LoteStatusAuxiliar.VeioDaClassificacao;

                foreach (var processo in lote.Processos)
                {
                    processo.Status = ProcessoStatus.SetaPreparacaoAjustes;
                }

                ////this.DefinirNovosStatus(lote, ProcessoStatus.AguardandoAjuste, DocumentoStatus.StatusParaAjusteOrigem);
                this.regraVioladaRepositorio.AlterarStatus(lote.Processos.FirstOrDefault(), 
                    Regra.CodigoRegraDocumentoComProblemaNaClassificacao, 
                    RegraVioladaStatus.Pendente, 
                    RegraVioladaStatus.Marcada);

                return;
            }

            this.DefinirNovosStatus(lote, ProcessoStatus.AguardandoMontagem, null);
            lote.Status = LoteStatus.Identificado;
        }

        private bool TemDocumentoPendenteFormalistica(Lote lote)
        {
            var processo = lote.Processos.FirstOrDefault();

            if (processo == null)
            {
                return false;
            }

            var documentos = processo.Documentos.Where(x =>
                x.Virtual &&
                x.TipoDocumento.Id != TipoDocumento.CodigoFolhaDeRosto &&
                x.TipoDocumento.Id != TipoDocumento.CodigoTermoAutuacaoDossie &&
                x.Status != DocumentoStatus.Excluido &&
                (x.Status == DocumentoStatus.AguardandoIdentificacao || 
                    x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado ||
                    x.TipoDocumento.Id == TipoDocumento.CodigoAguardandoNovoTipo)).ToList();

            return documentos.Any();            
        }

        private void SetaDocumentoJaIdentificado(Lote lote)
        {
            if (!lote.Processos.Any())
            {
                return;
            }

            var documentos = lote.Processos.First().Documentos.Where(x =>
                    x.TipoDocumento.Id != TipoDocumento.CodigoNaoIdentificado &&
                    x.TipoDocumento.Id != TipoDocumento.CodigoAguardandoNovoTipo && 
                    x.Status == DocumentoStatus.AguardandoIdentificacao);

            foreach (var documento in documentos)
            {
                documento.Status = DocumentoStatus.IdentificacaoConcluida;
            }
        }

        private void DefinirNovosStatus(Lote lote, ProcessoStatus processoStatus, DocumentoStatus documentoStatus)
        {
            foreach (var processo in lote.Processos)
            {
                processo.Status = processoStatus;

                if (documentoStatus != null)
                {
                    processo.AlterarStatusDosDocumentos(documentoStatus);
                }
            }
        }
        
        private bool TemDocumentoNaoIdentificado(Lote lote)
        {
            var processo = lote.Processos.FirstOrDefault();

            if (processo == null)
            {
                return false;
            }

            var documentos = processo.Documentos.Where(x => 
                x.Virtual &&
                x.Status != DocumentoStatus.Excluido &&
                x.IndicioDeFraude != Documento.MarcaDeProblema &&
                (x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado || 
                x.TipoDocumento.Id == TipoDocumento.CodigoAguardandoNovoTipo)).ToList();

            return documentos.Any();
        }
        
        private bool TemDocumentoComProblema(Lote lote)
        {
            var processo = lote.Processos.FirstOrDefault();

            if (processo == null)
            {
                return false;
            }

            return processo.Documentos.Any(x => 
                x.Status != DocumentoStatus.Excluido &&
                x.IndicioDeFraude == Documento.MarcaDeProblema);
        }
    }
}