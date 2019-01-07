namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using Framework.Modelo;

    [Serializable]
    public class TipoDocumento : Entidade
    {
        public const int CodigoRg = 7;
        public const int CodigoCnh = 8;
        public const int CodigoCie = 9;
        public const int CodigoPassaporte = 23;
        public const int CodigoCim = 29;
        public const int CodigoCtps = 31;
        public const int CodigoAtestadoDeMatricula = 61;
        public const int CodigoBoletoComAutenticacao = 24;
        public const int CodigoBoletoSemAutenticacao = 25;
        public const int CodigoComprovanteDeResidencia = 53;
        public const int CodigoNaoIdentificado = 990;
        public const int CodigoAguardandoNovoTipo = 991;
        public const int CodigoFichaDeCadastro = 35;
        public const int CodigoRendaHolerite = 30;
        public const int CodigoRendaContraChequeInss = 39;
        public const int CodigoRendaImpostoRendaPf = 40;
        public const int CodigoRendaExtratoBancario = 41;
        public const int CodigoResidenciaBoletoCondominio = 42;
        public const int CodigoResidenciaDeclaracaoResidencia = 43;
        public const int CodigoResidenciaIptu = 45;
        
        public const int CodigoDocumentoGeral = 27;
        public const int CodigoPaginaDaPac = 36;
        public const int CodigoFotoFrontal = 37;
        public const int CodigoFotoLateral = 46;
        public const int CodigoFotoLinkedin = 49;
        public const int CodigoFotoFacebook = 48;
        public const int CodigoVideo = 38;
        public const int CodigoAssinatura = 36;
        public const int CodigoDocumentoIdentificacao = 100;
        public const int CodigoRendaInformeRendimentos = 47;
        public const int CodigoOutroDocumentoIdentificacao = 52;
        public const int CodigoOutroComprovanteResidencia = 53;
        public const int CodigoOutroComprovanteRenda = 51;
        public const int CodigoCartaDoRh = 54;
        public const int CodigoContratoLocacao = 55;
        public const int CodigoResidenciaExtratoBancario = 56;
        public const int CodigoOab = 57;
        public const int CodigoCrm = 58;
        public const int CodigoCro = 59;

        //// Telefonica
        public const int CodigoFoto = 48;
        public const int CodigoDiOutro = 52;
        public const int CodigoComprovanteResidencia = 53;

        //// GEDCEF
        public const int CodigoDossie = 60;
        public const int CodigoCapaDeIdentificacao = 61;
        public const int CodigoFolhaDeRosto = 75;
        public const int CodigoDossiePdf = 62;
        public const int CodigoTermoAutuacaoDossie = 121;
        public const int CodigoEmAjuste = 13;

        public static readonly int[] ListaTipoPacs = new int[]
        {
        };

        public static readonly List<int> CodigosDi = new List<int>()
        {
            TipoDocumento.CodigoRg,
            TipoDocumento.CodigoCnh,
            TipoDocumento.CodigoCie,
            TipoDocumento.CodigoPassaporte,
            TipoDocumento.CodigoDiOutro
        };

        public static readonly List<int> CodigosBloqueadosNaClassificacao = new List<int>()
        {
            TipoDocumento.CodigoNaoIdentificado,
            TipoDocumento.CodigoAguardandoNovoTipo,
            TipoDocumento.CodigoEmAjuste,
            TipoDocumento.CodigoDocumentoGeral,
            TipoDocumento.CodigoDossie,
            TipoDocumento.CodigoDossiePdf,
            TipoDocumento.CodigoFolhaDeRosto,
            TipoDocumento.CodigoTermoAutuacaoDossie
        };

        private static readonly List<int> codigosDocumentosMestre = new List<int>()
        {
            TipoDocumento.CodigoFichaDeCadastro
        };

        ////public virtual int Codigo
        ////{
        ////    get;
        ////    set;
        ////}

        public virtual string Description
        {
            get;
            set;
        }

        public virtual string Abreviacao
        {
            get;
            set;
        }

        public virtual RankingReclassificacao Ranking
        {
            get;
            set;
        }

        public virtual bool IsPac
        {
            get;
            set;
        }

        public virtual int TypeDocCode
        {
            get;
            set;
        }

        public virtual bool TipoDocumentoEhMestre
        {
            get
            {
                return codigosDocumentosMestre.Contains(this.Id);
            }
        }

        public virtual bool PassivelDeFraude
        {
            get;
            set;
        }

        public virtual bool PassivelDeReclassificacao
        {
            get;
            set;
        }

        public virtual int QuantidadeDePaginas
        {
            get;
            set;
        }

        public virtual bool IsDi
        {
            get
            {
                return TipoDocumento.CodigosDi.IndexOf(this.Id) >= 0;
            }
        }

        public virtual bool IsFoto
        {
            get
            {
                return this.Id == TipoDocumento.CodigoFoto;
            }
        }

        public virtual bool IsComprovanteResidencia
        {
            get
            {
                return this.Id == TipoDocumento.CodigoComprovanteResidencia;
            }
        }

        public virtual DateTime? DataCriacao
        {
            get;
            set;
        }

        public virtual int CodigoOcorrencia
        {
            get;
            set;
        }

        public virtual int GrupoId
        {
            get;            
            set;
        }

        public virtual string GrupoDescricao
        {
            get;
            set;
        }

        public static TipoDocumento CriarNaoIdentificado()
        {
            return new TipoDocumento
            {
                Id = CodigoNaoIdentificado
            };
        }

        public static int ObterPorNome(string nomeDoTipoDeDocumento, int retornoNaoEncontrado = 0)
        {
            switch (nomeDoTipoDeDocumento)
            {
                case "RG":
                case "RG_FRENTEVERSO":
                case "RG_FRENTE":
                case "RG_RECORTE":
                case "RG_FRENTEVERSO_RJ":
                    return TipoDocumento.CodigoRg;
                case "CNH":
                case "CNH2":
                    return TipoDocumento.CodigoCnh;
                ////case "RNE":
                    //// TODO: return TipoDocumento.CodigoRne;
                case "PASSAPORTE":
                    return TipoDocumento.CodigoPassaporte;
                ////case "CIE":
                ////    return TipoDocumento.CodigoCie;
                case "CARTAENDERECO":
                case "GENERICBARCODE1":
                case "CONCESSIONARIAS":
                    return TipoDocumento.CodigoComprovanteDeResidencia;
                case "PORPFICHA01":
                    return TipoDocumento.CodigoFichaDeCadastro;
                default:
                    return retornoNaoEncontrado;
            }
        }

        public static List<int> TiposDocsPermitidosOcr()
        {
            return new List<int>()
                       {
                           TipoDocumento.CodigoRg,
                           TipoDocumento.CodigoCnh,
                           TipoDocumento.CodigoCie,
                           TipoDocumento.CodigoComprovanteDeResidencia,
                           TipoDocumento.CodigoRendaHolerite
                       };
        }

        public virtual bool PodeSerReconhecido()
        {
            switch (this.Id)
            {
                case TipoDocumento.CodigoVideo:
                case TipoDocumento.CodigoFichaDeCadastro:
                case TipoDocumento.CodigoFotoFacebook:
                case TipoDocumento.CodigoFotoFrontal:
                case TipoDocumento.CodigoFotoLateral:
                case TipoDocumento.CodigoFotoLinkedin:
                case TipoDocumento.CodigoAssinatura:
                    return false;
                default:
                    return true;
            }
        }        
    }
}
