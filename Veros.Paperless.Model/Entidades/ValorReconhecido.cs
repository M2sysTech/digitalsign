namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;

    public class ValorReconhecido : Entidade
    {
        public virtual TipoDado DataType
        {
            get;
            set;
        }

        public virtual Pagina Pagina
        {
            get;
            set;
        }

        public virtual string Value
        {
            get;
            set;
        }

        public virtual string CampoTemplate
        {
            get;
            set;
        }

        public virtual string TemplateName
        {
            get;
            set;
        }

        public virtual int Left
        {
            get;
            set;
        }

        public virtual int Right
        {
            get;
            set;
        }

        public virtual int Width
        {
            get;
            set;
        }

        public virtual int Height
        {
            get;
            set;
        }

        public virtual int Top
        {
            get;
            set;
        }

        public virtual int Bottom
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual void SetarCampoReconhecidoDoEngine(CampoReconhecido campoReconhecidoEngine)
        {
            if (campoReconhecidoEngine == null)
            {
                return;
            }

            this.CampoTemplate = campoReconhecidoEngine.Nome;
            this.TemplateName = campoReconhecidoEngine.NomeDoTemplate;
            this.Left = campoReconhecidoEngine.Area.Left;
            this.Right = campoReconhecidoEngine.Area.Right;
            this.Width = campoReconhecidoEngine.Area.Width;
            this.Height = campoReconhecidoEngine.Area.Height;
            this.Top = campoReconhecidoEngine.Area.Top;
            this.Bottom = campoReconhecidoEngine.Area.Bottom;
        }

        public virtual void SetarCampo(
            IList<MapeamentoCampo> mapeamentos,
            CampoReconhecido campoReconhecidoEngine)
        {
            if (mapeamentos.Count == 0)
            {
                return;
            }

            var mapeamento = mapeamentos
                .Where(x => x.NomeCampoNoTemplate.ToLower() == campoReconhecidoEngine.Nome.ToLower())
                .Where(x => x.NomeTemplate.ToLower() == campoReconhecidoEngine.NomeDoTemplate.ToLower())
                .ToList();

            if (mapeamento.Count > 0)
            {
                this.Campo = mapeamento.ElementAt(0).Campo;
            }
        }
    }
}
