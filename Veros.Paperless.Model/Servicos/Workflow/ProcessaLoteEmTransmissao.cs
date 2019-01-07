namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Repositorios;
    using System.Linq;

    public class ProcessaLoteEmTransmissao : IProcessaFase<Lote>
    {
        private readonly IDocumentoRepositorio documentoRepositorio;

        public ProcessaLoteEmTransmissao(IDocumentoRepositorio documentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Processar(Lote lote, bool faseEstaAtiva)
        {
            if (lote.Status != LoteStatus.AguardandoTransmissao)
            {
                return;
            }

            //// TODO: ponto de saidas
            if (faseEstaAtiva)
            {
                //// TODO: consulta para retornar quantidade de documentos ao inves de trazer o lote inteiro
                ////var documentosInseridos = this.documentoRepositorio.ObterTodosPorLote(lote).Count();

                ////if (lote.QuantidadeDocumentos == documentosInseridos)
                ////{
                ////    lote.Status = LoteStatus.SetaReconhecimento;
                ////}
            }
            else
            {
                lote.Status = LoteStatus.SetaReconhecimento;
            }
        }
    }
}