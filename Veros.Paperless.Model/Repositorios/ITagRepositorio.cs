namespace Veros.Paperless.Model.Repositorios
{
    using Entidades;
    using Framework.Modelo;

    public interface ITagRepositorio : IRepositorio<Tag>
    {
        Tag ObterTagDeExpurgo();

        Tag ObterDiretorioBancoOriginal();

        string ObterValorPorNome(string nome, string valorPadrao = "");

        Tag ObterPorNome(string nome);

        void AtualizaQualidadePorcentagem(string porcentagemm2Sys, string porcentagemCef);
    }
}
