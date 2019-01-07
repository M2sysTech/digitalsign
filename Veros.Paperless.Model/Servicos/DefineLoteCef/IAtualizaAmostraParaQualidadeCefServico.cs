namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using Entidades;

    public interface IAtualizaAmostraParaQualidadeCefServico
    {
        bool Executar(int loteCefId);

        bool LocalizarLotesAprovadosQualiM2(int lotecef, bool prioridade = false);
    }
}
