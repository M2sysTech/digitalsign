namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Veros.Paperless.Model.Entidades;

    public interface IVerificaSeNomeReconhecidoServico
    {
        bool Verificar(string palavra, ImagemReconhecida valoresReconhecidos);
    }
}