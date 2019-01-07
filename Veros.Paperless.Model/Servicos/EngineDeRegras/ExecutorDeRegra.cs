namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    /// <summary>
    /// Responsabilidade:
    /// Descobrir e salvar regras atendidas no processo para a fase
    /// </summary>
    public class ExecutorDeRegra : IExecutorDeRegra
    {
        private readonly IRegraRepositorio regraRepositorio;
        private readonly IExecutorDeCondicoes executorDeCondicoes;
        private readonly ISalvaRegraViolada salvaRegraViolada;
        private readonly IRegraImportadaRepositorio regraImportadaRepositorio;
        private readonly IAtribuiValorCalculadoNaRegra atribuiValorCalculadoNaRegra;

        public ExecutorDeRegra(
            IRegraRepositorio regraRepositorio, 
            IExecutorDeCondicoes executorDeCondicoes, 
            ISalvaRegraViolada salvaRegraViolada, 
            IRegraImportadaRepositorio regraImportadaRepositorio,
            IAtribuiValorCalculadoNaRegra atribuiValorCalculadoNaRegra)
        {
            this.regraRepositorio = regraRepositorio;
            this.executorDeCondicoes = executorDeCondicoes;
            this.salvaRegraViolada = salvaRegraViolada;
            this.regraImportadaRepositorio = regraImportadaRepositorio;
            this.atribuiValorCalculadoNaRegra = atribuiValorCalculadoNaRegra;
        }
        
        public bool ExistemRegrasAtendidas(string faseAtual, Processo processo)
        {
            var houveAlgumaRegraAtendida = false;

            var regrasDaFase = this.regraRepositorio
                .ObterRegrasPorFase(faseAtual, processo.TipoDeProcesso);

            foreach (var regra in regrasDaFase.Where(x => x.DeveProcessarNoMotor()))
            {
                var resultadoCondicoes = this.executorDeCondicoes.Executar(processo, regra);

                if (regra.TemVinculo())
                {
                    resultadoCondicoes.Documento = this.regraImportadaRepositorio
                        .ObterDocumentoPorVinculoEProcesso(regra.Vinculo, processo.Id);
                }

                if (resultadoCondicoes.ForamAtendidas)
                {
                    houveAlgumaRegraAtendida = true;
                }

                this.salvaRegraViolada.Salvar(processo, resultadoCondicoes.RegraViolada, faseAtual);

                if (string.IsNullOrEmpty(resultadoCondicoes.ResultadoCalculado) == false)
                {
                    this.atribuiValorCalculadoNaRegra.Executar(resultadoCondicoes, processo);
                }
            }

            return houveAlgumaRegraAtendida;
        }
    }
}
