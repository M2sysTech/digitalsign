namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System.Collections.Generic;
    using Entidades;

    public interface IExecutaAjusteDeReclassificacaoServico
    {
        void Executar(IEnumerable<AjusteDeDocumento> ajustes);
    }
}
