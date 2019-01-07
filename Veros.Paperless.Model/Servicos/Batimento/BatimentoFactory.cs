namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using Experimental;
    using Experimental.BatimentoPorDocumento;
    using Framework;

    public class BatimentoFactory
    {
        public static IBatimentoDocumento Criar(Documento documento)
        {
            return IoC.Current.Resolve<BatimentoDocumento<QualquerDocumentoBatimento>>();
        }
    }
}