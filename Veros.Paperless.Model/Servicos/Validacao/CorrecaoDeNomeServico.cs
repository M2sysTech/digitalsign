namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Batimento;
    using Entidades;
    using Repositorios;

    public class CorrecaoDeNomeServico : ICorrecaoDeNomeServico
    {
        private readonly ObtemPalavrasDiferentesEntreNomesServico obtemPalavrasDiferentesEntreNomesServico;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly IVerificaSeNomeReconhecidoServico verificaSeNomeReconhecido;
        private readonly IInformacoesReconhecimento informacoesReconhecimento;

        public CorrecaoDeNomeServico(
            ObtemPalavrasDiferentesEntreNomesServico obtemPalavrasDiferentesEntreNomesServico, 
            IValorReconhecidoRepositorio valorReconhecidoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            IVerificaSeNomeReconhecidoServico verificaSeNomeReconhecido,
            IInformacoesReconhecimento informacoesReconhecimento)
        {
            this.obtemPalavrasDiferentesEntreNomesServico = obtemPalavrasDiferentesEntreNomesServico;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.verificaSeNomeReconhecido = verificaSeNomeReconhecido;
            this.informacoesReconhecimento = informacoesReconhecimento;
        }

        public void Execute(Processo processo)
        {
            foreach (var documento in processo.Documentos)
            {
                var nomeDeMae = documento.ObterIndexacao("NMMAECLI");

                if (nomeDeMae == null)
                {
                    continue;
                }

                var nome1 = nomeDeMae.SegundoValor;
                var nome2 = nomeDeMae.ValorFinal;

                if (string.IsNullOrEmpty(nome1) || string.IsNullOrEmpty(nome2))
                {
                    continue;
                }

                var palavrasDiferentes = this.obtemPalavrasDiferentesEntreNomesServico.Obter(nome1, nome2);

                if (palavrasDiferentes == null || palavrasDiferentes.Count != 2)
                {
                    this.GravarLog(documento.Id, string.Format("Não foi possível encontrar diferença no nome de mãe"));
                    continue;
                }

                var imagemReconhecida = this.informacoesReconhecimento.Obter(documento);
                
                var primeiroNomeReconhecido = this.verificaSeNomeReconhecido.Verificar(palavrasDiferentes[0].ToUpper().Trim(), imagemReconhecida);
                var segundoNomeReconhecido = this.verificaSeNomeReconhecido.Verificar(palavrasDiferentes[1].ToUpper().Trim(), imagemReconhecida);

                if (primeiroNomeReconhecido && segundoNomeReconhecido)
                {
                    this.GravarLog(documento.Id, 
                        string.Format("Não alterar nome da mãe - ambas possibilidades encontradas no OCR [{0}] [{1}]", palavrasDiferentes[0],  palavrasDiferentes[1]));

                    continue;
                }

                if (primeiroNomeReconhecido)
                {
                    this.GravarLog(documento.Id, string.Format("Alteraria nome da mãe para: [{0}] (2)", nome1));
                    continue;
                }

                if (segundoNomeReconhecido)
                {
                    this.GravarLog(documento.Id, string.Format("Alteraria nome da mãe para: [{0}] (F)", nome2));
                    continue;
                }

                this.GravarLog(documento.Id, 
                    string.Format("Nenhuma das palavras foi encontrada no OCR: [{0}] [{1}]", palavrasDiferentes[0], palavrasDiferentes[1]));
            }
        }
        
        private void GravarLog(int documentoId, string mensagem)
        {
            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoAlteraNome, documentoId, mensagem);
        }
    }
}
