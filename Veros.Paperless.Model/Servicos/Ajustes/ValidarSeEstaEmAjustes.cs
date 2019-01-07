namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ValidarSeEstaEmAjustes : IValidarSeEstaEmAjustes
    {
        private readonly IProcessoRepositorio processoRepositorio;

        public ValidarSeEstaEmAjustes(IProcessoRepositorio processoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
        }

        public bool Validar(int processoId)
        {
            var processo = this.processoRepositorio.ObterComLote(processoId);

            var estaEmAjuste = processo.Lote.Status == LoteStatus.AguardandoAjustes && 
                processo.Status == ProcessoStatus.AguardandoAjuste;

            Log.Application.DebugFormat("Processo está em ajustes {0}", estaEmAjuste);

            return estaEmAjuste;
        }
    }
}