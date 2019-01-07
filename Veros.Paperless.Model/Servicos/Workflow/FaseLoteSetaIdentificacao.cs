namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using Entidades;
    using System.Linq;

    public class FaseLoteSetaIdentificacao : FaseDeWorkflow<Lote, LoteStatus>
    {
        public FaseLoteSetaIdentificacao()
        {
            this.FaseEstaAtiva = x => x.FormalisticaAtiva;
            this.StatusDaFase = LoteStatus.SetaIdentificacao;
            this.StatusSeFaseEstiverInativa = LoteStatus.SetaControleQualidadeM2;
        }

        protected override void ProcessarFase(Lote lote)
        {
            this.ExcluirDocumentosNaoVirtuais(lote);
            lote.Status = LoteStatus.AguardandoIdentificacao;
            this.DefinirNovosStatus(lote, ProcessoStatus.AguardandoIdentificacao, DocumentoStatus.AguardandoIdentificacao);
            
            ////Registra hora que finalmente acabou OCR e veio pra ca.
            lote.DataFimIcr = DateTime.Now;
        }

        private void DefinirNovosStatus(Lote lote, ProcessoStatus processoStatus, DocumentoStatus documentoStatus)
        {
            foreach (var processo in lote.Processos)
            {
                processo.Status = processoStatus;

                foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido 
                        && x.Virtual 
                        && x.TipoDocumento.Id != TipoDocumento.CodigoFolhaDeRosto 
                        && x.TipoDocumento.Id != TipoDocumento.CodigoTermoAutuacaoDossie))
                {
                    documento.Status = documentoStatus;
                }
            }
        }

        private void ExcluirDocumentosNaoVirtuais(Lote lote)
        {
            foreach (var documento in lote.Processos.FirstOrDefault().Documentos)
            {
                var ajustadoNaSeparacao = documento.Marca == "M" || documento.Marca == "A";

                if (documento.Virtual == false && ajustadoNaSeparacao == false)
                {
                    documento.Status = DocumentoStatus.Excluido;
                }
            }
        }

        private bool ExisteDocumentoNaoIdentificado(Lote lote)
        {
            if (lote.Processos.Any())
            {
                if (lote.Processos.First().Documentos.Any(x => 
                    x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado ||
                    x.TipoDocumento.Id == TipoDocumento.CodigoAguardandoNovoTipo))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
