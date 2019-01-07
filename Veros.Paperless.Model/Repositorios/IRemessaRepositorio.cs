namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IRemessaRepositorio : IRepositorio<Remessa>
    {
        void AlterarStatusPorProcesso(int processoId, RemessaStatus status);

        void FinalizaRemessaAposRetorno(string nomeArquivo, string extensao);

        void FinalizaRemessaAposExport(int processoId);
    }
}
