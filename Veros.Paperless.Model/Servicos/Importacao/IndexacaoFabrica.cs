namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Comparacao;
    using Framework;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class IndexacaoFabrica : IIndexacaoFabrica
    {
        private readonly ICampoRepositorio campoRepositorio;

        public IndexacaoFabrica(ICampoRepositorio campoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;
        }

        public Indexacao Criar(Documento documento, int campoId, string valor)
        {
            return new Indexacao
            {
                Campo = new Campo { Id = campoId },
                SegundoValor = string.IsNullOrEmpty(valor) ? valor : valor.ToUpper().Trim(),
                Documento = documento
            };
        }

        public Indexacao Criar(Documento documento, string campoRefArquivo, string valor)
        {
            var campo = this.campoRepositorio
                .ObterPorReferenciaDeArquivo(documento.TipoDocumento, campoRefArquivo);

            if (campo == null)
            {
                Log.Application.WarnFormat(
                    "Não foi possível inserir indexação do refarquivo {0} para o documento {1}",
                    campoRefArquivo,
                    documento.Id);

                return null;
            }

            return new Indexacao
            {
                Campo = new Campo { Id = campo.Id },
                SegundoValor = string.IsNullOrEmpty(valor) ? valor : valor.ToUpper().Trim().RemoverDiacritico(),
                Documento = documento
            }; 
        }
    }
}
