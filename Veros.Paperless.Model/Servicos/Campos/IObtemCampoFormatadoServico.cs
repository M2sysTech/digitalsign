namespace Veros.Paperless.Model.Servicos.Campos
{
    using Veros.Paperless.Model.Entidades;

    public interface IObtemCampoFormatadoServico
    {
        string Obter(Campo campo, string valor);

        string Obter(Documento documento, int campoId, bool ignorarFormatacao = false);
    }
}