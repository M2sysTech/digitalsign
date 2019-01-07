namespace SignService.Boot
{
    using Veros.Framework.DependencyResolver;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Infra;

    public class DependencyRegistry : Registrar<DependencyRegistry>
    {
        public override void Registros(StructureMap.Graph.IAssemblyScanner scan)
        {
            this.For<ISessaoDoUsuario>().Use<SessaoDoUsuarioServidoDoWindows>();
        }
    }
}
