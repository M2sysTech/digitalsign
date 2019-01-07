namespace Veros.Framework.DependencyResolver
{
    using StructureMap;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public abstract class Registrar<T> : Registry where T : class
    {
        protected Registrar()
        {
            Log.Framework.DebugFormat("Executando Registrar<T> em {0}", typeof(T).FullName);
            
            this.Scan(x =>
            {
                x.AssemblyContainingType<T>();
                x.WithDefaultConventions();
                this.Registros(x);
            });
        }

        public virtual void Registros(IAssemblyScanner scan)
        {
        }
    }
}
