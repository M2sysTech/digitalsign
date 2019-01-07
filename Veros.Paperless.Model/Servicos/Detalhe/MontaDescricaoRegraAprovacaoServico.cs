namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Entidades;

    public class MontaDescricaoRegraAprovacaoServico : IMontaDescricaoRegraAprovacaoServico
    {
        public string Montar(RegraViolada regraViolada)
        {
            return string.Format("{0} ({1}{2})", 
                regraViolada.Regra.Descricao, 
                this.CodigoDoDocumento(regraViolada), 
                this.CodigoDoCheckList(regraViolada));
        }

        private string CodigoDoDocumento(RegraViolada regraViolada)
        {
            return regraViolada.Documento == null || string.IsNullOrEmpty(regraViolada.Vinculo) ? 
                "sem vinculo" :
                string.Format("doc {0}", regraViolada.Documento.ObterValorFinal(Campo.ReferenciaDeArquivoCep));
        }

        private object CodigoDoCheckList(RegraViolada regraViolada)
        {
            return string.IsNullOrEmpty(regraViolada.Vinculo) ? string.Empty : string.Format(" checklist {0}", regraViolada.Vinculo);
        }
    }
}