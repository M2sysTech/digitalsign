namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using Entidades;
    using Framework.Modelo;
    using Repositorios;

    public class ValidaSePodeSalvarAjustesServico : IValidaSePodeSalvarAjustesServico
    {
        private readonly IProcessoRepositorio processoRepositorio;

        public ValidaSePodeSalvarAjustesServico(IProcessoRepositorio processoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
        }

        public void Validar(int processoId)
        {
            var processo = this.processoRepositorio.ObterComLote(processoId);

            if (processo.Lote.Status != LoteStatus.AguardandoAjustes)
            {
                throw new RegraDeNegocioException("Lote foi ajustado por outro usuário (mudança de status)!");
            }

            if (processo.Status != ProcessoStatus.AguardandoAjuste)
            {
                throw new RegraDeNegocioException("Processo foi ajustado por outro usuário (mudança de status)!");
            }
        }
    }
}
