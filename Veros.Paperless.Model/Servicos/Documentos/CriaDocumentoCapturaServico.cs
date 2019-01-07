namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class CriaDocumentoCapturaServico : ICriaDocumentoCapturaServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly IPaginaRepositorio paginaRepositorio;
        
        public CriaDocumentoCapturaServico(
            IProcessoRepositorio processoRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.paginaRepositorio = paginaRepositorio;
        }

        public Documento CriarForcado(
            int processoId, 
            int tipoDocumentoId, 
            int tipoSinistradoId, 
            string cpf,
            string versao = "0",
            bool pdfVirtual = false, 
            int documentoPaiId = 0)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);
            var tipoDocumento = new TipoDocumento { Id = tipoDocumentoId };

            var documento = Documento.Novo(tipoDocumento, cpf, processo.Lote, processo, versao);
            documento.SequenciaTitular = tipoSinistradoId;
            documento.Virtual = pdfVirtual;
            documento.DocumentoPaiId = documentoPaiId;

            this.documentoRepositorio.Salvar(documento);

            return documento;
        }

        public void CriarDocumentosPelosArquivos(
            IEnumerable<ImagemDoLote> imagensCapturadas, 
            int loteId, 
            string diretorioDestino, 
            string diretorioOrigem)
        {
            Log.Application.DebugFormat("Criando documentos (de captura) para o lote [{0}] ", loteId);

            var ultimoNomeDoArquivo = string.Empty;

            foreach (var imagemCapturada in imagensCapturadas)
            {
                var nomeDoArquivo = Path.GetFileNameWithoutExtension(imagemCapturada.NomeArquivo);
                var arquivoCapturado = ArquivoCapturado.CriarArquivo(imagemCapturada.NomeArquivo.Split('_'));

                if (this.CopiarParaDestino(imagemCapturada.NomeArquivo, diretorioDestino, diretorioOrigem, loteId) == false)
                {
                    continue;
                }

                if (ultimoNomeDoArquivo == arquivoCapturado.NomeArquivo())
                {
                    continue;
                }

                try
                {
                    var processo = this.processoRepositorio.ObterPorLoteComDocumentos(loteId).FirstOrDefault();
                        
                    var versao = 0;
                    var documentoPaiId = 0;
            
                    //// cria o novo doc
                    var documento = this.CriarForcado(
                        processo.Id, 
                        arquivoCapturado.TipoDocumentoId, 
                        arquivoCapturado.TipoSinistradoId, 
                        arquivoCapturado.Cpf, 
                        versao.ToString(),
                        false,
                        documentoPaiId);

                    ultimoNomeDoArquivo = arquivoCapturado.NomeArquivo();
                    Log.Application.DebugFormat("MDOC do tipo [{1}] criado com sucesso no lote [{0}]", loteId, arquivoCapturado.TipoDocumentoId);
                }
                catch (Exception exception)
                {
                    Log.Application.Error(string.Format("Erro ao criar MDOC no lote [{0}]", loteId), exception);
                    throw;
                }
            }            
        }

        public string CaminhoDiretorioScan(int loteId)
        {
            return Path.Combine(Contexto.PastaArquivosDossie, loteId.ToString());
        }

        private void ExcluirDocumento(Documento documento)
        {
            documento.Status = DocumentoStatus.Excluido;
            this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.Excluido);
            this.paginaRepositorio.AlterarStatusDaPaginaPorDocumento(documento.Id, PaginaStatus.StatusExcluida);

            this.gravaLogDoLoteServico.Executar(
                LogLote.AcaoConverterDocumentacaoGeral,
                documento.Lote.Id,
                string.Format("Documento [{0}] foi excluído. Foi gerada uma nova versão.", documento.Id));

            Log.Application.DebugFormat("Documento [{0}] foi excluído. Foi gerada uma nova versão.", documento.Id);
        }

        private void ExcluirTodosDocumentosAtuais(Processo processo)
        {
            foreach (var documento in processo.Documentos)
            {
                this.ExcluirDocumento(documento);
            }
        }

        private bool CopiarParaDestino(
            string arqvAtual, 
            string diretorioDestino,
            string diretorioOrigem, 
            int loteId)
        {
            var nomeArqv = Path.GetFileName(arqvAtual);

            try
            {
                File.Copy(Path.Combine(diretorioOrigem, loteId.ToString(), nomeArqv), Path.Combine(diretorioDestino, nomeArqv), true);
            }
            catch (Exception exception)
            {
                Log.Application.Error(string.Format("Erro ao copiar para pasta de upload no lote [{0}]", loteId), exception);
                return false;
            }

            Log.Application.DebugFormat("Pagina [{1}] copiada com sucesso no lote [{0}]", loteId, nomeArqv);

            return true;
        }
    }
}
