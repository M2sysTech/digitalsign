namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using Entidades;
    
    public interface IPreparaContextoLoteCefServico
    {
        void Executar(ConfiguracaoDeLoteCef configuracaoDeLoteCef);
    }
}
