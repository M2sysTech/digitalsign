namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Framework;
    using Image.Reconhecimento;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Image;

    public class ReconhecePaginaComum : IReconhecePaginaComum
    {
        private readonly IPostaImagemReconhecidaServico postaImagemReconhecidaServico;
        private readonly ISalvarReconhecimentoPaginaServico salvarReconhecimentoPaginaServico;
        private readonly IAdicionaQuantidadeLicencaConsumida adicionaQuantidadeLicencaConsumida;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private IReconheceImagem reconheceImagem;

        public ReconhecePaginaComum(
            IPostaImagemReconhecidaServico postaImagemReconhecidaServico, 
            ISalvarReconhecimentoPaginaServico salvarReconhecimentoPaginaServico, 
            IAdicionaQuantidadeLicencaConsumida adicionaQuantidadeLicencaConsumida,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.postaImagemReconhecidaServico = postaImagemReconhecidaServico;
            this.salvarReconhecimentoPaginaServico = salvarReconhecimentoPaginaServico;
            this.adicionaQuantidadeLicencaConsumida = adicionaQuantidadeLicencaConsumida;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Executar(Pagina pagina, Veros.Paperless.Model.Entidades.Imagem imagem)
        {
            this.reconheceImagem = IoC.Current.Resolve<IReconheceImagem>();

            var tipoDocumento = pagina.Documento.TipoDocumento;

            if (tipoDocumento.PodeSerReconhecido() == false)
            {
                return;
            }

            var tipoReconhecivel = this.ObterTipoReconhecivel(tipoDocumento);

            var resultadoReconhecimento = this.reconheceImagem
                .Reconhecer(imagem.Caminho, tipoReconhecivel);

            ////this.postaImagemReconhecidaServico
            ////    .Executar(pagina.Id, imagem, resultadoReconhecimento.CaminhoImagemProcessada);

            this.salvarReconhecimentoPaginaServico
                .Executar(pagina, resultadoReconhecimento);

            this.adicionaQuantidadeLicencaConsumida
                .Executar(pagina, resultadoReconhecimento.LicencasUtilizadas);
        }

        private TipoDocumentoReconhecivel ObterTipoReconhecivel(TipoDocumento tipoDocumento)
        {
            switch (tipoDocumento.Id)
            {
                case TipoDocumento.CodigoRg:
                    return TipoDocumentoReconhecivel.Rg;
                case TipoDocumento.CodigoCnh:
                    return TipoDocumentoReconhecivel.CnhFoto;
                case TipoDocumento.CodigoCie:
                    return TipoDocumentoReconhecivel.Cie;
                case TipoDocumento.CodigoComprovanteDeResidencia:
                    return TipoDocumentoReconhecivel.Residencia;
                default:
                    return TipoDocumentoReconhecivel.CnhFoto;
            }
        }
    }
}