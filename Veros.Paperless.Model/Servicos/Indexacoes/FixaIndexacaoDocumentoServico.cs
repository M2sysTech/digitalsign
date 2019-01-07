namespace Veros.Paperless.Model.Servicos.Indexacoes
{
    using Veros.Paperless.Model.Entidades;

    public class FixaIndexacaoDocumentoServico : IFixaIndexacaoDocumentoServico
    {
        private readonly IFixaIndexacaoCnhServico fixaIndexacaoCnhServico;
        private readonly IFixaIndexacaoComprovanteResidenciaServico fixaIndexacaoComprovanteResidenciaServico;
        private readonly IFixaIndexacaoDocumentoIdentificacaoServico fixaIndexacaoDocumentoIdentificacaoServico;

        public FixaIndexacaoDocumentoServico(
            IFixaIndexacaoCnhServico fixaIndexacaoCnhServico, 
            IFixaIndexacaoComprovanteResidenciaServico fixaIndexacaoComprovanteResidenciaServico, 
            IFixaIndexacaoDocumentoIdentificacaoServico fixaIndexacaoDocumentoIdentificacaoServico)
        {
            this.fixaIndexacaoCnhServico = fixaIndexacaoCnhServico;
            this.fixaIndexacaoComprovanteResidenciaServico = fixaIndexacaoComprovanteResidenciaServico;
            this.fixaIndexacaoDocumentoIdentificacaoServico = fixaIndexacaoDocumentoIdentificacaoServico;
        }

        public void Execute(Documento documento)
        {
            switch (documento.TipoDocumento.Id)
            {
                case TipoDocumento.CodigoCnh:
                    this.fixaIndexacaoCnhServico.Executar(documento);
                    break;
                
                case TipoDocumento.CodigoRg:
                case TipoDocumento.CodigoCie:
                case TipoDocumento.CodigoPassaporte:
                    this.fixaIndexacaoDocumentoIdentificacaoServico.Executar(documento);
                    break;

                case TipoDocumento.CodigoComprovanteDeResidencia:
                    this.fixaIndexacaoComprovanteResidenciaServico.Executar(documento);
                    break;
            }
        }
    }
}