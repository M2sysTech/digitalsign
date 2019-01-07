namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class ObterImagensCapturadas
    {
        public List<ImagemConta> Executar(List<ArquivoCapturado> imagensDaConta)
        {
            var imagens = imagensDaConta.GroupBy(i => new
            {
                i.TipoDocumentoId,
                i.ImagemConta.Face
            });

            var imagensOrdenadas = new List<ImagemConta>();

            foreach (var grupo in imagens)
            {
                if (grupo.Count() == 1)
                {
                    imagensOrdenadas.Add(grupo.ElementAt(0).ImagemConta);
                }
                else
                {
                    var ultimaImagem = this.ObterUltimaImagem(grupo);
                    imagensOrdenadas.Add(ultimaImagem.ImagemConta);
                }
            }

            return imagensOrdenadas;
        }

        private ArquivoCapturado ObterUltimaImagem(IEnumerable<ArquivoCapturado> grupo)
        {
            var ultimoArquivo = grupo.OrderByDescending(x => x.DataCriacaoArquivo).First();
            return ultimoArquivo;
        }
    }
}