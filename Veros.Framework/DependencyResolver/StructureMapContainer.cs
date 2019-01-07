namespace Veros.Framework.DependencyResolver
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Service;
    using StructureMap;
    using StructureMap.Graph;

    public class StructureMapContainer : IIoC
    {
        private IContainer container = new Container();

        public void RegisterDependencies(
            List<string> assembliesPreffixes, 
            List<string> excludes, 
            string dependencyPlugin)
        {
            Log.Framework.Debug("Registrando dependencias");

            try
            {
                this.InternalRegisterDependencies(
                    assembliesPreffixes, 
                    excludes, 
                    dependencyPlugin);
            }
            catch (Exception exception)
            {
                Log.Framework.Error("Falha ao registrar dependencias", exception);
                throw;
            }
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return this.container.GetAllInstances<T>();
        }

        public IEnumerable GetAllInstance(Type type)
        {
            return this.container.GetAllInstances(type);
        }

        public object Resolve(Type type)
        {
            return this.InternalResolve(type, string.Empty);
        }

        public T Resolve<T>()
        {
            return (T)this.InternalResolve(typeof(T), string.Empty);
        }

        public T Resolve<T>(string namedInstance)
        {
            return (T)this.InternalResolve(typeof(T), namedInstance);
        }

        public void BuildUp(object obj)
        {
            this.container.BuildUp(obj);
        }

        public void Inject<T>(object instance)
        {
            this.container.EjectAllInstancesOf<T>();
            this.container.Inject(typeof(T), instance);
        }
        
        private void InternalRegisterDependencies(List<string> assembliesPreffixes, List<string> excludes, string dependencyPlugin)
        {
            this.container.Configure(config => config.Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(assembly =>
                {
                    var assemblyName = assembly.GetName().Name;

                    return this.ShouldRegister(
                        dependencyPlugin,
                        assemblyName, 
                        assembliesPreffixes.ToArray(), 
                        excludes.ToArray());
                });

                scan.AddAllTypesOf<IPluginBoot>();
                scan.AddAllTypesOf<IConfiguracaoBoot>();
                scan.AddAllTypesOf<IFrameworkBoot>();
                scan.AddAllTypesOf<IApplicationBoot>();
                scan.AddAllTypesOf<ITarefaM2>();

                scan.LookForRegistries();
            }));
        }

        private bool ShouldRegister(
           string dependencyPlugin,
           string assemblyName,
           string[] prefixosArray,
           string[] excludes)
        {
            var shouldRegister =
                assemblyName.ComecaCom(prefixosArray) &&
                assemblyName.Equals("WebGrease") == false &&
                assemblyName.Equals("WebActivator") == false &&
                assemblyName.NaoContem(excludes);

            if (shouldRegister && assemblyName.Contains(".Plugin."))
            {
                shouldRegister = assemblyName.Equals(dependencyPlugin);
            }

            if (shouldRegister)
            {
                Log.Framework.DebugFormat("Assembly será registrado {0}", assemblyName);
            }

            return shouldRegister;
        }

        private object InternalResolve(Type type, string namedInstance)
        {
            if (type == null)
            {
                return null;
            }

            try
            {
                var instance = this.GetInstance(type, namedInstance);
                this.BuildUpInstance(instance);
                return instance;
            }
            catch (Exception exception)
            {
                Log.Framework.Error("Erro ao resolver " + type.Name, exception);
                throw;
            }
        }

        private void BuildUpInstance(object instance)
        {
            if (instance != null)
            {
                this.BuildUp(instance);
            }
        }

        private object GetInstance(Type type, string namedInstance)
        {
            if (string.IsNullOrEmpty(namedInstance))
            {
                return type.IsAbstract || type.IsInterface
                    ? this.container.TryGetInstance(type)
                    : this.container.GetInstance(type);
            }

            return type.IsAbstract || type.IsInterface
                ? this.container.TryGetInstance(type, namedInstance)
                : this.container.GetInstance(type, namedInstance);
        }
    }
}