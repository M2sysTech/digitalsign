namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using System.Linq;

    public class ImagensCapturadas
    {
        public ImagensCapturadas()
        {
            this.Documentos = new List<ArquivoCapturado>();
        }

        public string LoteId
        {
            get;
            set;
        }

        public string UsuarioId
        {
            get;
            set;
        }

        public string Foto
        {
            get;
            set;
        }
        
        public IList<ArquivoCapturado> Documentos
        {
            get;
            set;
        }

        public string CaminhoFichaPadrao
        {
            get; 
            set;
        }

        public void AdicionarDocumento(string nomeImagem, string tipoDocumento)
        {
            var arquivoCapturado = new ArquivoCapturado
            {
                Img = nomeImagem,
                Tipo = tipoDocumento
            };

            this.Documentos.Add(arquivoCapturado);
        }

        public IList<string> ObterImagens(int tipoDocumentoId)
        {
            if (tipoDocumentoId == TipoDocumento.CodigoFotoFrontal)
            {
                return this.ObterImagensDaFoto();
            }

            if (tipoDocumentoId == TipoDocumento.CodigoFichaDeCadastro)
            {
                return this.ObterFichaDeCadastro();
            }

            return null;
        }

        public IList<string> FrentesNaoIdentificadas()
        {
            var documentos = this.Documentos.Where(x => x.DocumentoFicha() == false);

            var documentosNaoIdentificados = new List<string>();

            foreach (var documento in documentos)
            {
                if (documento.EhFrente())
                {
                    documentosNaoIdentificados.Add(documento.Img);
                }
            }

            return documentosNaoIdentificados;
        }

        public IList<string> ObterPaginas(string paginaFrente)
        {
            var paginas = new List<string>
            {
                paginaFrente
            };

            var nomeVerso = paginaFrente.Replace("F.", "V.");

            var documentoVerso = this.Documentos.FirstOrDefault(x => x.Img == nomeVerso);

            if (documentoVerso != null && string.IsNullOrEmpty(documentoVerso.Img) == false)
            {
                paginas.Add(documentoVerso.Img);
            }

            return paginas;
        }

        public bool PossuiFicha()
        {
            return this.Documentos.Any(x => x.DocumentoFicha());
        }

        private IList<string> ObterImagensDaFoto()
        {
            return new List<string>
                {
                    this.Foto
                };
        }

        private IList<string> ObterFichaDeCadastro()
        {
            var imagens = this.Documentos.Where(x => x.DocumentoFicha()).Select(documento => documento.Img).ToList();

            if (imagens.Any() == false)
            {
                imagens.Add(this.CaminhoFichaPadrao);
            }

            return imagens;
        }
    }
}