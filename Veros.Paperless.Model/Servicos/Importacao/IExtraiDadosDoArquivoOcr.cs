namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IExtraiDadosDoArquivoOcr
    {
        List<ValorReconhecido> Execute(Processo processo, string nomeDaPasta);
    }
}
