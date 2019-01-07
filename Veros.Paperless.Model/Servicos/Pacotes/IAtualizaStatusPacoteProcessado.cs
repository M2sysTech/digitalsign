namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using Entidades;

    public interface IAtualizaStatusPacoteProcessado
    {
        void Executar(PacoteProcessado rajada);
    }
}