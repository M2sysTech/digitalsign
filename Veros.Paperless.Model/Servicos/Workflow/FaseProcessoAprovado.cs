namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Linq;
    using Entidades;
    using Repositorios;

    /// <summary>
    /// TODO: refatorar
    /// </summary>
    public class FaseProcessoAprovado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public FaseProcessoAprovado(IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;

            this.FaseEstaAtiva = x => x.AprovacaoAtivo;
            this.StatusDaFase = ProcessoStatus.Aprovado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Finalizado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            this.AtualizaIndexacaoDoCampoAlteracao(processo);

            processo.Status = ProcessoStatus.SetaExportacao;
            processo.Lote.DataAguardandoAprovacao = DateTime.Now;
        }

        private void AtualizaIndexacaoDoCampoAlteracao(Processo processo)
        {
            var pac = processo.Pac;

            if (pac == null)
            {
                return;
            }

            var indexacaoDeAlteracaoDeDadosCadastrais = pac.Indexacao
                .FirstOrDefault(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoIndicadorAlteracaoDadosCadastrais);

            if (indexacaoDeAlteracaoDeDadosCadastrais == null)
            {
                return;
            }

            var indexacaoAlterada = this.regraVioladaRepositorio
                .ExisteRegraDeDivergenciaNoProcesso(processo.Id) ? "S" : "N";

            pac.Indexacao.Add(new Indexacao
            {
                Campo = indexacaoDeAlteracaoDeDadosCadastrais.Campo,
                ValorFinal = indexacaoAlterada
            });
        }
    }
}