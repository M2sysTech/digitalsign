namespace Veros.Framework
{
    using System.Collections.Generic;
    using System.Linq;

    public class Dependencias
    {
        public static List<string> Prefixos
        {
            get;
            private set;
        }

        public static List<string> Excludes
        {
            get;
            private set;
        }

        public static void Registrar(
            string[] prefixosNoCodigo,
            string[] prefixosNaConfiguracao,
            string dependencyPlugin = "",
            bool incluiTestes = false)
        {
            Prefixos = new List<string>();
            Excludes = new List<string>();

            Prefixos.AddRange(new[] { Aplicacao.Nome });
            Prefixos.AddRange(prefixosNoCodigo);
            Prefixos.AddRange(prefixosNaConfiguracao);
            Prefixos = CombinarNamespacesDeTestes(Prefixos, incluiTestes);
            Prefixos = CombinarComPrefixosPadrao(Prefixos);

            if (incluiTestes == false)
            {
                Excludes.Add("Tests");
                Excludes.Add("TestFramework");
                Excludes.Add("SoftekBarcodeLib");
            }

            IoC.Current.RegisterDependencies(Prefixos, Excludes, dependencyPlugin);
        }

        private static List<string> CombinarNamespacesDeTestes(List<string> prefixos, bool incluiTestes)
        {
            var list = prefixos.ToList();

            if (incluiTestes == false)
            {
                return list;
            }

            list.Add("M2.IntegrationTests");
            list.Add("MdiDemo.IntegrationTests");
            list.Add("MdiDemo.IntegrationTests");
            return list;
        }

        private static List<string> CombinarComPrefixosPadrao(IEnumerable<string> prefixosDaAplicacao)
        {
            var list = prefixosDaAplicacao.ToList();

            if (list.Contains("Veros") == false)
            {
                list.Add("Veros");
            }

            if (list.Contains("M2Sys") == false)
            {
                list.Add("M2Sys");
            }

            return list;
        }
    }
}