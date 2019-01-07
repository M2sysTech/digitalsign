namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface IProcedureRepositorio : IRepositorio<Documento>
    {
        void RegerarPdfPorDocumento(int documentoId);

        void RetornarLoteParaSeparacao(int loteId);

        void RecontarPaginasCertificado(int lotecefId);       
    }
}
