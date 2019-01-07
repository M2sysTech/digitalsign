namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Diagnostics;
    using Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class WorkflowDeLote
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public WorkflowDeLote(ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Processar(Lote lote, ConfiguracaoDeFases configuracaoDeFases)
        {
            ////this.Fase<FaseLoteAguardandoCaptura>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteCapturaFinalizada>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteTransmissaoFinalizada>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteSetaTriagemPreOcr>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteIdentificacaoManual>().Processar(lote, configuracaoDeFases);

            //// Fase de OCR pode ser setada como gargalo alterando a tag GARGALO_OCR
            this.Fase<FaseLoteSetaReconhecimento>().Processar(lote, configuracaoDeFases);
            //// Se tem Doc geral, a fase abaixo avalia pra onde deve ir
            this.Fase<FaseLoteAguardandoReconhecimento>().Processar(lote, configuracaoDeFases);

            //// fases de classifier e de reconhecimento não estão ativadas. pulando do status 15 para 5S
            ////this.Fase<FaseLoteSetaClassifier>().Processar(lote, configuracaoDeFases);
            
            ////this.Fase<FaseLoteAguardandoClassifier>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteClassifierConcluido>().Processar(lote, configuracaoDeFases);            

            //// do lote status 5S para 61 quem faz é o serviço de Batimento
            this.Fase<FaseLoteSetaIdentificacao>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteAguardandoIdentificacao>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteIdentificado>().Processar(lote, configuracaoDeFases);
            
            //// fase que verifica necessidade de parar na QC M2sys 
            this.Fase<FaseLoteSetaQualidadeM2Sys>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteGeracaoTermosConcluido>().Processar(lote, configuracaoDeFases); 

            this.Fase<FaseLoteAguardandoAssinatura>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteAssinaturaFinalizada>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteExportadoParaCloud>().Processar(lote, configuracaoDeFases);

            //// fase que verifica necessidade de parar na QC CEF e joga o dossie num dia lotecef
            this.Fase<FaseLoteSetaQualidadeCef>().Processar(lote, configuracaoDeFases);
            ////this.Fase<FaseLoteAguardandoQualidadeCef>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteQualidadeCefFinalizado>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteAguardandoPreparacaoDeAjuste>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLotePreparacaoAjustes>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteAguardandoRealizacaoDeAjuste>().Processar(lote, configuracaoDeFases);
            this.Fase<FaseLoteAjusteRealizado>().Processar(lote, configuracaoDeFases);

            //// gera faturamento 
            this.Fase<FaseLoteAguardandoFaturamento>().Processar(lote, configuracaoDeFases);

            this.Fase<FaseLoteBatimentoFinalizado>().Processar(lote, configuracaoDeFases);
            
            this.SalvarLote(lote);
        }

        private void SalvarLote(Lote lote)
        {
            this.loteRepositorio.Salvar(lote);

            foreach (var processo in lote.Processos)
            {
                this.processoRepositorio.Salvar(processo);

                foreach (var documento in processo.Documentos)
                {
                    this.documentoRepositorio.Salvar(documento);
                    
                    foreach (var pagina in documento.Paginas)
                    {
                        this.paginaRepositorio.Salvar(pagina);
                    }
                }
            }
        }

        [DebuggerStepThrough]
        private T Fase<T>() where T : FaseDeWorkflow<Lote, LoteStatus>
        {
            return IoC.Current.Resolve<T>();
        }
    }
}