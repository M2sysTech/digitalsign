namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IArquivoColetaRepositorio : IRepositorio<ArquivoColeta>
    {
        ArquivoColeta ObterUltimo(int coletaId);
        
        ArquivoColeta ObterUltimo(int coletaId, string nomeArquivo);

        void FinalizarPendentes(int coletaId);

        void AlterarStatus(ArquivoColeta arquivo, string status);
    }
}