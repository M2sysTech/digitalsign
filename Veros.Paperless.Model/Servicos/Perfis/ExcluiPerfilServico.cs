namespace Veros.Paperless.Model.Servicos.Perfis
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Repositorios;

    public class ExcluiPerfilServico : IExcluiPerfilServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IPerfilRepositorio perfilRepositorio;
        private readonly IAcessoRepositorio acessoRepositorio;

        public ExcluiPerfilServico(
            IUsuarioRepositorio usuarioRepositorio,
            IPerfilRepositorio perfilRepositorio,
            IAcessoRepositorio acessoRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.perfilRepositorio = perfilRepositorio;
            this.acessoRepositorio = acessoRepositorio;
        }

        public void Executar(int perfilId)
        {
            if (this.usuarioRepositorio.ExisteUsuarioPorPerfil(perfilId))
            {
                throw new RegraDeNegocioException("Exclusão não permitida. Existe usuário com esse perfil");    
            }

            this.acessoRepositorio.ApagarPorPerfil(perfilId);

            this.perfilRepositorio.Apagar(new Perfil { Id = perfilId });
        }
    }
}