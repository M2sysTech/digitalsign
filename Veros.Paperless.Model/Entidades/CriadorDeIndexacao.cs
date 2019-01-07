namespace Veros.Paperless.Model.Entidades
{
    using Veros.Paperless.Model.Repositorios;

    public static class CriadorDeIndexacao
    {
        public static Indexacao Create(
            IIndexacaoRepositorio indexacaoRepositorio,
            Campo campo,
            Documento documento)
        {
            //// TODO: validar se o pessoal do mdi chama isso (mdocdados) de indexacao
            var indexacao = indexacaoRepositorio.ObterPorCampoDeUmDocumento(campo.Id, documento);

            if (indexacao == null)
            {
                indexacao = new Indexacao { Campo = campo };
            }

            return indexacao;
        }
    }
}
