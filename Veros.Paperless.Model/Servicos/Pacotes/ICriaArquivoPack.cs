namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using Veros.Paperless.Model.Entidades;

    public interface ICriaArquivoPack
    {
        void Executar(string pack, PacoteProcessado rajada, ArquivoPackStatus status, string observacao);
    }
}