namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Entidades;

    public interface IMontaDescricaoRegraAprovacaoServico
    {
        string Montar(RegraViolada regraViolada);
    }
}