namespace Veros.Paperless.Infra.Boot
{
    using Data.Seeds;
    using Model.Boot;
    using Veros.Data.Hibernate;
    using Veros.Framework.DependencyResolver;
    using Veros.Framework.Modelo;
    using Veros.Storage.FileTransfer;

    public class RegistraDependencias : Registrar<DependencyRegistry>
    {
        public override void Registros(StructureMap.Graph.IAssemblyScanner scan)
        {
            scan.AssemblyContainingType<RegistraDependencias>();
            scan.AddAllTypesOf(typeof(IRepositorio<>));
            scan.Convention<HibernateRepositoryConvention>();
            scan.AddAllTypesOf(typeof(IRepositorio));
            scan.Convention<HibernateRepositoryConvention>();
            scan.AddAllTypesOf(typeof(IRepositorioUnico<>));
            scan.Convention<HibernateRepositoryConvention>();

            this.For<IFileTransfer>().Use(x => FileTransferCliente.Obter());
            
            scan.AddAllTypesOf<Seed>();
        }
    }
}
