namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;

    public interface IInformacoesReconhecimento
    {
        ImagemReconhecida Obter(Documento documento);
    }
}