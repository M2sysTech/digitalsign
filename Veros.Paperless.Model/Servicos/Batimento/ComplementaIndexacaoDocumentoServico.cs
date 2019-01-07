namespace Veros.Paperless.Model.Servicos.Batimento
{
    using Entidades;
    using Framework;
    using Repositorios;
    using System.Linq;
    using Veros.Paperless.Model.Servicos.Batimento.Experimental;

    public class ComplementaIndexacaoDocumentoServico : IComplementaIndexacaoDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private IBatimentoEComplementacaoDocumentoServico batimentoEComplementacaoDocumentoServico;
        
        public ComplementaIndexacaoDocumentoServico(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Execute(Documento documento, ImagemReconhecida imagemReconhecida, ResultadoBatimentoDocumento resultadoBatimento)
        {
            Log.Application.InfoFormat("Inicio do batimento do documento {0}", documento.Id);

            if (this.TipoPodeFazerBatimento(documento.TipoDocumento))
            {
                return;
            }

            switch (documento.TipoDocumento.Id)
            {
             ////Por enquanto, no projeto Banco Original, não há necessidade de analisar por tipo de doc.
                ////case TipoDocumento.CodigoComprovanteDeResidencia:
                ////    this.batimentoEComplementacaoDocumentoServico = 
                ////    IoC.Current.Resolve<BatimentoEComplementacaoComprovanteResidenciaServico>();
                ////    break;
                default:
                    this.batimentoEComplementacaoDocumentoServico =
                    IoC.Current.Resolve<BatimentoEComplementacaoDocumentoServico>();
                    break;
            }

            var camposJaBatidosId = (from x in resultadoBatimento.Campos where x.Batido select x.Indexacao.Campo.Id).ToList();
            this.batimentoEComplementacaoDocumentoServico.Execute(documento, imagemReconhecida, camposJaBatidosId);

            this.documentoRepositorio.Salvar(documento);
        }

        private bool TipoPodeFazerBatimento(TipoDocumento tipoDocumento)
        {
            return tipoDocumento.Id == TipoDocumento.CodigoDocumentoGeral || tipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado;
        }
    }
}   
