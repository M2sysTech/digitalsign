namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class FaseProcessoAguardandoMontagem : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly ICampoRepositorio campoRepositorio;

        public FaseProcessoAguardandoMontagem(
            ICampoRepositorio campoRepositorio)
        {
            this.campoRepositorio = campoRepositorio;

            this.FaseEstaAtiva = x => x.MontagemAtivo;
            this.StatusDaFase = ProcessoStatus.AguardandoMontagem;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.Montado;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (processo.Lote.Status != LoteStatus.Montado)
            {
                return;
            }

            //// Serviço de montagem ficará desligado -> Não terá tarja no projeto.
            ////if (processo.Documentos.Any(x => x.Status == DocumentoStatus.AguardandoMontagem))
            ////{
            ////    return;
            ////}

            foreach (var documento in processo.Documentos)
            {
                this.AjustarMarcacaoDeDigitacaoDeIndexadores(documento);

                this.CriarIndexadoresFaltantes(documento);
            }
            
            processo.Status = ProcessoStatus.Montado;    
        }
        
        private void CriarIndexadoresFaltantes(Documento documento)
        {
            ////TODO: Verificar como fazer isso sem acessar o repositório
            var campos = this.campoRepositorio.ObterPorCodigoTipoDocumento(documento.TipoDocumento.Id);

            if (campos == null)
            {
                return;
            }

            foreach (var campo in campos.Where(campo => campo.Obrigatorio && documento.Indexacao.Any(x => x.Campo == campo) == false))
            {
                documento.Indexacao.Add(new Indexacao
                {
                    Campo = campo,
                    Documento = documento
                });
            }
        }

        private void AjustarMarcacaoDeDigitacaoDeIndexadores(Documento documento)
        {
            foreach (var indexacao in documento.Indexacao)
            {
                if (string.IsNullOrEmpty(indexacao.PrimeiroValor) == false && indexacao.UsuarioPrimeiroValor == 0)
                {
                    indexacao.UsuarioPrimeiroValor = -2;
                    indexacao.DataPrimeiraDigitacao = DateTime.Now;
                }

                if (string.IsNullOrEmpty(indexacao.PrimeiroValor))
                {
                    indexacao.UsuarioPrimeiroValor = 0;
                    indexacao.DataPrimeiraDigitacao = null;
                }

                if (string.IsNullOrEmpty(indexacao.SegundoValor) == false && indexacao.UsuarioSegundoValor == 0)
                {
                    indexacao.UsuarioSegundoValor = -2;
                    indexacao.DataSegundaDigitacao = DateTime.Now;
                }

                if (string.IsNullOrEmpty(indexacao.SegundoValor))
                {
                    indexacao.UsuarioSegundoValor = 0;
                    indexacao.DataSegundaDigitacao = null;
                }
            }
        }
    }
}