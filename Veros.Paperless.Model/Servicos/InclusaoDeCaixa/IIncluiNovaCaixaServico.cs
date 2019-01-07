namespace Veros.Paperless.Model.Servicos.InclusaoDeCaixa
{
    using Entidades;

    public interface IIncluiNovaCaixaServico
    {
        void Executar(Pacote pacote);
    }
}
