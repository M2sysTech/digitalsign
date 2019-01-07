namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class ComplementaIndexacaoDocumentosDoProcessoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public ComplementaIndexacaoDocumentosDoProcessoServico(
            IIndexacaoRepositorio indexacaoRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Execute(Processo processo)
        {
            Log.Application.InfoFormat("Validação do processo #{0}", processo.Id);

            var outrosDocumentos = processo.OutrosDocumentos;
            int indexacaoFicha = 0;
            string signo = null;

            foreach (var documento in outrosDocumentos)
            {
                if (documento.TipoDocumento.IsDi)
                {
                    var indexacoes = this.indexacaoRepositorio.ObterPorReferenciaDeArquivo(documento.Id, Campo.ReferenciaDeArquivoDataDeNascimentoDoParticipante);
                    
                    var dataNascimento = indexacoes[0].ValorFinal;

                    if (dataNascimento != null)
                    {
                        signo = new Signo().ObterPorDataNascimento(dataNascimento);
                    }
                }
                else
                {
                    var indexacoesFicha = this.indexacaoRepositorio.ObterPorReferenciaDeArquivo(documento.Id, Campo.ReferenciaDeArquivoSignoInformado);

                    if (indexacoesFicha != null && indexacoesFicha.Count > 0)
                    {
                        if (indexacaoFicha == 0)
                        {
                            indexacaoFicha = indexacoesFicha[0].Id;
                        }
                    }
                }
            }
            
            if (signo != null)
            {
                this.indexacaoRepositorio.AlterarPrimeiroValor(indexacaoFicha, signo);    
            }
            
            this.processoRepositorio.AlterarStatus(processo.Id, ProcessoStatus.Validado);
            Log.Application.InfoFormat("Processo #{0} avaliado com sucesso", processo.Id);
        }
    }
}
