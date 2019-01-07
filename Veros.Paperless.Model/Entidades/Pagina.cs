namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class Pagina : Entidade
    {
        public Pagina()
        {
            this.ValoresReconhecidos = new List<ValorReconhecido>();
            this.PalavrasTipos = new List<PalavraTipo>();
        }

        public virtual string TipoArquivo
        {
            get;
            set;
        }

        public virtual string TipoArquivoOriginal
        {
            get;
            set;
        }

        public virtual DateTime DataCriacao
        {
            get;
            set;
        }

        public virtual PaginaStatus Status
        {
            get;
            set;
        }

        public virtual PaginaTipoOcr AnaliseConteudoOcr
        {
            get;
            set;
        }

        public virtual string AgenciaRemetente
        {
            get;
            set;
        }

        public virtual string ImagemFront
        {
            get;
            set;
        }

        public virtual int TamanhoImagemFrente
        {
            get;
            set;
        }

        public virtual int DocCodeReferencia
        {
            get;
            set;
        }
        
        public virtual int Ordem
        {
            get;
            set;
        }

        public virtual DateTime? FimOcr
        {
            get;
            set;
        }

        public virtual string Info
        {
            get;
            set;
        }

        public virtual string ImagemFrenteEmBranco
        {
            get;
            set;
        }

        public virtual string ImagemVersoEmBranco
        {
            get;
            set;
        }

        public virtual Lote Lote
        {
            get;
            set;
        }

        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual List<ValorReconhecido> ValoresReconhecidos
        {
            get;
            set;
        }

        public virtual string NomeArquivoSemExtensao
        {
            get;
            set;
        }

        public virtual string NomeDoArquivo
        {
            get
            {
                return this.NomeArquivoSemExtensao + "." + this.TipoArquivo;
            }
        }

        public virtual string CaminhoCompletoDoArquivo
        {
            get;
            set;
        }

        public virtual float WidthPixels
        {
            get;
            set;
        }

        public virtual float HeightPixels
        {
            get;
            set;
        }

        public virtual List<PalavraTipo> PalavrasTipos
        {
            get;
            set;
        }

        public virtual string CaminhoCompletoDoArquivoOriginal
        {
            get
            {
                return Path.Combine(
                    Path.GetDirectoryName(this.CaminhoCompletoDoArquivo),
                    Path.GetFileNameWithoutExtension(this.CaminhoCompletoDoArquivo) + "." + this.TipoArquivoOriginal);
            }
        }

        public virtual string PaginaImpostoRenda
        {
            get; 
            set;
        }

        public virtual string StatusFace { get; set; }

        public virtual DateTime TimeFace { get; set; }

        public virtual string Versao { get; set; }

        public virtual int PaginaPaiId { get; set; }

        public virtual int OrientacaoTesseract { get; set; }

        /// <summary>
        /// Esse campo não persiste no banco. utilizado apenas para algoritmo de separação de paginas em branco
        /// ReavaliarBrancosTarefa.cs
        /// </summary>
        public virtual bool Separadora
        {
            get;
            set;
        }

        public virtual bool EmBranco
        {
            get;
            set;
        }

        public virtual int DataCenter
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumentoDefinidoPorOcr
        {
            get;
            set;
        }

        public virtual string PalavrasReconhecidasOcr
        {
            get;
            set;
        }

        public virtual string Recapturado
        {
            get;
            set;
        }

        public virtual bool ContrapartidaDeSeparadora
        {
            get;
            set;
        }

        public virtual bool CloudOk
        {
            get;
            set;
        }

        public virtual bool RemovidoFileTransferM2
        {
            get;
            set;
        }

        public virtual int DataCenterAntesCloud
        {
            get;
            set;
        }

        public virtual GroupType GetGroupType()
        {
            if (string.IsNullOrEmpty(this.NomeArquivoSemExtensao))
            {
                return GroupType.NoMatch;
            }

            var fileName = this.NomeArquivoSemExtensao;

            if (this.NomeArquivoSemExtensao.ToLower().Contains("_page"))
            {
                fileName = this.NomeArquivoSemExtensao.ToLower().Replace("_page", string.Empty);
                fileName = fileName.Remove(fileName.Length - 1);
                fileName = fileName.Trim();
            }

            var fileId = fileName.Substring(fileName.Length - 3);

            switch (fileId)
            {
                case "163":
                    return GroupType.Pac;
                case "339":
                    return GroupType.Pac;
                case "338":
                    return GroupType.Pac;
                case "337":
                    return GroupType.Pac;
                case "162":
                    return GroupType.Pac;
                case "164":
                    return GroupType.Pac;
                case "262": ////TODO: Ficha Autografo
                    return GroupType.Pac;
                case "269": ////TODO: Formulário Genérico
                    return GroupType.Pac;
                case "270":
                    return GroupType.Cpf;
                case "271":
                    return GroupType.Identificacao;
                case "272":
                    return GroupType.Residencia;
                case "273": ////TODO: Comprovante de Renda
                    return GroupType.Pac;
                default:
                    return GroupType.NoMatch;
            }
        }

        public virtual Pagina CloneWithoutId()
        {
            return new Pagina
            {
                Id = 0,
                AgenciaRemetente = this.AgenciaRemetente,
                ImagemVersoEmBranco = this.ImagemVersoEmBranco,
                ImagemFrenteEmBranco = this.ImagemFrenteEmBranco,
                DataCriacao = this.DataCriacao,
                NomeArquivoSemExtensao = this.NomeArquivoSemExtensao,
                TamanhoImagemFrente = this.TamanhoImagemFrente,
                ImagemFront = this.ImagemFront,
                Info = this.Info,
                Ordem = this.Ordem,
                Status = this.Status,
                TipoArquivo = this.TipoArquivo,
                Lote = this.Lote,
                Documento = this.Documento
            };
        }

        public virtual bool EhDePac()
        {
            return this.Documento.TipoDocumento.TipoDocumentoEhMestre;
        }

        public virtual bool PossuiThumbnail()
        {
            return this.ImagemFrenteEmBranco == "S";
        }
    }
}
