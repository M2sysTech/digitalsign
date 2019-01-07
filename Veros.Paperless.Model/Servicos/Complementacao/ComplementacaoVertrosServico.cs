namespace Veros.Paperless.Model.Servicos.Complementacao
{
    using Data;
    using Repositorios;
    using Storages;

    public class ComplementacaoVertrosServico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPortalVertros portalVertros;
        private readonly IConsultaVertrosStorage consultaVertrosStorage;

        public ComplementacaoVertrosServico(
            ILoteRepositorio loteRepositorio,
            IUnitOfWork unitOfWork,
            IPortalVertros portalVertros,
            IConsultaVertrosStorage consultaVertrosStorage)
        {
            this.loteRepositorio = loteRepositorio;
            this.unitOfWork = unitOfWork;
            this.portalVertros = portalVertros;
            this.consultaVertrosStorage = consultaVertrosStorage;
        }

        public void Executar()
        {
            var lotes = this.unitOfWork.Obter(() => this.loteRepositorio.ObterPendentesConsultaVertros());

            foreach (var lote in lotes)
            {
                var dados = this.portalVertros.Analisar(lote.Identificacao);
                this.consultaVertrosStorage.Adicionar(lote.Identificacao, dados);
                this.unitOfWork.Transacionar(() => this.loteRepositorio.AtualizaVertrosOk(lote.Id));
            }
        }
    }
}