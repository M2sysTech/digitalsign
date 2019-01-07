namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class ObterDetalheLoteServico : IObterDetalheLoteServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ICriarDetalheLoteServico criarDetalheLoteServico;

        public ObterDetalheLoteServico(
            IProcessoRepositorio processoRepositorio,
            ICriarDetalheLoteServico criarDetalheLoteServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.criarDetalheLoteServico = criarDetalheLoteServico;
        }

        public DetalheLote Obter(string tipoDeSolicitacao, int processoId)
        {
            if (processoId > 0)
            {
                return this.criarDetalheLoteServico.Criar(this.processoRepositorio.ObterPorId(processoId));
            }

            return null;
        }
    }
}
