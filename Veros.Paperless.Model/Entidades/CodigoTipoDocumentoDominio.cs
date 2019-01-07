namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class CodigoTipoDocumentoDominio : EnumerationString<CodigoTipoDocumentoDominio>
    {
        public static CodigoTipoDocumentoDominio ComprovanteAssalariado = new CodigoTipoDocumentoDominio("CA", "Comprovante Assalariado");
        public static CodigoTipoDocumentoDominio CartaDoRh = new CodigoTipoDocumentoDominio("CRH", "Carta do RH");
        public static CodigoTipoDocumentoDominio ImpostoDeRenda = new CodigoTipoDocumentoDominio("I", "Imposto de Renda");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina1 = new CodigoTipoDocumentoDominio("I1", "Imposto de Renda Página 1");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina2 = new CodigoTipoDocumentoDominio("I2", "Imposto de Renda Página 2");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina3 = new CodigoTipoDocumentoDominio("I3", "Imposto de Renda Página 3");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina4 = new CodigoTipoDocumentoDominio("I4", "Imposto de Renda Página 4");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina5 = new CodigoTipoDocumentoDominio("I5", "Imposto de Renda Página 5");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina6 = new CodigoTipoDocumentoDominio("I6", "Imposto de Renda Página 6");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina7 = new CodigoTipoDocumentoDominio("I7", "Imposto de Renda Página 7");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina8 = new CodigoTipoDocumentoDominio("I8", "Imposto de Renda Página 8");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina9 = new CodigoTipoDocumentoDominio("I9", "Imposto de Renda Página 9");
        public static CodigoTipoDocumentoDominio ImpostoDeRendaPagina10 = new CodigoTipoDocumentoDominio("I10", "Imposto de Renda Página 10");
        public static CodigoTipoDocumentoDominio Aposentadoria = new CodigoTipoDocumentoDominio("APO", "Aposentadoria");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda = new CodigoTipoDocumentoDominio("O", "Outro Comprovante Renda");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda1 = new CodigoTipoDocumentoDominio("O1", "Outro Comprovante Renda 1");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda2 = new CodigoTipoDocumentoDominio("O2", "Outro Comprovante Renda 2");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda3 = new CodigoTipoDocumentoDominio("O3", "Outro Comprovante Renda 3");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda4 = new CodigoTipoDocumentoDominio("O4", "Outro Comprovante Renda 4");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda5 = new CodigoTipoDocumentoDominio("O5", "Outro Comprovante Renda 5");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda6 = new CodigoTipoDocumentoDominio("O6", "Outro Comprovante Renda 6");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda7 = new CodigoTipoDocumentoDominio("O7", "Outro Comprovante Renda 7");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda8 = new CodigoTipoDocumentoDominio("O8", "Outro Comprovante Renda 8");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda9 = new CodigoTipoDocumentoDominio("O9", "Outro Comprovante Renda 9");
        public static CodigoTipoDocumentoDominio OutroComprovanteRenda10 = new CodigoTipoDocumentoDominio("O10", "Outro Comprovante Renda 10");
        public static CodigoTipoDocumentoDominio Assinatura = new CodigoTipoDocumentoDominio("ASS", "Assinatura");
        public static CodigoTipoDocumentoDominio SelfieFrente = new CodigoTipoDocumentoDominio("FF", "Selfie Frente");
        public static CodigoTipoDocumentoDominio SelfieDiagonal = new CodigoTipoDocumentoDominio("FD", "Selfie Diagonal");
        public static CodigoTipoDocumentoDominio DocumentoIdentificacaoFrente = new CodigoTipoDocumentoDominio("DIF", "Documento identificaçao Frente");
        public static CodigoTipoDocumentoDominio DocumentoIdentificacaoVerso = new CodigoTipoDocumentoDominio("DIV", "Documento identificação Verso");
        public static CodigoTipoDocumentoDominio ContasConsumoConcessionarias = new CodigoTipoDocumentoDominio("CCC", "Contas Consumo Concessionarias");
        public static CodigoTipoDocumentoDominio Declaracaoresidencia = new CodigoTipoDocumentoDominio("DRS", "Declaração de Residência");
        public static CodigoTipoDocumentoDominio BoletoCondominio = new CodigoTipoDocumentoDominio("BCN", "Boleto de condomínio");
        public static CodigoTipoDocumentoDominio ContasOrgaosPublicos = new CodigoTipoDocumentoDominio("COP", "Contas Orgãos Públicos");
        public static CodigoTipoDocumentoDominio ContratoDeLocacao = new CodigoTipoDocumentoDominio("CL", "Contrato de Locação");
        public static CodigoTipoDocumentoDominio ComprovanteDeRenda = new CodigoTipoDocumentoDominio("CR", "Comprovante de Renda");
        public static CodigoTipoDocumentoDominio FotoPerfilFacebook = new CodigoTipoDocumentoDominio("FFB", "Foto Perfil Facebook");
        public static CodigoTipoDocumentoDominio FotoPerfilLinkedin = new CodigoTipoDocumentoDominio("FLI", "Foto Perfil Linkedin");
        public static CodigoTipoDocumentoDominio VideoLiveness = new CodigoTipoDocumentoDominio("LIV", "Vídeo Liveness");
        public static CodigoTipoDocumentoDominio NaoIdentificado = new CodigoTipoDocumentoDominio("NID", "Não Identificado");

        public CodigoTipoDocumentoDominio(string value, string counterName) : 
            base(value, counterName)
        {
        }
    }
}