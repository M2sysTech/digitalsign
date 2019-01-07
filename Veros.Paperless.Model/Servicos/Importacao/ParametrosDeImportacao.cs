namespace Veros.Paperless.Model.Servicos.Importacao
{
    public class ParametrosDeImportacao
    {
        public static int TamanhoDaParalelizacao
        {
            get
            {
#if DEBUG
                return 1;
#else
                return Veros.Framework.Aplicacao.Nucleos * 3;
#endif
            }
        }
    }
}