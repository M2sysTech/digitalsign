namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    public class ColetaStatus : EnumerationString<ColetaStatus>
    {
        public static ColetaStatus Agendado = new ColetaStatus("10", "Aguardando Arquivo");
        public static ColetaStatus ImportacaoDeArquivo = new ColetaStatus("11", "Em Importação");
        public static ColetaStatus Coleta = new ColetaStatus("12", "Realizar Coleta");
        public static ColetaStatus EtiquetasGeradas = new ColetaStatus("15", "Etiquetas Geradas");
        public static ColetaStatus EmCaptura = new ColetaStatus("20", "Em Captura");
        public static ColetaStatus Recebido = new ColetaStatus("30", "Recebido");
        public static ColetaStatus Devolvido = new ColetaStatus("40", "Devolvido");
        public static ColetaStatus ErroNaImportacao = new ColetaStatus("99", "Erro na Importação");
        
        public static DocumentoStatus Excluido = new DocumentoStatus("*", "Excluido");

        public ColetaStatus(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
