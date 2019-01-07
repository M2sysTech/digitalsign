namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidarSeEstaEmControleQualidadeM2 : IValidarSeEstaEmControleQualidadeM2
    {
        private readonly IProcessoRepositorio processoRepositorio;

        public ValidarSeEstaEmControleQualidadeM2(IProcessoRepositorio processoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
        }

        public bool Validar(int processoId)
        {
            var processo = this.processoRepositorio.ObterComLote(processoId);

            var estaEmControleQualidadeM2 = processo.Status == ProcessoStatus.AguardandoControleQualidadeM2 &&
                processo.Lote.Status == LoteStatus.AguardandoControleQualidadeM2;

            Log.Application.DebugFormat("Processo está em ControleQualidadeM2 {0}", estaEmControleQualidadeM2);

            return estaEmControleQualidadeM2;
        }
    }
}