namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IDocumentoFabrica
    {
        Documento Criar(Processo processo,
            TipoDocumento tipoDocumento,
            string cpf);

        Documento CriarFolhaRosto(Processo processo);

        Documento CriarTermoAutuacao(Processo processo);
    }
}
