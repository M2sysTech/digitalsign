namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;

    public class CriadorDeProcessadorDeBinario : ICriadorDeProcessadorDeBinario
    {
        private static Dictionary<Binario, IProcessadorDeBinario> processadoresBinario;

        public IProcessadorDeBinario Obter(Binario binario)
        {
            PopulaSeNecessario();
            return processadoresBinario[binario];
        }

        private static void PopulaSeNecessario()
        {
            if (processadoresBinario == null)
            {
                processadoresBinario = new Dictionary<Binario, IProcessadorDeBinario>
                {
                    { Binario.B0, IoC.Current.Resolve<ProcessadorBinarioB0>() },
                    { Binario.B1, IoC.Current.Resolve<ProcessadorBinarioB1>() },
                    { Binario.B2, IoC.Current.Resolve<ProcessadorBinarioB2>() },
                    { Binario.B4, IoC.Current.Resolve<ProcessadorBinarioB4>() },
                    { Binario.B8, IoC.Current.Resolve<ProcessadorBinarioB8>() },
                    { Binario.B16, IoC.Current.Resolve<ProcessadorBinarioB16>() },
                    { Binario.B32, IoC.Current.Resolve<ProcessadorBinarioB32>() },
                    { Binario.B64, IoC.Current.Resolve<ProcessadorBinarioB64>() },
                    { Binario.B128, IoC.Current.Resolve<ProcessadorBinarioB128>() },
                    { Binario.B256, IoC.Current.Resolve<ProcessadorBinarioB256>() },
                };
            }
        }
    }
}