namespace Veros.Paperless.Infra.Repositorios
{
    using System.Globalization;
    using Data.Hibernate;
    using Model.Entidades;
    using Model.Repositorios;

    /// <summary>
    /// TODO: refatorar. Poder pegar valores parseado pelo tipo
    /// </summary>
    public class TagRepositorio : Repositorio<Tag>, ITagRepositorio
    {
        public Tag ObterTagDeExpurgo()
        {
            return this.Session.QueryOver<Tag>()
                .Where(x => x.Descricao == "EXPURGO")
                .Take(1)
                .SingleOrDefault();
        }

        public Tag ObterDiretorioBancoOriginal()
        {
            return this.Session.QueryOver<Tag>()
                .Where(x => x.Descricao == "recepcao.diretorio.digitalizacao")
                .Take(1)
                .SingleOrDefault();
        }

        public string ObterValorPorNome(string nome, string valorPadrao = "")
        {
            var tag = this.ObterPorNome(nome);

            if (tag == null)
            {
                return valorPadrao;
            }

            return tag.Valor;
        }

        public Tag ObterPorNome(string nome)
        {
            return this.Session.QueryOver<Tag>()
                .Where(x => x.Descricao == nome)
                .Take(1)
                .SingleOrDefault();
        }

        public void AtualizaQualidadePorcentagem(string porcentagemm2Sys, string porcentagemCef)
        {
            this.Session
               .CreateQuery("update Tag set Valor = :porcentagemm2Sys where Chave = 'QUALIDADE_PORCENTAGEM_M2'")
               .SetAnsiString("porcentagemm2Sys", porcentagemm2Sys)
               .ExecuteUpdate();

            this.Session
               .CreateQuery("update Tag set Valor = :porcentagemCef where Chave = 'QUALIDADE_PORCENTAGEM_CEF'")
               .SetAnsiString("porcentagemCef", porcentagemCef)
               .ExecuteUpdate();
        }
    }
}
