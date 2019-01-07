namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Framework;
    using Veros.Paperless.Model.Entidades;

    public class ValidaCondicaoDeRegra : IValidaCondicaoDeRegra
    {
        private readonly ICriadorDeProcessadorDeBinario criadorDeProcessadorDeBinario;

        public ValidaCondicaoDeRegra(ICriadorDeProcessadorDeBinario criadorDeProcessadorDeBinario)
        {
            this.criadorDeProcessadorDeBinario = criadorDeProcessadorDeBinario;
        }
        
        public ResultadoCondicaoDeRegra Validar(Processo processo, Regra regra, RegraCondicional condicao)
        {
            var processadorBinarios = this.criadorDeProcessadorDeBinario.Obter(condicao.Binario);

            if (processadorBinarios == null)
            {
                Log.Application.DebugFormat(
                    "Não foi encontrado processador do binario {0}", 
                    condicao.Binario);
            }

            return processadorBinarios.Processar(processo, regra, condicao);
        }
    }
}
