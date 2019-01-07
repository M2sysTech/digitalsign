namespace Veros.Paperless.Model
{
    using System.Collections.Generic;
    using System;
    using System.IO;
    using Entidades;

    public class Contexto
    {
        public static TimeSpan IntervaloBatimento = JobIntervalo.DeSegundos(10);
        public static TimeSpan IntervaloWorkflow = JobIntervalo.DeSegundos(15);
        public static TimeSpan IntervaloBatimentoComProvaZero = JobIntervalo.DeMinutos(1);
        public static int ContagemDossieQualidadeM2sys;
        public static IDictionary<int, int> ContagemDossieQualidadeCef;
        public static IDictionary<int, int> OverheadDossieQualidadeCef;
        public static IDictionary<int, int> LotesCef = new Dictionary<int, int>();

        public static bool UsarBatimentoAntigo
        {
            get;
            set;
        }

        public static bool GargaloDeOcr
        {
            get;
            set;
        }

        public static string EngineOcr
        {
            get;
            set;
        }

        public static int TipoPostagemOcr
        {
            get;
            set;
        }

        public static int TempoFilasWeb
        {
            get;
            set;
        }

        public static List<int> OcrTypedocsPermitidos
        {
            get;
            set;
        }

        public static ConfiguracaoDeFases ConfiguracaoDeFases
        {
            get;
            set;
        }

        public static ConfiguracaoDeLoteCef ConfiguracaoDeLoteCef
        {
            get;
            set;
        }

        public static bool UsarThumbnail
        {
            get;
            set;
        }

        public static bool CorrigirOrientacao
        {
            get;
            set;
        }

        public static string DiretorioTessdata
        {
            get;
            set;
        }

        public static List<int> DimensoesThumbnail
        {
            get;
            set;
        }

        public static string PastaArquivosDossie
        {
            get
            {
                return @"C:\Packs\GEDCEF\DOSSIE";
            }
        }

        public static string PastaInboxRecognition
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\INBOX";    
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "INBOX");    
            }
        }

        public static string PastaInboxRecognitionTemp
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolderTemp))
                {
                    return @"C:\Packs\GEDCEFTEMP\INBOX";
                }

                return Path.Combine(Contexto.DirectoryWorkFolderTemp, "INBOX");
            }
        }

        public static string PastaInboxPosAjusteRecognition
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\INBOX-POS-AJUSTE";
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "INBOX-POS-AJUSTE");
            }
        }

        public static string PastaInboxPosAjusteRecognitionTemp
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolderTemp))
                {
                    return @"C:\Packs\GEDCEFTEMP\INBOX-POS-AJUSTE";
                }

                return Path.Combine(Contexto.DirectoryWorkFolderTemp, "INBOX-POS-AJUSTE");
            }
        }

        public static string PastaAjusteTratamento
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\AJUSTE";
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "AJUSTE");
            }
        }

        public static string PastaOutboxRecognition
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\OUTBOX";
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "OUTBOX");    
            }
        }

        public static string PastaOutboxPdfRecognition
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\OUTBOXPDF";
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "OUTBOXPDF");
            }
        }

        public static string PastaOutboxPdfPosAjusteRecognition
        {
            get
            {
                if (string.IsNullOrEmpty(Contexto.DirectoryWorkFolder))
                {
                    return @"C:\Packs\GEDCEF\OUTBOXPDF-POS-AJUSTE";
                }

                return Path.Combine(Contexto.DirectoryWorkFolder, "OUTBOXPDF-POS-AJUSTE");
            }
        }

        public static TimeSpan TimeoutWorkflow
        {
            get
            {
#if DEBUG
                return TimeSpan.FromMinutes(1);
#else
                return TimeSpan.FromMinutes(10);
#endif
            }
        }

        public static string AssinaturaDigitalAuthor
        {
            get;
            set;
        }

        public static string AssinaturaDigitalCreator
        {
            get;
            set;
        }

        public static string AssinaturaDigitalKeywords
        {
            get;
            set;
        }

        public static string AssinaturaDigitalProducer
        {
            get;
            set;
        }

        public static string AssinaturaDigitalContact
        {
            get;
            set;
        }

        public static string AssinaturaDigitalLocation
        {
            get;
            set;
        }

        public static string AssinaturaDigitalReason
        {
            get;
            set;
        }

        public static string AssinaturaDigitalSubject
        {
            get;
            set;
        }

        public static string AssinaturaDigitalTitle
        {
            get;
            set;
        }

        public static int QualidadePorcentagemm2Sys
        {
            get;
            set;
        }

        public static int QualidadePorcentagemCef
        {
            get;
            set;
        }

        public static bool AssinaturaDigitalAtivada
        {
            get;
            set;
        }

        public static string DirectoryWorkFolder
        {
            get;
            set;
        }

        public static string DirectoryWorkFolderTemp
        {
            get;
            set;
        }

        public static string DirectoryNetworkUser
        {
            get;
            set;
        }

        public static int MinWidthPixel
        {
            get;
            set;
        }

        public static int MinHeightPixel
        {
            get;
            set;
        }

        public static int MinMargemPixel
        {
            get;
            set;
        }

        public static bool PodeExecutarDirectoryInbox
        {
            get;
            set;
        }

        public static bool PodeExecutarDirectoryInboxPosAjuste
        {
            get;
            set;
        }

        public static bool PodeExecutarDirectoryOutbox
        {
            get;
            set;
        }

        public static bool PodeExecutarDirectoryOutboxPosAjuste
        {
            get;
            set;
        }

        public static string CaminhoFiletransferLinux
        {
            get;
            set;
        }

        public static string VersaoSeparadorMinima
        {
            get;
            set;
        }

        public static bool IdentificarTipoPorOcr
        {
            get;
            set;
        }

        public static bool ManterPalavrasReconhecidas
        {
            get;
            set;
        }

        public static int PercentRegiaoTopoOcr
        {
            get;
            set;
        }

        public static bool SeparadorPorBarcode
        {
            get;
            set;
        }

        public static bool SeparadorPorCarimbo
        {
            get;
            set;
        }

        public static int PixelsCropCarimbo
        {
            get;
            set;
        }

        public static long QualidadeThumbnail
        {
            get;
            set;
        }

        public static int TempoParaAssinarDocumentosComErro
        {
            get;
            set;
        }
    }
}
