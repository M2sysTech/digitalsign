namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Entidades;

    public interface ICapturaServico
    {
        Lote Iniciar(Usuario usuario);

        Processo Iniciar(ImagemConta proposta);

        Lote IniciarComCpf(string cpf, string token);

        void EncerrarCaptura(int loteId);
    }
}