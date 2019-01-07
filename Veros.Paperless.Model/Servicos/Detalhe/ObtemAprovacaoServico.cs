namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System.Linq;
    using Aprovacao;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemAprovacaoServico : IObtemAprovacaoServico
    {
        private readonly IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio;
        private readonly ISessaoDoUsuario userSession;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IParticipantesServico participantesServico;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public ObtemAprovacaoServico(
            IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio,
            ISessaoDoUsuario userSession,
            IProcessoRepositorio processoRepositorio,
            IParticipantesServico participantesServico, 
            IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.estatisticaAprovacaoRepositorio = estatisticaAprovacaoRepositorio;
            this.userSession = userSession;
            this.processoRepositorio = processoRepositorio;
            this.participantesServico = participantesServico;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
        }

        public Aprovacao Obter(Processo processo)
        {
            var regras = this.regraVioladaRepositorio.ObterRegrasVioladasParaAprovacao(processo.Id);

            this.processoRepositorio.AlterarResponsavel(processo.Id, this.userSession.UsuarioAtual.Id);
            
            var aprovacao = new Aprovacao
            {
                Processo = processo,
                RegrasVioladas = regras,
                Pac = processo.Pac ?? processo.OutrosDocumentos.ElementAt(0),
                ContadorProdutividadeUsuario = this.CalcularContadorDeProdutividade(),
                Participantes = this.participantesServico.Obter(processo),
                TipoDocumento1 = TipoDocumento.CodigoRg,
                TipoDocumento2 = TipoDocumento.CodigoComprovanteDeResidencia,
                PossuiRegrasDeFraude = processo.Documentos.Any(x => string.IsNullOrEmpty(x.IndicioDeFraude) == false)
            };

            return aprovacao;
        }

        private int CalcularContadorDeProdutividade()
        {
            var estatisticaAprovacao = this.estatisticaAprovacaoRepositorio
                    .ObterDeHojePorUsuario(this.userSession.UsuarioAtual.Id);

            return estatisticaAprovacao == null
                ? 0
                : estatisticaAprovacao.Producao;
        }
    }
}