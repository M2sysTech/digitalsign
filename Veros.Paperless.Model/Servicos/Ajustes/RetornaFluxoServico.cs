namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class RetornaFluxoServico : IRetornaFluxoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico;

        public RetornaFluxoServico(
            IDocumentoRepositorio documentoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            IValidaSePodeSalvarAjustesServico validaSePodeSalvarAjustesServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.validaSePodeSalvarAjustesServico = validaSePodeSalvarAjustesServico;
        }

        public void Executar(int processoId, string operacao, string observacao, string acoes)
        {
            this.validaSePodeSalvarAjustesServico.Validar(processoId);

            var listaDocumentos = this.documentoRepositorio.ObterPorProcesso(processoId);
            var processo = listaDocumentos.First().Processo;
            var listaIdsAlterados = new List<int>();
            var loteId = listaDocumentos.First().Lote.Id;

            foreach (var documentoAtual in listaDocumentos.Where(x => x.TipoDocumento.Id == TipoDocumento.CodigoEmAjuste))
            {
                documentoAtual.Status = DocumentoStatus.Excluido;
                listaIdsAlterados.Add(documentoAtual.Id);
            }

            foreach (var documentoAtual in listaDocumentos.Where(x => x.Status == DocumentoStatus.AjustePreparacaoRealizada))
            {
                if (documentoAtual.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado || documentoAtual.TipoDocumento.Id == TipoDocumento.CodigoAguardandoNovoTipo)
                {
                    documentoAtual.Status = DocumentoStatus.AguardandoIdentificacao;    
                }
                else
                {
                    documentoAtual.Status = DocumentoStatus.IdentificacaoConcluida;    
                }

                documentoAtual.IndicioDeFraude = string.Empty;
                listaIdsAlterados.Add(documentoAtual.Id);
            }

            foreach (var documentoAtual in listaDocumentos.Where(x => listaIdsAlterados.Any(y => y == x.Id)))
            {
                this.documentoRepositorio.Salvar(documentoAtual);    
            }

            this.loteRepositorio.AlterarStatus(loteId, LoteStatus.SetaIdentificacao);

            Log.Application.DebugFormat("Dossiê retornou ao fluxo sem realização de ajustes. Proc:[{0}] ", processoId);
            this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoRetornarParaFluxo, processo, "Dossiê retornou ao fluxo sem realização de ajustes.");
        }
    }
}
