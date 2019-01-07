namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using System.Linq;
    using Entidades;

    public class DefineLoteCefServico : IDefineLoteCefServico
    {
        public void Executar(Lote lote)
        {
            if (lote.LoteCef != null)
            {
                return;
            }
            
            var loteCefId = this.ObterLoteCef();

            if (loteCefId > 0)
            {
                lote.LoteCef = new LoteCef { Id = loteCefId };    
            }
        }

        private int ObterLoteCef()
        {
            var item = Contexto.LotesCef.Where(x => x.Key > 0).OrderBy(x => x.Key).FirstOrDefault();

            if (item.Key > 0)
            {
                Contexto.LotesCef[item.Key] = item.Value + 1;
                return item.Key;
            }

            return 0;
        }
    }
}
