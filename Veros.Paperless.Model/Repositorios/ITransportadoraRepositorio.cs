namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ITransportadoraRepositorio : IRepositorio<Transportadora>
    {
        Transportadora ObterPeloCnpj(string cnpj);

        IList<Transportadora> ObterAtivas();

        void AlterarStatus(int transportadoraId, string status);
    }
}
