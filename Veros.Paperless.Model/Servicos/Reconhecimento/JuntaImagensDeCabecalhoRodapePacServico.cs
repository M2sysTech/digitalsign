namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using System.IO;
    using System.Linq;
    using Framework;
    using Veros.Framework.IO;
    using Veros.Image;

    public class JuntaImagensDeCabecalhoRodapePacServico : IJuntaImagensDeCabecalhoRodapePacServico
    {
        private readonly ITratamentoImagem tratamentoImagens;
        private readonly IFileSystem fileSystem;

        public JuntaImagensDeCabecalhoRodapePacServico(
            ITratamentoImagem tratamentoImagens, 
            IFileSystem fileSystem)
        {
            this.tratamentoImagens = tratamentoImagens;
            this.fileSystem = fileSystem;
        }

        public string Executar(params string[] imagens)
        {
            var juntado = imagens.ToList();
            
            var arquivo = Path.Combine(
                Path.GetDirectoryName(imagens.ElementAt(0)), 
                Path.GetDirectoryName(imagens.ElementAt(0)) + "-juntada.tif");

            juntado.Add(arquivo);

            var imagemJuntada = this.tratamentoImagens.Juntar(juntado.ToArray());
            
            if (this.fileSystem.Exists(imagemJuntada) == false)
            {
                Log.Application.Debug("Não foi possível juntar imagens recortadas");
                return string.Empty;
            }

            Log.Application.Debug("Imagens juntadas com sucesso.");

            return imagemJuntada;
        }
    }
}