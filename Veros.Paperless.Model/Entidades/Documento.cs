namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class Documento : Entidade
    {
        public static string SimboloEngrenagem = "GV";
        public static string MarcaDeProblema = "DP";
        public static string MarcaDeCriadoNaSeparacao = "M";
        public static string MarcaDeAlteradoNaSeparacao = "A";

        public Documento()
        {
            this.Indexacao = new List<Indexacao>();
            this.Paginas = new List<Pagina>();
        }

        public virtual Processo Processo
        {
            get;
            set;
        }

        public virtual List<Pagina> Paginas
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumentoOriginal
        {
            get;
            set;
        }

        public virtual Lote Lote
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: rever ISet
        /// </summary>
        public virtual List<Indexacao> Indexacao
        {
            get;
            set;
        }

        public virtual DocumentoStatus Status
        {
            get;
            set;
        }

        public virtual string Templates
        {
            get;
            set;
        }

        public virtual int Ordem
        {
            get;
            set;
        }

        public virtual string IndicioDeFraude
        {
            get;
            set;
        }

        public virtual string IdDocumentoComTipoDocumento
        {
            get
            {
                return string.Format(
                    "{0} ({1})",
                    this.TipoDocumento.Description,
                    this.Id);
            }
        }

        public virtual string TipoDocumentoComCpf
        {
            get
            {
                return string.Format(
                    "{0} ({1})",
                    this.TipoDocumento.Description,
                    this.ObterValorFinal(Campo.ReferenciaDeArquivoCpf));
            }
        }

        public virtual bool EhPac
        {
            get
            {
                return this.TipoDocumento.IsPac;
            }
        }

        public virtual DocumentoStatus StatusDeConsulta
        {
            get; 
            set;
        }

        public virtual DateTime? HoraInicio
        {
            get;
            set;
        }

        public virtual bool Reclassificado
        {
            get;
            set;
        }

        public virtual string Cpf
        {
            get;
            set;
        }

        public virtual int SequenciaTitular
        {
            get;
            set;
        }

        public virtual bool Virtual
        {
            get; 
            set;
        }

        public virtual bool NaoIdentificado
        {
            get
            {
                return this.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado;
            }
        }

        public virtual string StatusDeFraude
        {
            get; 
            set; 
        }

        public virtual string TipoDeArquivo
        {
            get; 
            set;
        }

        public virtual string Versao
        {
            get; 
            set;
        }

        public virtual int DocumentoPaiId
        {
            get; 
            set;
        }

        public virtual int UsuarioResponsavelId
        {
            get;
            set;
        }

        public virtual int QuantidadeDePaginas
        {
            get;
            set;
        }

        public virtual string Marca
        {
            get;
            set;
        }

        public virtual bool Recontado
        {
            get;
            set;
        }

        public virtual bool RecognitionService
        {
            get;
            set;
        }

        public virtual DateTime? RecognitionEm
        {
            get;
            set;
        }

        public virtual DateTime? RecognitionInicioEm
        {
            get;
            set;
        }

        public virtual int DocumentoAvoId
        {
            get;
            set;
        }

        public virtual int ArquivoDigital
        {
            get;
            set;
        }

        public virtual DateTime? RecognitionPosAjusteInicioEm
        {
            get;
            set;
        }

        public virtual DateTime? RecognitionPosAjusteTerminoEm
        {
            get;
            set;
        }

        public virtual bool RecognitionPosAjusteServiceFinalizado
        {
            get;
            set;
        }

        public static Documento Novo(
            TipoDocumento tipoDocumento,
            string cpf,
            Lote lote,
            Processo processo,
            string versao = "0")
        {
            return new Documento
            {
                Cpf = cpf,
                TipoDocumento = tipoDocumento,
                Status = DocumentoStatus.TransmissaoOk,
                Lote = lote,
                Processo = processo,
                TipoDocumentoOriginal = tipoDocumento,
                Versao = versao
            };
        }

        public virtual void AdicionaIndexacao(Indexacao indexacao)
        {
            indexacao.Documento = this;
            this.Indexacao.Add(indexacao);
        }

        public virtual void AdicionaPagina(Pagina pagina)
        {
            pagina.Documento = this;
            this.Paginas.Add(pagina);
        }
        
        public virtual bool EstaBatido()
        {
            var indicesBatidos = 0;

            foreach (var indexacao in this.Indexacao)
            {
                if (string.IsNullOrWhiteSpace(indexacao.ValorFinal) == false & string.IsNullOrEmpty(indexacao.ValorFinal) == false)
                {
                    indicesBatidos++;
                    continue;
                }

                if (!indexacao.EstaBatido()) 
                {
                    continue;
                }

                indicesBatidos++;
                indexacao.ValorFinal = indexacao.SegundoValor;
                indexacao.ValorUtilizadoParaValorFinal = ValorUtilizadoParaValorFinal.SegundoValor;
            }

            return indicesBatidos.Equals(this.Indexacao.Count);
        }

        public virtual Indexacao ObterIndexacao(string referenciaArquivo)
        {
            if (this.Indexacao == null)
            {
                return null;
            }

            var indexacao = this.Indexacao.FirstOrDefault(
                x => x.Campo.ReferenciaArquivo == referenciaArquivo);

            if (indexacao == null)
            {
                indexacao = new Indexacao { ValorFinal = string.Empty };
            }

            return indexacao;
        }

        public virtual string ObterPrimeiroValor(string referenciaArquivo)
        {
            var indexacao = this.ObterIndexacao(referenciaArquivo);

            return indexacao == null ?
                string.Empty :
                string.IsNullOrEmpty(indexacao.PrimeiroValor) ?
                string.Empty :
                indexacao.PrimeiroValor;
        }

        public virtual string ObterSegundoValor(string referenciaArquivo)
        {
            var indexacao = this.ObterIndexacao(referenciaArquivo);

            return indexacao == null ?
                string.Empty :
                string.IsNullOrEmpty(indexacao.SegundoValor) ?
                string.Empty :
                indexacao.SegundoValor;
        }

        public virtual string ObterSegundoValor(int campoId)
        {
            var indexacao = this.Indexacao.FirstOrDefault(x => x.Campo.Id == campoId);

            return indexacao == null ?
               string.Empty :
               string.IsNullOrEmpty(indexacao.SegundoValor) ?
               string.Empty :
               indexacao.SegundoValor;
        }

        public virtual string ObterValorFinal(string referenciaArquivo)
        {
            var indexacao = this.ObterIndexacao(referenciaArquivo);

            return indexacao == null ? 
                string.Empty : 
                string.IsNullOrEmpty(indexacao.ValorFinal) ?
                indexacao.SegundoValor :
                indexacao.ValorFinal;
        }

        public virtual string ObterCpf()
        {
            var indexacao = this.ObterIndexacao(Campo.ReferenciaDeArquivoCpf);

            return indexacao == null ?
                string.Empty :
                string.IsNullOrEmpty(indexacao.ValorFinal) || indexacao.ValorFinal.Trim().Length < 11 ?
                indexacao.SegundoValor :
                indexacao.ValorFinal;
        }

        public virtual void AlterarStatusDasPaginas(PaginaStatus statusTransmissaoOk)
        {
            foreach (var pagina in this.Paginas.Where(x => x.Status != PaginaStatus.StatusExcluida))
            {
                pagina.Status = statusTransmissaoOk;
            }
        }

        public virtual void SetarParaOcrNasPaginasQueAindaNaoFizeram(PaginaStatus statusEscolhido)
        {
            var listaImagensPassiveisdeOcr = new List<string>() { "GIF", "JPG", "TIF", "BMP", "PNG", "JPEG", "TIFF" };
            foreach (var pagina in this.Paginas.Where(x => x.Status != PaginaStatus.StatusExcluida && x.FimOcr == null))
            {
                if (pagina.FimOcr == null || listaImagensPassiveisdeOcr.Any(x => x == pagina.TipoArquivo.ToUpper()))
                {
                    pagina.Status = statusEscolhido;    
                }
                else
                {
                    pagina.Status = PaginaStatus.StatusReconhecimentoExecutado;    
                }
            }
        }

        public virtual bool PossuiPendenciaDeDigitacao()
        {
            return this.Indexacao.Any(x => x.EstaDigitado() == false);
        }

        public virtual bool ExisteDivergenciaEmAlgumaIndexacao()
        {
            return this.Indexacao
                .Where(x => string.IsNullOrEmpty(x.ValorFinal))
                .Any(x =>
                    x.Campo.Digitavel == false ||
                    string.IsNullOrEmpty(x.PrimeiroValor) ||
                    x.PrimeiroValorTratado() == "?" ||
                    x.PrimeiroValorTratado() == "#" ||
                    x.PrimeiroValorTratado().ToUpper() != x.SegundoValorTratado().ToUpper());
        }

        public virtual bool EstaFaltandoAlgo()
        {
            return this.Paginas.Any() == false;
        }

        public virtual bool PossuiIndexacaoComValorFinalAlterado()
        {
            return this.Indexacao.Any(x => x.ValorFinalFoiAlterado());
        }

        public virtual void AdicionarIndexadoresFaltantes(IEnumerable<Campo> todosOsCampos)
        {
            if (todosOsCampos == null)
            {
                return;
            }

            foreach (var campo in todosOsCampos.Where(campo => (campo.Obrigatorio || campo.DuplaDigitacao) && this.Indexacao.Any(x => x.Campo == campo) == false))
            {
                this.AdicionaIndexacao(new Indexacao { Campo = campo });
            }
        }

        public virtual void AtualizarValorFinalDeIndexacao(string referenciaDeArquivo, string valor)
        {
            var indexacao = this.ObterIndexacao(referenciaDeArquivo);

            if (indexacao != null)
            {
                indexacao.ValorFinal = valor;
            }
        }

        public virtual bool TemPaginas()
        {
            return this.Paginas.Count > 0;
        }

        public virtual IEnumerable<Pagina> ObterPaginasOrdenadas()
        {
            var paginas = this.Paginas
                .OrderBy(x => x.Ordem)
                .ThenByDescending(x => x.Id).ToList();

            return paginas;
        }
    }
}