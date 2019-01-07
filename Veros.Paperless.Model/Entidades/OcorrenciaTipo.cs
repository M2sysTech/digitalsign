namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class OcorrenciaTipo : Entidade
    {
        public static int TipoDocumentoNaoEncontrado = 21;
        public static int TipoDocumentoAdicionado = 43;

        public virtual string Nome
        {
            get;
            set;
        }

        public virtual int TipoOcorrenciaParaFilhos
        {
            get;
            set;
        }
    }
}
