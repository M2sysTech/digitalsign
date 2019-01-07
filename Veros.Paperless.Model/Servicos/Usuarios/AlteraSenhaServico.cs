namespace Veros.Paperless.Model.Servicos.Usuarios
{
    using Entidades;
    using Framework.Security;
    using Framework.Servicos;
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Repositorios;

    public class AlteraSenhaServico : IAlteraSenhaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public AlteraSenhaServico(ISessaoDoUsuario userSession, 
            IUsuarioRepositorio usuarioRepositorio, 
            IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.userSession = userSession;
            this.usuarioRepositorio = usuarioRepositorio;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
        }

        public void Executar(int usuarioId, string login, string senhaAtual, string senhaNova)
        {
            var usuario = this.usuarioRepositorio.ObterPeloLoginSenha(login, senhaAtual);

            if (usuario == null || usuario.Id != usuarioId)
            {
                throw new RegraDeNegocioException("Senha atual não confere");
            }

            this.usuarioRepositorio.AlterarSenha(usuarioId, senhaNova);
        }

        public void Executar(int usuarioId, string senhaNova)
        {
            var usuario = this.usuarioRepositorio.ObterPorId(usuarioId);

            if (usuario == null || usuario.Id < 1)
            {
                throw new RegraDeNegocioException("Usuário não está encontrado");
            }

            if (string.IsNullOrEmpty(senhaNova) || senhaNova.Length < 3)
            {
                throw new RegraDeNegocioException("A senha deve possuir mais de 3 caracteres");
            }

            var senhaCriptografada = new UpperCaseHash().HashText(senhaNova.ToUpper());

            if (usuario.Senha == senhaCriptografada)
            {
                throw new RegraDeNegocioException("Nova senha não pode ser igual a senha atual");
            }

            this.usuarioRepositorio.AlterarSenha(usuarioId, senhaNova);

            var acao = LogGenerico.AcaoAlteracaoDeSenha;
            var mensagem = "Usuário alterou a senha";

            if (this.userSession.UsuarioAtual.Id != usuarioId)
            {
                acao = LogGenerico.AcaoAlteracaoDeSenhaDeOutroUsuario;
                mensagem = string.Format("Senha do usuário {0} foi alterada.", usuarioId);
            }
            
            this.gravaLogGenericoServico.Executar(acao, 
                usuarioId, 
                mensagem, 
                "Alteração de Senha",
                this.userSession.UsuarioAtual.Login);
        }
    }
}