namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;
    using Repositorios;

    public class SalvaRegraViolada : ISalvaRegraViolada
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IGravaLogDoLoteServico salvaLogLote;

        public SalvaRegraViolada(
            IRegraVioladaRepositorio regraVioladaRepositorio, 
            IGravaLogDoLoteServico salvaLogLote)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.salvaLogLote = salvaLogLote;
        }

        public void Salvar(Processo processo, RegraViolada regraViolada, string faseAtual)
        {
            this.regraVioladaRepositorio.ExcluirRegraDoProcesso(regraViolada.Processo.Id, regraViolada.Regra.Id);

            this.regraVioladaRepositorio.Salvar(regraViolada);
            this.salvaLogLote.Executar("71", processo.Lote.Id, regraViolada.Regra.Log(faseAtual, regraViolada.Status.DisplayName));

            ////var valor2 = this.indexacaoRepositorio
            ////    .ObterValor2DoCampoCpfBas(regraViolada.Documento.Id);
        }
    }
}