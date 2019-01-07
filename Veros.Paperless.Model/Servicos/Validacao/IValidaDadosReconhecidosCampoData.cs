namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IValidaDadosReconhecidosCampoData
    {
        bool PossuiMesNumerico(Indexacao indexacao);
    }
}