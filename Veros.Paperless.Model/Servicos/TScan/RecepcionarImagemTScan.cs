namespace Veros.Paperless.Model.Servicos.TScan
{
    using System;
    using System.IO;
    using Framework;
    using Framework.IO;
    using Framework.Security;

    public class RecepcionarImagemTScan : IRecepcionarImagemTScan
    {
        private readonly ICryptography crypto;
        private readonly IFileSystem fileSystem;

        public RecepcionarImagemTScan(
            ICryptography crypto,
            IFileSystem fileSystem)
        {
            this.crypto = crypto;
            this.fileSystem = fileSystem;
        }

        public void Execute(
            bool verso,
            string nomeArquivo,
            int id,
            bool enviado,
            string conteudoEmBase64,
            int loteId,
            string token,
            string hash)
        {
            var mensagem = this.crypto.Decode(token);
            Log.Application.InfoFormat("Mensagem decifrada.");

            if (this.MensagemInvalida(mensagem, verso, nomeArquivo, id, loteId, enviado, hash))
            {
                Log.Application.InfoFormat("token invalido.");
                throw new Exception("Token Inválido");
            }

            var diretorio = this.CaminhoDiretorio(loteId);

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var caminhoDoArquivo = Path.Combine(diretorio, nomeArquivo);
          
            this.fileSystem.CreateFileFromBase64(caminhoDoArquivo, conteudoEmBase64);
        }

        public string CaminhoDiretorio(int loteId)
        {
            return Path.Combine(Aplicacao.Caminho, "Upload", loteId.ToString());
        }

        private bool MensagemInvalida(
            string mensagem,
            bool verso,
            string nomeArquivo,
            int id,
            int loteId,
            bool upado,
            string hash)
        {
            var campos = mensagem.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            var subiu = bool.Parse(campos[3]).Equals(upado);
            var versoDaImagem = bool.Parse(campos[4]).Equals(verso);

            var valido = campos[0].Equals("*" + id) &&
            campos[1].Equals(loteId.ToString()) &&
            campos[2].Equals(nomeArquivo) &&
            subiu &&
            versoDaImagem &&
            campos[5].Equals(hash);

            return valido == false;
        }
    }
}