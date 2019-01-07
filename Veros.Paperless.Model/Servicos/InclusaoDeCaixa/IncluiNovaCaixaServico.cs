namespace Veros.Paperless.Model.Servicos.InclusaoDeCaixa
{
    using Entidades;
    using Framework.Servicos;
    using Repositorios;

    public class IncluiNovaCaixaServico : IIncluiNovaCaixaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IPacoteRepositorio pacoteRespositorio;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public IncluiNovaCaixaServico(ISessaoDoUsuario userSession, 
            IPacoteRepositorio pacoteRespositorio, IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.pacoteRespositorio = pacoteRespositorio;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
            this.userSession = userSession;
        }

        public void Executar(Pacote pacote)
        {
            var pacoteCadastrado = this.pacoteRespositorio.ObterPorIdentificacaoCaixa(pacote.Identificacao);

            if (pacoteCadastrado != null && pacoteCadastrado.Id > 0)
            {
                pacoteCadastrado.Status = Pacote.Recebido;
                pacoteCadastrado.Coleta = pacote.Coleta;
                pacoteCadastrado.UsuarioQueCapturouId = this.userSession.UsuarioAtual.Id;

                this.pacoteRespositorio.Salvar(pacoteCadastrado);
                return;
            }

            pacote.UsuarioQueCapturouId = this.userSession.UsuarioAtual.Id;
            pacote.Status = Pacote.Recebido;
            pacote.Agencia = 1234;
            pacote.Bdu = "1234";
            pacote.Estacao = "0004";
            this.pacoteRespositorio.Salvar(pacote);
            
            this.gravaLogGenericoServico.Executar(LogGenerico.AcaoCriaCaixa,
                pacote.Id,
                "Caixa foi gerada na conferência.",
                LogGenerico.ModuloCriaCaixa,
                this.userSession.UsuarioAtual.Login);
        }
    }
}
