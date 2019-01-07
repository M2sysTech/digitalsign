namespace Veros.Paperless.Model.Servicos.Preparo
{
    using System;
    using Entidades;
    using Framework.Modelo;
    using Framework.Servicos;
    using Repositorios;

    public class GravaHorasPreparoServico : IGravaHorasPreparoServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public GravaHorasPreparoServico(ISessaoDoUsuario userSession,
            IPacoteRepositorio pacoteRepositorio, 
            IUsuarioRepositorio usuarioRepositorio, 
            IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.userSession = userSession;
            this.pacoteRepositorio = pacoteRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
        }

        public void Executar(string acao, string barcode, string matricula1, string matricula2, string matricula3, string matricula4, string horaInicio, string horaFim)
        {
            var pacote = this.pacoteRepositorio.ObterPorIdentificacaoCaixa(barcode);

            if (pacote == null)
            {
                throw new RegraDeNegocioException("Caixa não encontrada!");
            }

            if (acao == "I")
            {
                pacote.DataInicioPreparo = DateTime.Now;    
            }

            if (acao == "F")
            {
                pacote.DataFimPreparo = DateTime.Now;
            }

            if (acao == "A")
            {
                pacote.DataInicioPreparo = DateTime.Parse(horaInicio);
                pacote.DataFimPreparo = DateTime.Parse(horaFim);
            }

            pacote.UsuarioPreparo = this.ObterUsuario(matricula1, pacote.UsuarioPreparo);
            pacote.UsuarioPreparo2 = this.ObterUsuario(matricula2, pacote.UsuarioPreparo2);
            pacote.UsuarioPreparo3 = this.ObterUsuario(matricula3, pacote.UsuarioPreparo3);
            pacote.UsuarioPreparo4 = this.ObterUsuario(matricula4, pacote.UsuarioPreparo4);

            if (string.IsNullOrEmpty(horaInicio) == false && horaInicio.Length > 4)
            {
                pacote.DataInicioPreparo = DateTime.Parse(horaInicio);
            }

            if (pacote.DataInicioPreparo.HasValue == false)
            {
                throw new RegraDeNegocioException("Hora de Início do Preparo não foi definida!");
            }

            if (pacote.UsuarioPreparo == null)
            {
                throw new RegraDeNegocioException("Usuário 1 não foi definido!");
            }

            this.pacoteRepositorio.Salvar(pacote);

            var mensagemLog = string.Format("Ini:{0} - U1:{1} - U2:{2} - U3:{3} - U4{4}- Fim:{5}", 
                pacote.DataInicioPreparo.GetValueOrDefault(), 
                pacote.UsuarioPreparo.Login,
                pacote.UsuarioPreparo2 != null ? pacote.UsuarioPreparo2.Login : string.Empty,
                pacote.UsuarioPreparo3 != null ? pacote.UsuarioPreparo3.Login : string.Empty,
                pacote.UsuarioPreparo4 != null ? pacote.UsuarioPreparo4.Login : string.Empty,
                pacote.DataFimPreparo.GetValueOrDefault());

            var tipoLog = string.Empty;
            switch (acao)
            {
                case "I":
                    tipoLog  = LogGenerico.AcaoInicioPreparo;
                    break;
                case "F":
                    tipoLog  = LogGenerico.AcaoInicioPreparo;
                    break;
                case "A":
                    tipoLog  = LogGenerico.AcaoAtualizarPreparo;
                    break;
            }
            
            this.gravaLogGenericoServico.Executar(tipoLog,
                pacote.Id,
                mensagemLog,
                LogGenerico.ModuloPreparo,
                this.userSession.UsuarioAtual.Login);
        }

        private Usuario ObterUsuario(string matricula, Usuario usuarioPadrao)
        {
            if (string.IsNullOrEmpty(matricula))
            {
                return usuarioPadrao;
            }

            var usuario = this.usuarioRepositorio.ObterPeloLogin(matricula);

            if (usuario == null)
            {
                throw new RegraDeNegocioException("Nenhum usuário não encontrado com a matrícula " + matricula);
            }

            return usuario;
        }
    }
}
