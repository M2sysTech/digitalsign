namespace Veros.Data
{
    using System.Collections.Generic;

    public interface IDatabaseSchema
    {
        void AtualizarParaUltimoMigration();

        void SetarVersaoManualmente(long version);

        List<long> ObterTodosMigrations();

        long ObterUltimaVersaoNaoAplicado();

        List<long> ObterMigrationsAplicados();

        List<long> ObterMigrationsNaoAplicados();
    }
}
