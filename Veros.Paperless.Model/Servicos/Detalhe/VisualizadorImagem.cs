namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using Entidades;

    public class VisualizadorImagem
    {
        public string NomeViewer
        {
            get;
            set;
        }

        public Documento Documento
        {
            get;
            set;
        }

        public int PaginaInicial
        {
            get;
            set;
        }

        public string FuncaoJsDetalheDocumento
        {
            get;
            set;
        }

        public string FuncaoJsIndicioDeFraude
        {
            get;
            set;
        }

        public string FuncaoJsCarregarImagensOriginais
        {
            get;
            set;
        }

        public bool CarregarImagensOriginais
        {
            get;
            set;
        }

        public string FuncaoJsAbrirPopUp
        {
            get;
            set;
        }
    }
}