namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;
    using TriagemPreOcr;
    using ViewModel;

    public class GravaAjustesServico : IGravaAjustesServico
    {
        private readonly IGravaAjustesDoProcessoServico gravaAjustesDoProcessoServico;
        private readonly IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IExecutaAcoesTriagemServico executaAcoesTriagemServico;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public GravaAjustesServico(
            IGravaAjustesDoProcessoServico gravaAjustesDoProcessoServico, 
            IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico,
            IRegraVioladaRepositorio regraVioladaRepositorio, 
            IExecutaAcoesTriagemServico executaAcoesTriagemServico, 
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.gravaAjustesDoProcessoServico = gravaAjustesDoProcessoServico;
            this.validaSePodeSalvarAjustesServico = validaSePodeSalvarAjustesServico;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.executaAcoesTriagemServico = executaAcoesTriagemServico;
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(int processoId, string operacao, string observacao, string acoes)
        {
            this.validaSePodeSalvarAjustesServico.Validar(processoId);
            
            var loteTriagem = this.executaAcoesTriagemServico.ExecutarAcoes(processoId, acoes, true, true, LoteTriagemViewModel.FaseAjuste);

            this.AtualizarDocumentosManipulados(loteTriagem);
            
            this.regraVioladaRepositorio.AprovarRegrasMarcadas(processoId);

            this.gravaAjustesDoProcessoServico.Executar(processoId, operacao, observacao);
            
            var processo = this.processoRepositorio.ObterPorId(processoId);

            this.loteRepositorio.AlterarStatus(processo.Lote.Id, LoteStatus.AguardandoRealizacaoAjustes);
        }

        private void AtualizarDocumentosManipulados(LoteTriagemViewModel loteTriagem)
        {
            if (loteTriagem != null)
            {
                //// seta status para 5B para novos mdocs
                foreach (var documentosNovo in loteTriagem.DocumentosNovos)
                {
                    documentosNovo.Marca = Documento.MarcaDeAlteradoNaSeparacao;
                    documentosNovo.Status = DocumentoStatus.StatusParaReconhecimentoPosAjuste;
                    this.documentoRepositorio.Salvar(documentosNovo);
                    this.documentoRepositorio.AlterarMarca(documentosNovo.Id, Documento.MarcaDeAlteradoNaSeparacao);
                    this.documentoRepositorio.AlterarStatus(documentosNovo.Id, DocumentoStatus.StatusParaReconhecimentoPosAjuste);
                    this.documentoRepositorio.AjusteFinalizado(documentosNovo.Id);
                }

                foreach (var documento in loteTriagem.Documentos.Where(x => x.Manipulado).Distinct())
                {
                    this.documentoRepositorio.AlterarMarca(documento.Id, Documento.MarcaDeAlteradoNaSeparacao);
                    this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.StatusParaReconhecimentoPosAjuste);
                    this.documentoRepositorio.AjusteFinalizado(documento.Id);
                }
            }
        }
    }
}
