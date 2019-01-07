namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System;
    using System.IO;
    using Entidades;
    using Framework;
    using Framework.IO;
    using Newtonsoft.Json;

    public class RecepcaoDePropostas : IRecepcaoDePropostas
    {
        private readonly IFileSystem fileSystem;
        
        public RecepcaoDePropostas(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Receber(int loteId, ImagemConta imagemConta)
        {
            if (loteId == 0)
            {
                throw new NullReferenceException("Informe o lote para recepção das imagens");
            }

            if (imagemConta.NaoTemConteudo())
            {
                throw new NullReferenceException("Não foi possível importar proposta. Proposta vazia ou nula.");
            }
            
            if (string.IsNullOrEmpty(imagemConta.Cpf))
            {
                throw new Exception("Não foi possível importar proposta. Não foi encontrado o número Cpf");
            }

            if (string.IsNullOrEmpty(imagemConta.FormatoBase64))
            {
                throw new Exception("Não foi possível importar proposta. Não foram encontrados os arquivos da conta no campo FormatoBase64");
            }

            if (imagemConta.TipoDocumentoId == 0)
            {
                throw new Exception("Não foi possível importar proposta. TipoDocumentoId não é válido");
            }

            try
            {
                this.CriarArquivoDeProposta(loteId, imagemConta);
            }
            catch (Exception exception)
            {
                Log.Application.Error(exception);
                throw;
            }
        }

        private void CriarArquivoDeProposta(int loteId, ImagemConta imagemConta)
        {
            var jsonConteudo = JsonConvert.SerializeObject(imagemConta, Formatting.Indented);

            var caminho = Path.Combine(Configuracao.CaminhoDePacotesRecebidos, loteId.ToString());

            if (Directory.Exists(caminho) == false)
            {
                Directory.CreateDirectory(caminho);
            }

            var jsonArquivo = imagemConta.Cpf + "_" + Guid.NewGuid() + ".json";

            var arquivo = Path.Combine(caminho, jsonArquivo);
            this.fileSystem.CreateFile(arquivo, jsonConteudo);
        }
    }
}