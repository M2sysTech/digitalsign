namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using System.Linq;

    public class EstoqueFilasProducao
    {
        public EstoqueFilasProducao()
        {
            this.CapturaRecaptura = new FaseCapturaRecaptura();
            this.SeparacaoAutomatica = new FaseSeparacaoAutomatica();
            this.TriagemOcr = new FilaTriagemOcr();
            this.IdentificacaoManual = new FaseIdentificacaoManual();
            this.QualidadeM2Sys = new FaseQualidadeM2Sys();
            this.Ajustes = new FaseAjustes();
            this.AjustesQCef = new FaseAjustesQCef();
        }

        public FaseCapturaRecaptura CapturaRecaptura
        {
            get;
            set;
        }

        public FaseSeparacaoAutomatica SeparacaoAutomatica
        {
            get;
            set;
        }

        public FilaTriagemOcr TriagemOcr
        {
            get;
            set;
        }

        public FaseIdentificacaoManual IdentificacaoManual
        {
            get;
            set;
        }

        public FaseQualidadeM2Sys QualidadeM2Sys
        {
            get;
            set;
        }

        public FaseAjustes Ajustes
        {
            get;
            set;
        }

        public FaseAjustesQCef AjustesQCef
        {
            get;
            set;
        }

        public static EstoqueFilasProducao Criar(
            IList<FaseCapturaRecaptura> estoqueCapturaRecaptura,
            IList<FaseSeparacaoAutomatica> estoqueSeparacaoAutomatica,
            IList<FilaTriagemOcr> estoqueTriagemOcr,
            IList<FaseIdentificacaoManual> estoqueIdentificacaoManual,
            IList<FaseQualidadeM2Sys> estoqueQualidadeM2,
            IList<FaseAjustes> estoqueAjuste,
            IList<FaseAjustesQCef> estoqueAjusteQCef)
        {
            return new EstoqueFilasProducao
            {
                CapturaRecaptura = estoqueCapturaRecaptura.FirstOrDefault(),
                SeparacaoAutomatica = estoqueSeparacaoAutomatica.FirstOrDefault(),
                Ajustes = estoqueAjuste.FirstOrDefault(),
                TriagemOcr = estoqueTriagemOcr.FirstOrDefault(),
                IdentificacaoManual = estoqueIdentificacaoManual.FirstOrDefault(),
                QualidadeM2Sys = estoqueQualidadeM2.FirstOrDefault(),
                AjustesQCef = estoqueAjusteQCef.FirstOrDefault()
            };
        }

        public class FaseAjustes : EstoqueBase
        {
        }

        public class FaseAjustesQCef : EstoqueBase
        {
        }
    
        public class FaseCapturaRecaptura : EstoqueBase
        {
        }

        public class FaseQualidadeM2Sys : EstoqueBase
        {
        }

        public class FilaTriagemOcr : EstoqueBase   
        {
        }

        public class FaseIdentificacaoManual : EstoqueBase
        {
        }

        public class FaseSeparacaoAutomatica : EstoqueBase
        {
        }

        public class EstoqueBase
        {
            public int Alocado
            {
                get;
                set;
            }

            public int RestaTerminar
            {
                get;
                set;
            }

            public int Total
            {
                get
                {
                    return this.Alocado + this.RestaTerminar;
                }
            }
        }
    }
}