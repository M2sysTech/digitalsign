namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using Entidades;
    using Repositorios;

    public class PreparaContextoLoteCefServico : IPreparaContextoLoteCefServico
    {
        private readonly ILoteCefRepositorio loteCefRepositorio;

        public PreparaContextoLoteCefServico(
            ILoteCefRepositorio loteCefRepositorio)
        {
            this.loteCefRepositorio = loteCefRepositorio;
        }

        public void Executar(ConfiguracaoDeLoteCef configuracaoDeLoteCef)
        {
            Contexto.LotesCef.Clear();
            var lotesCef = this.loteCefRepositorio.ObterAbertos(configuracaoDeLoteCef);

            while (lotesCef.Count < 1)
            {
                var novoLote = LoteCef.Novo();
                this.loteCefRepositorio.Salvar(novoLote);

                lotesCef.Add(novoLote);
            }

            foreach (var loteCef in lotesCef)
            {
                this.AdicionaNoContexto(loteCef.Id, loteCef.QuantidadeDeLotes);
            }
        }

        private void AdicionaNoContexto(int loteCefId, int quantidade)
        {
            if (Contexto.LotesCef.ContainsKey(loteCefId))
            {
                Contexto.LotesCef[loteCefId] = quantidade;
                return;
            }

            Contexto.LotesCef.Add(loteCefId, quantidade);
        }
    }
}
