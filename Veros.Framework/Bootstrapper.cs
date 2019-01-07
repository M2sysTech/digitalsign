//-----------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Service;
    using Tarefas;
    using log4net.Core;

    public class Bootstrapper
    {
        private static bool executado;

        public static void Executar(
            string caminhoDaAplicacao = "", 
            bool executarApplicationBoot = true,
            bool incluiTestes = false,
            LogConfiguration logConfiguration = null,
            params string[] prefixosDeDependencias)
        {
            if (executado)
            {
                Log.Application.DebugFormat("Foi chamado Bootstrapper.Executar mas o boot já foi executado");
                return;
            }

            try
            {
                Aplicacao.MainAssembly = Assembly.GetCallingAssembly();
                Aplicacao.EstaRodandoComoTesteAutomatizado = incluiTestes;
                Aplicacao.Configurar(caminhoDaAplicacao);
                Log.Configurar(logConfiguration);

                Log.Application.Info("--------------------------------------------------------------");
                Log.Application.InfoFormat("Inicializando {0}", Aplicacao.Nome);
                Log.Application.InfoFormat("Versão: {0}", Aplicacao.Versao);
                Log.Application.InfoFormat("Local: {0}", Aplicacao.Caminho);
                Log.Application.DebugFormat("x64 bit process? {0}", Aplicacao.Rodando64Bits);

                AssemblyRedirect();

                Dependencias.Registrar(
                    prefixosDeDependencias,
                    SettingsConfig.DependencyPrefixes,
                    SettingsConfig.DependencyPlugin,
                    incluiTestes);

                ExecutarTarefasDoTipo<IPluginBoot>();
                ExecutarTarefasDoTipo<IConfiguracaoBoot>();
                ExecutarTarefasDoTipo<IFrameworkBoot>();

                if (executarApplicationBoot)
                {
                    ExecutarTarefasDoTipo<IApplicationBoot>();
                }

                Log.Application.FatalFormat("{0} iniciado", Aplicacao.Nome);

                executado = true;
            }
            catch (Exception exception)
            {
                Log.Application.Fatal("Erro ao executar Bootstrapper.Executar", exception);
                throw;
            }

            ExecutarM2SeFoiPassadoParametros();
        }

        private static void ExecutarM2SeFoiPassadoParametros()
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length == 1)
            {
                return;
            }

            if (args[1].ToLower() == "m2")
            {
                Log.Configurar(new LogConfiguration
                {
                    ConsoleAtivo = true,
                    Level = Level.Info
                });

                var newArgs = new List<string>(args);
                newArgs.RemoveAt(0);
                newArgs.RemoveAt(0);
                new TarefaExecutor().Executar(newArgs.ToArray());

                Environment.Exit(0);
            }
        }

        private static void AssemblyRedirect()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;
        }

        private static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs e)
        {
            var nomeAssembly = e.Name.Split(',').FirstOrDefault();

            try
            {
                var caminho = Path.Combine(Aplicacao.Caminho, nomeAssembly + ".dll");
                Assembly assembly = Assembly.LoadFrom(caminho);
                return assembly;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void ExecutarTarefasDoTipo<T>() where T : IExecutable
        {
            Log.Framework.DebugFormat("Carregando tarefas {0}", typeof(T).Name);

            foreach (var task in IoC.Current.GetAllInstances<T>())
            {
                Log.Framework.DebugFormat("Executando {0}", task.GetType().FullName);

                try
                {
                    task.Execute();
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat(exception, "Erro ao executar {0}", task.GetType().FullName);
                }
            }
        }
    }
}