namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class Processo : Entidade, IProcessavelPeloWorkflow<ProcessoStatus>
    {
        public static string MarcaPassouPelaQualidadeCef = "QC";

        public Processo()
        {
            this.Documentos = new List<Documento>();
            this.Participantes = new List<Participante>();
            this.RegrasVioladas = new List<RegraViolada>();
            this.RegrasImportadas = new List<RegraImportada>();
            this.Remessas = new List<Remessa>();
        }

        ////public virtual DateTime DataCaptura { get; set; }
        
        public virtual ProcessoStatus Status
        {
            get;
            set;
        }

        public virtual string Agencia
        {
            get;
            set;
        }

        public virtual string Conta
        {
            get;
            set;
        }

        public virtual Lote Lote
        {
            get;
            set;
        }

        public virtual string TimeStamp
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: trocar int para TipoProcesso
        /// </summary>
        public virtual TipoProcesso TipoDeProcesso
        {
            get;
            set;
        }

        public virtual ProcessoDecisao Decisao
        {
            get;
            set;
        }

        public virtual string ObservacaoProcesso
        {
            get; 
            set;
        }

        public virtual int AcaoClassifier
        {
            get;
            set;
        }

        public virtual List<Documento> Documentos
        {
            get;
            set;
        }

        public virtual IEnumerable<Documento> OutrosDocumentos
        {
            get
            {
                return this.Documentos.Where(x => x.EhPac == false);
            }
        }

        public virtual Documento Pac
        {
            get
            {
                return this.Documentos.FirstOrDefault(x => x.EhPac);
            }
        }

        public virtual List<RegraViolada> RegrasVioladas
        {
            get; 
            set;
        }

        public virtual DateTime? HoraInicio
        {
            get; 
            set; 
        }

        public virtual Usuario UsuarioResponsavel
        {
            get;
            set;
        }

        public virtual List<Participante> Participantes
        {
            get;
            set;
        }

        public virtual List<RegraImportada> RegrasImportadas
        {
            get;
            set;
        }
        
        public virtual string Identificacao
        {
            get; 
            set;
        }

        public virtual int QtdePaginas
        {
            get;
            set;
        }

        public virtual bool ExistePac
        {
            get
            {
                return this.Pac != null;
            }  
        }

        public virtual List<Remessa> Remessas
        {
            get;
            set;
        }

        public virtual IEnumerable<Documento> ObterDocumentosComprovacao
        {
            get
            {
                return this.Documentos.Where(x => x.TipoDocumento.TipoDocumentoEhMestre == false);
            }
        }

        public virtual string Barcode
        {
            get;
            set;
        }

        public virtual string Marca
        {
            get;
            set;
        }

        public virtual DateTime? HoraInicioAjuste
        {
            get;
            set;
        }

        public static Processo CriarNovo(Lote lote)
        {
            return new Processo
            {
                TipoDeProcesso = new TipoProcesso { Id = TipoProcesso.Varejo },
                Lote = lote,
                Status = ProcessoStatus.AguardandoTransmissao
            };
        }

        public virtual void AdicionaDocumento(Documento documento)
        {
            documento.Processo = this;
            this.Documentos.Add(documento);
        }

        public virtual string ObterIndicioDeFraude()
        {
            var indicioDeFraude = string.Empty;
            var separador = string.Empty;

            foreach (var documento in this.Documentos.Where(x => string.IsNullOrEmpty(x.IndicioDeFraude) == false))
            {
                indicioDeFraude += separador + documento.IndicioDeFraude;
                separador = " ";
            }

            return string.IsNullOrEmpty(indicioDeFraude) ? " " : indicioDeFraude;
        }

        public virtual Documento ObterDocumentoPorTipo(TipoDocumento tipoDocumento)
        {
            return this.Documentos.FirstOrDefault(x => x.TipoDocumento == tipoDocumento);
        }

        public virtual void AdicionaRegraViolada(RegraViolada regraViolada)
        {
            regraViolada.Processo = this;
            this.RegrasVioladas.Add(regraViolada);
        }

        public virtual void AdicionaRegraImportada(RegraImportada regraImportada)
        {
            regraImportada.Processo = this;
            this.RegrasImportadas.Add(regraImportada);
        }

        public virtual IEnumerable<Documento> ObterDocumentosMestre()
        {
            return this.Documentos.Where(x => x.TipoDocumento.TipoDocumentoEhMestre);
        }

        public virtual string StatusDescricao()
        {
            return this.Status.DisplayName;
        }

        public virtual string DescricaoParaCliente()
        {
            return this.Status.DisplayName;
        }

        public virtual string DescricaoRetornoDoBanco()
        {
            if (this.Remessas != null && this.Remessas.Count > 0)
            {
                return RetornoBanco.ObterDescricao(this.Remessas.First().Extensao);
            }

            return string.Empty;
        }

        public virtual bool FoiDevolvido()
        {
            //// TODO: refatorar
            return this.RegrasVioladas.Count(x => x.Status == RegraVioladaStatus.Marcada) > 0;
        }

        public virtual bool PossuiIndicioDeFraude()
        {
            return this.Documentos.Count(x => x.IndicioDeFraude != null) > 0;
        }

        public virtual void AlterarStatusDosDocumentos(DocumentoStatus status)
        {
            foreach (var documento in this.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
            {
                documento.Status = status;
            }
        }

        public virtual bool ExistePeloMenosUmCampoQueRequerDigitacao()
        {
            return this.Documentos.Any(documento => documento.Indexacao.Any(x => x.Campo.Digitavel));
        }

        public virtual bool TemDocumentoComStatus(DocumentoStatus status)
        {
            return this.Documentos.Any(documento => documento.Status == status);
        }

        public virtual bool PossuiDocumentoDoTipo(TipoDocumento tipoDocumento)
        {
            return this.PossuiDocumentoDoTipo(tipoDocumento.Id);
        }

        public virtual bool PossuiDocumentoDoTipo(int tipoDocumentoId)
        {
            return this.Documentos.Any(x => x.TipoDocumento.Id == tipoDocumentoId && x.Status != DocumentoStatus.Excluido);
        }

        public virtual bool PossuiCampoComValorPreenchido(Campo campo)
        {
            return (from documento in this.Documentos
                    from indexacao in documento.Indexacao.Where(x => x.Campo == campo)
                    select string.IsNullOrEmpty(indexacao.PrimeiroValor) == false).FirstOrDefault();
        }

        public virtual bool EstaFaltandoAlgo()
        {
            return this.Documentos.Any() == false || this.Documentos.Any(x => x.Status != DocumentoStatus.Excluido && x.EstaFaltandoAlgo());
        }

        public virtual Indexacao ObterIndexador(int tipoDocumentoId, string referenciaDeArquivo)
        {
            var documento = this.Documentos.FirstOrDefault(x => x.TipoDocumento.Id == tipoDocumentoId);

            if (documento != null)
            {
                return documento.ObterIndexacao(referenciaDeArquivo);
            }

            return null;
        }

        public virtual IList<Indexacao> ObterIndexadores(int tipoDocumentoId, string referenciaDeArquivo)
        {
            var documentos = this.Documentos.Where(x => x.TipoDocumento.Id == tipoDocumentoId);

            if (documentos.Any() == false)
            {
                return null;
            }

            return documentos.Select(holerite => holerite.Indexacao.FirstOrDefault(x => x.Campo.ReferenciaArquivo == referenciaDeArquivo)).ToList();
        }

        public virtual Documento ObterDocumentoIdentificacao()
        {
            var documentoIdentificacao = this.Documentos.FirstOrDefault(
                x => x.TipoDocumento.Id.Equals(TipoDocumento.CodigoCnh) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRg) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoOab) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoCrm) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoCro) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoCie));

            if (documentoIdentificacao == null)
            {
                documentoIdentificacao = this.Documentos.FirstOrDefault(
                    x => x.TipoDocumento.Id.Equals(TipoDocumento.CodigoOutroDocumentoIdentificacao));
            }

            return documentoIdentificacao;
        }

        public virtual Documento ObterComprovanteDeResidencia()
        {
            var comprovanteResidencia = this.Documentos.FirstOrDefault(
                x => x.TipoDocumento.Id.Equals(TipoDocumento.CodigoComprovanteDeResidencia) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoResidenciaIptu) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoContratoLocacao) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoResidenciaExtratoBancario) ||
                x.TipoDocumento.Id.Equals(TipoDocumento.CodigoResidenciaBoletoCondominio));

            if (comprovanteResidencia == null)
            {
                comprovanteResidencia = this.Documentos.FirstOrDefault(
                    x => x.TipoDocumento.Id.Equals(TipoDocumento.CodigoOutroComprovanteResidencia));
            }

            return comprovanteResidencia;
        }

        public virtual IList<Documento> ObterComprovantesDeRenda()
        {
            var comprovantesRenda = this.Documentos
                .Where(x => x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRendaHolerite) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRendaContraChequeInss) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRendaExtratoBancario) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRendaImpostoRendaPf) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoRendaInformeRendimentos) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoCartaDoRh) ||
                            x.TipoDocumento.Id.Equals(TipoDocumento.CodigoOutroComprovanteRenda))
                 .ToList();

            return comprovantesRenda;
        }

        public virtual string TipoDeDi()
        {
            if (this.Documentos.Any(x => x.TipoDocumento.Id == TipoDocumento.CodigoRg))
            {
                return "RG";
            }

            if (this.Documentos.Any(x => x.TipoDocumento.Id == TipoDocumento.CodigoCnh))
            {
                return "CNH";
            }

            return string.Empty;
        }

        public virtual string ObterNumeroContrato()
        {
            ////52126.0000312382111-1/1
            ////var pattern = new Regex(@"(?<tipoDocumento>\d+)_[0-9]{1,100}_[0-9]{1,15}_[0-9]{1}_[0-9]{3,10}(F|V)");
            var pattern = new Regex(@"[0-9]{1,5}.(?<numeroContrato>\d+)-(?<nivel>\d+)/[0-9]{1}");
            var match = pattern.Match(this.Identificacao);
            var numeroContrato = match.Groups["numeroContrato"].Value;
            var nivel = match.Groups["nivel"].Value;

            return numeroContrato + nivel;
        }

        public virtual string ObterTipoContrato()
        {
            ////52126.0000312382111-1/1
            ////var pattern = new Regex(@"(?<tipoDocumento>\d+)_[0-9]{1,100}_[0-9]{1,15}_[0-9]{1}_[0-9]{3,10}(F|V)");
            var pattern = new Regex(@"[0-9]{1,5}.[0-9]{1,13}-[0-9]{1}/(?<tipoContrato>\d+)");
            var match = pattern.Match(this.Identificacao);
            var tipoContrato = match.Groups["tipoContrato"].Value;
            
            return tipoContrato;
        }

        public virtual IList<Documento> ObterDocumentosVirtuaisAtivos()
        {
            return this.Documentos.Where(x => x.Virtual && x.Status != DocumentoStatus.Excluido).ToList();
        }
    }
}