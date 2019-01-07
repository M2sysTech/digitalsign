namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;
    using Repositorios;

    public class ProcessadorBinarioB8 : IProcessadorDeBinario
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public ProcessadorBinarioB8(IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
        }

        public ResultadoCondicaoDeRegra Processar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            return new ResultadoCondicaoDeRegra(this.regraVioladaRepositorio
                .ExistePorProcessoERegraEStatus(processo.Id, regra.Id, RegraVioladaStatus.Marcada));
        }
    }
}