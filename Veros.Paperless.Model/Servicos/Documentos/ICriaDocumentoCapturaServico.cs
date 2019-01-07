namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Entidades;

    public interface ICriaDocumentoCapturaServico
    {
        Documento CriarForcado(
            int processoId, 
            int tipoDocumentoId, 
            int tipoSinistradoId, 
            string cpf, 
            string versao = "0", 
            bool pdfVirtual = false, 
            int documentoPaiId = 0);

        void CriarDocumentosPelosArquivos(
            IEnumerable<ImagemDoLote> imagensCapturadas, 
            int loteId, 
            string diretorioDestino, 
            string diretorioOrigem);

        string CaminhoDiretorioScan(int loteId);
    }
}
