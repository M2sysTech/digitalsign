namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.Collections.Generic;
    using Entidades;
    using Image.Reconhecimento;

    public interface ISalvarReconhecimentoPaginaServico
    {
        void Executar(Pagina pagina, ResultadoReconhecimento resultadoReconhecimento);
    }
}