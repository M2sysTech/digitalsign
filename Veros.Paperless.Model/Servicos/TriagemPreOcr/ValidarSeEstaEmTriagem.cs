namespace Veros.Paperless.Model.Servicos.TriagemPreOcr
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidarSeEstaEmTriagem : IValidarSeEstaEmTriagem
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;

        public ValidarSeEstaEmTriagem(IProcessoRepositorio processoRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
        }

        public bool Validar(int processoId)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);

            var estaNaTriagem = processo.Status == ProcessoStatus.AguardandoTriagem;

            Log.Application.DebugFormat("Processo {0} está em triagem {1}", processoId, estaNaTriagem);

            if (estaNaTriagem)
            {
                this.gravaLogDoLoteServico.Executar(LogLote.AcaoRecebidoNaTriagem, processo.Lote.Id, "Lote recebido na triagem");
            }

            return estaNaTriagem;
        }
    }
}
