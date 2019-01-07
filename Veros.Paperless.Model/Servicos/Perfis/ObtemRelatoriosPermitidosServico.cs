namespace Veros.Paperless.Model.Servicos.Perfis
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;
    using System.Collections.Generic;
    using System.Linq;

    public class PodeAcessarFuncionalidadeServico : IPodeAcessarFuncionalidadeServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IAcessoRepositorio acessoRepositorio;

        public PodeAcessarFuncionalidadeServico(
            ISessaoDoUsuario userSession,
            IAcessoRepositorio acessoRepositorio)
        {
            this.userSession = userSession;
            this.acessoRepositorio = acessoRepositorio;
        }

        public bool Executar(Funcionalidade funcionalidade)
        {
            if (this.userSession.UsuarioAtual == null)
            {
                return false;
            }

            var usuario = (Usuario) this.userSession.UsuarioAtual;

            if (usuario.Perfil == null)
            {
                return false;
            }
            
            return this.acessoRepositorio.ExisteAcessoPorPerfilEFuncionalidade(usuario.Perfil.Id, funcionalidade);
        }

        public bool Executar(IList<Funcionalidade> funcionalidades)
        {
            if (this.userSession.UsuarioAtual == null)
            {
                return false;
            }

            var usuario = (Usuario)this.userSession.UsuarioAtual;

            if (usuario.Perfil == null)
            {
                return false;
            }

            if (usuario.Perfil.Acessos.Any())
            {
                return usuario.Perfil.Acessos.Any(x => funcionalidades.Any(func => func == x.Funcionalidade));
            }

            var acessos = this.acessoRepositorio.ObterAcessosPorPerfil(usuario.Perfil.Id);
            return acessos.Any(x => funcionalidades.Any(func => func == x.Funcionalidade));
        }

        public bool ChecarSiglaPerfil(IList<string> listaSiglaPerfil)
        {
            if (this.userSession.UsuarioAtual == null)
            {
                return false;
            }

            var usuario = (Usuario)this.userSession.UsuarioAtual;

            if (usuario.Perfil == null)
            {
                return false;
            }

            return listaSiglaPerfil.Any(x => x == usuario.Perfil.Sigla);
        }
    }
}