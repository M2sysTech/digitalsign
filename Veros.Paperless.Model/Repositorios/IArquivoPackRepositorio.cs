namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IArquivoPackRepositorio : IRepositorio<ArquivoPack>
    {
        IList<ArquivoPack> ObterComTimeout();
        
        void AtualizaStatus(int arquivoPackId, ArquivoPackStatus arquivoPackStatus);
    }
}