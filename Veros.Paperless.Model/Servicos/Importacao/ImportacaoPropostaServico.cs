namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Comparacao;
    using Complementacao;
    using Entidades;
    using Framework;
    using Repositorios;
    using Storages;

    public class ImportacaoPropostaServico : IImportacaoPropostaServico
    {
        private readonly IAdicionaDocumentoAoProcessoServico adicionaDocumentoAoProcessoServico;
        private readonly IPropostaAberturaContaServico propostaAberturaConta;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IndexacaoDocumento indexacaoDocumento;
        private readonly IIndexacaoFabrica indexacaoFabrica;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IConsultaVertrosStorage consultaVertrosStorage;

        public ImportacaoPropostaServico(
            IAdicionaDocumentoAoProcessoServico adicionaDocumentoAoProcessoServico,
            IPropostaAberturaContaServico propostaAberturaConta,
            IProcessoRepositorio processoRepositorio,
            IndexacaoDocumento indexacaoDocumento,
            IIndexacaoFabrica indexacaoFabrica,
            IIndexacaoRepositorio indexacaoRepositorio,
            IConsultaVertrosStorage consultaVertrosStorage)
        {
            this.adicionaDocumentoAoProcessoServico = adicionaDocumentoAoProcessoServico;
            this.propostaAberturaConta = propostaAberturaConta;
            this.processoRepositorio = processoRepositorio;
            this.indexacaoDocumento = indexacaoDocumento;
            this.indexacaoFabrica = indexacaoFabrica;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.consultaVertrosStorage = consultaVertrosStorage;
        }

        public void Executar(int loteId, IList<ImagemConta> imagensConta, CapturaFinalizada capturaFinalizada)
        {
            if (imagensConta.NaoTemConteudo())
            {
                Log.Application.ErrorFormat(
                    "Não foi possivel importar lote {0}. Não foram encontradas imagens para conta",
                    loteId);
                return;
            }

            var primeiraImagem = imagensConta.First();
            var cpf = primeiraImagem.Cpf.RemoverDiacritico();
            var imagensPorTipo = imagensConta
                .GroupBy(x => x.TipoDocumentoId)
                .ToList();

            var processo = this.processoRepositorio.ObterPorLoteId(loteId);
            var ficha = this.propostaAberturaConta.CriarPacVirtual(processo, cpf);

            this.indexacaoDocumento.Executar(ficha, cpf);

            this.AdicionarIndexacaoSignoNaFicha(ficha, capturaFinalizada);
            this.AdicionarIndexacaoLatitudeLongitudeNaFicha(ficha, capturaFinalizada);
            this.AdicionarIndexacaoVertrosNaFicha(ficha, cpf);
            this.AdicionarIndexacaoContaSemFoto(ficha, imagensConta);

            foreach (var imagens in imagensPorTipo)
            {
                this.adicionaDocumentoAoProcessoServico
                    .Adicionar(loteId, imagens.ToList());
            }
        }

        private void AdicionarIndexacaoLatitudeLongitudeNaFicha(Documento ficha, CapturaFinalizada capturaFinalizada)
        {
            var indexacaoDocumento = this.indexacaoFabrica
                .Criar(ficha, Campo.ReferenciaDeArquivoLatitude, capturaFinalizada.Latitude.ToString());

            this.AdicionarIndexacao(indexacaoDocumento);

            indexacaoDocumento = this.indexacaoFabrica
                .Criar(ficha, Campo.ReferenciaDeArquivoLongitude, capturaFinalizada.Longitude.ToString());

            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void AdicionarIndexacaoContaSemFoto(Documento ficha, IList<ImagemConta> imagensConta)
        {
            var imagemFoto = imagensConta.FirstOrDefault(x => x.TipoDocumentoId == TipoDocumento.CodigoFoto);

            if (imagemFoto.NaoTemConteudo())
            {
                return;
            }

            var fotoRecusada = imagemFoto.FormatoBase64 == "NOK" ? "S" : "N";
            
            var indexacaoDocumento = this.indexacaoFabrica
                    .Criar(ficha, Campo.ReferenciaDeArquivoFotoRecusada, fotoRecusada);

            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void AdicionarIndexacaoVertrosNaFicha(Documento ficha, string cpf)
        {
            var vertros = "E";

            var vertrosStatus = this.consultaVertrosStorage.Obter(cpf);
            if (vertrosStatus == VertrosStatus.CpfOk)
            {
                vertros = "OK";
            }
            else if (vertrosStatus == VertrosStatus.CpfNok)
            {
                vertros = "NOK";
            }

            var indexacaoDocumento = this.indexacaoFabrica
               .Criar(ficha, Campo.ReferenciaDeArquivoVertros, vertros);

            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void AdicionarIndexacaoSignoNaFicha(Documento ficha, CapturaFinalizada capturaFinalizada)
        {
            if (string.IsNullOrEmpty(capturaFinalizada.Signo))
            {
                return;
            }

            var indexacaoDocumento = this.indexacaoFabrica
                .Criar(ficha, Campo.ReferenciaDeArquivoSignoInformado, capturaFinalizada.Signo);

            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void AdicionarIndexacao(Indexacao indexacaoDocumento)
        {
            if (indexacaoDocumento.NaoTemConteudo() == false)
            {
                this.indexacaoRepositorio.Salvar(indexacaoDocumento);
            }
        }
    }
}