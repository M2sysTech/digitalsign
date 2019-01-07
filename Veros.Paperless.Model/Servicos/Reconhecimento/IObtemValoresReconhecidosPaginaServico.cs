namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using Entidades;
    using Image.Reconhecimento;

    public interface IObtemValoresReconhecidosPaginaServico
    {
        List<ValorReconhecido> Obter(Pagina pagina, ResultadoReconhecimento resultadoReconhecimento);
    }
}