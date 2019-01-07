namespace Veros.Paperless.Model.Servicos.Ocorrencias
{
    using Entidades;

    public interface IGravaOcorrenciasNosFilhosServico
    {
        void Executar(Ocorrencia ocorrencia);
    }
}
