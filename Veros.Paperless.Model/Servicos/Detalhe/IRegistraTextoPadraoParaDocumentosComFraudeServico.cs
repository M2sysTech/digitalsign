namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using Entidades;

    public interface IRegistraTextoPadraoParaDocumentosComFraudeServico
    {
        void Executar(Processo processo);
    }
}