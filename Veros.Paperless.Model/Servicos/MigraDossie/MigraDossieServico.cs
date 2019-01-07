namespace Veros.Paperless.Model.Servicos.MigraDossie
{
    using Entidades;
    using Framework.Modelo;
    using Framework.Servicos;
    using Repositorios;

    public class MigraDossieServico : IMigraDossieServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public MigraDossieServico(ISessaoDoUsuario userSession, 
            IPacoteRepositorio pacoteRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio, 
            IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
            this.userSession = userSession;
        }

        public void Executar(int dossieId, string caixaIdentificacao)
        {
            var pacote = this.pacoteRepositorio.ObterPorIdentificacaoCaixa(caixaIdentificacao);

            if (pacote == null || pacote.Id < 1)
            {
                throw new RegraDeNegocioException("Caixa não encontrada!");
            }

            var dossie = this.dossieEsperadoRepositorio.ObterPorId(dossieId);
            var pacoteOriginalId = dossie.Pacote.Id;
            dossie.Pacote = pacote;
            this.dossieEsperadoRepositorio.Salvar(dossie);
            
            var observacaoLog = string.Format("Dossie foi migrado da caixa {0} para caixa {1}.", pacoteOriginalId, pacote.Id);
            this.gravaLogGenericoServico.Executar(LogGenerico.AcaoMigraDossie, 
                dossieId,
                observacaoLog, 
                LogGenerico.ModuloMigracaoDeDossie, 
                this.userSession.UsuarioAtual.Login);
        }
    }
}
