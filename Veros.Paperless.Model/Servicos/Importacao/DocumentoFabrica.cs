namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Linq;
    using Veros.Paperless.Model.Entidades;

    public class DocumentoFabrica : IDocumentoFabrica
    {
        public Documento Criar(Processo processo, TipoDocumento tipoDocumento, string cpf)
        {
            return new Documento
            {
                Lote = processo.Lote,
                Processo = processo,
                Status = DocumentoStatus.TransmissaoOk,
                TipoDocumento = tipoDocumento,
                TipoDocumentoOriginal = tipoDocumento,
                Cpf = cpf
            };
        }

        public Documento CriarFolhaRosto(Processo processo)
        {
            var tipoDocumento = new TipoDocumento
            {
                Id = TipoDocumento.CodigoFolhaDeRosto
            };  

            return new Documento
            {
                Lote = processo.Lote,
                Processo = processo,
                Status = DocumentoStatus.TransmissaoOk,
                TipoDocumento = tipoDocumento,
                TipoDocumentoOriginal = tipoDocumento,
                Ordem = 1,
                Virtual = true,
            };
        }

        public Documento CriarTermoAutuacao(Processo processo)
        {
            var tipoDocumento = new TipoDocumento
            {
                Id = TipoDocumento.CodigoTermoAutuacaoDossie
            };

            var documentos = processo.Documentos.Where(x => x.Virtual == true && x.Status != DocumentoStatus.Excluido);

            var quantidadeVirtuais = 0;

            quantidadeVirtuais = documentos.Count() != 0 ? documentos.OrderBy(x => x.Ordem).Last().Ordem : 0;

            return new Documento
            {
                Lote = processo.Lote,
                Processo = processo,
                Status = DocumentoStatus.TransmissaoOk,
                TipoDocumento = tipoDocumento,
                TipoDocumentoOriginal = tipoDocumento,
                Ordem = quantidadeVirtuais + 1, 
                Virtual = true,
            };
        }
    }
}
