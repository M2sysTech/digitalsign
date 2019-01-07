namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using Framework;
    using Framework.IO;
    using Image;

    public class RecortaCabecalhoRodapeDocumentoPacServico : IRecortaCabecalhoRodapeDocumentoPacServico
    {
        private readonly ITratamentoImagem tratamentoImagem;
        private readonly IFileSystem fileSystem;

        public RecortaCabecalhoRodapeDocumentoPacServico(
            ITratamentoImagem tratamentoImagem, 
            IFileSystem fileSystem)
        {
            this.tratamentoImagem = tratamentoImagem;
            this.fileSystem = fileSystem;
        }

        public string[] Executar(int documentoId, string imagemPaginaPac)
        {
            var diretorioDocumento = Path.Combine(Path.GetDirectoryName(imagemPaginaPac), documentoId.ToString(CultureInfo.InvariantCulture));
            Directories.CreateIfNotExist(diretorioDocumento);

            var nomeImagemSemExtensao = Path.GetFileNameWithoutExtension(imagemPaginaPac);
            var extensao = Path.GetExtension(imagemPaginaPac);

            var pacCabecalho = Path.Combine(diretorioDocumento, nomeImagemSemExtensao + "_cabecalho" + extensao);
            var pacCortadaNaBase = Path.Combine(diretorioDocumento, nomeImagemSemExtensao + "_temporaria" + extensao);
            var pacRodape = Path.Combine(diretorioDocumento, nomeImagemSemExtensao + "_rodape" + extensao);

            try
            {
                this.tratamentoImagem.Cortar(imagemPaginaPac, pacCabecalho, Corte.ApartirDoTopo, new Point(2000, 250));
                this.tratamentoImagem.Cortar(imagemPaginaPac, pacCortadaNaBase, Corte.ApartirDaBase, new Point(2000, 650));
                this.tratamentoImagem.Cortar(pacCortadaNaBase, pacRodape, Corte.ApartirDoTopo, new Point(2000, 150));

                Log.Application.DebugFormat("Recortes realizados com sucesso do documento #{0}", documentoId);
            }
            catch (Exception exception)
            {
                Log.Application.DebugFormat("não foi possível recortar documento #{0}", documentoId);
                return null;
            }

            if (this.fileSystem.Exists(pacCabecalho) == false || 
                this.fileSystem.Exists(pacRodape) == false)
            {
                Log.Application.DebugFormat("não foi possível recortar documento #{0}", documentoId);
                return null;
            }

            var recortes = new[] { pacCabecalho, pacRodape };

            return recortes;
        }
    }
}