namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using Entidades;

    /// <summary>
    /// TODO: teste
    /// </summary>
    public class ResultadoDasCondicoes
    {
        public ResultadoDasCondicoes(
            Processo processo,
            Regra regra,
            int binarioDasCondicoesAtendidas, 
            bool foramAtendidas,
            string resultadoCalculado = "")
        {
            this.BinarioDasCondicoesAtendidas = binarioDasCondicoesAtendidas;
            this.ForamAtendidas = foramAtendidas;
            this.ResultadoCalculado = resultadoCalculado;

            this.RegraViolada = new RegraViolada
            {
                Processo = processo,
                Observacao = regra.Descricao,
                SomaDoBinario = this.BinarioDasCondicoesAtendidas,
                Regra = regra,
                Vinculo = regra.Vinculo,
                Status = this.ForamAtendidas ? RegraVioladaStatus.Pendente : RegraVioladaStatus.Aprovada
            };
        }

        public int BinarioDasCondicoesAtendidas
        {
            get;
            private set;
        }

        public bool ForamAtendidas
        {
            get;
            private set;
        }

        public RegraViolada RegraViolada
        {
            get;
            private set;
        }

        public Documento Documento
        {
            get;
            set;
        }

        public string ResultadoCalculado
        {
            get;
            private set;
        }
    }
}