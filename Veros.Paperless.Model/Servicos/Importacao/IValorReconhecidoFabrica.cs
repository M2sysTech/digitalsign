namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using System;

    public interface IValorReconhecidoFabrica
    {
        List<ValorReconhecido> Criar(Pagina pagina, List<Tuple<string, string>> campos);
    }
}
