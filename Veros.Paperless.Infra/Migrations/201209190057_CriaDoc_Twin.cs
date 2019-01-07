namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190057)]
    public class CriaDocTwin : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "DOC_TWIN",
                this.WithId("DOC_CODE"),
                new Column("BATCH_CODE", DbType.Int32),
                new Column("ENV_CODE", DbType.Int32),
                new Column("TRANS_CODE", DbType.Int32),
                new Column("TYPEDOC_ID", DbType.Int32),
                new Column("DOC_DATE", DbType.DateTime),
                new Column("DOC_USRDIGIT", DbType.Int32),
                new Column("DOC_STATUS", DbType.AnsiString, 1),
                new Column("DOC_DTSTART", DbType.DateTime),
                new Column("DOC_CAPTUR", DbType.AnsiString, 127),
                new Column("DOC_FILETYPE", DbType.AnsiString, 16),
                new Column("DOC_IMGF", DbType.AnsiString, 1),
                new Column("DOC_IMGV", DbType.AnsiString, 1),
                new Column("DOC_HASHF", DbType.AnsiString, 127),
                new Column("DOC_HASHV", DbType.AnsiString, 127),
                new Column("DOC_CRYPTO", DbType.AnsiString, 1),
                new Column("DOC_BANCO", DbType.AnsiString, 3),
                new Column("DOC_AGENCIA", DbType.AnsiString, 4),
                new Column("DOC_INFO", DbType.AnsiString, 127),
                new Column("DOC_CNPJ", DbType.AnsiString, 25),
                new Column("DOC_CODRECEITA", DbType.AnsiString, 5),
                new Column("DOC_CONCESCODE", DbType.Int32),
                new Column("DOC_CONTA", DbType.AnsiString, 25),
                new Column("DOC_DATAVENCIM", DbType.AnsiString, 10),
                new Column("DOC_NUMREFERENC", DbType.AnsiString, 127),
                new Column("DOC_OPERACAO", DbType.AnsiString, 1),
                new Column("DOC_PAGAMENTO", DbType.AnsiString, 1),
                new Column("DOC_PERCENTUAL", DbType.Decimal),
                new Column("DOC_PERIODOAPUR", DbType.AnsiString, 10),
                new Column("DOC_CEP", DbType.AnsiString, 8),
                new Column("DOC_VALOR", DbType.Decimal),
                new Column("DOC_VALORABATIM", DbType.Decimal),
                new Column("DOC_VALORDESCONT", DbType.Decimal),
                new Column("DOC_VALORJUROS", DbType.Decimal),
                new Column("DOC_VALORMULTA", DbType.Decimal),
                new Column("DOC_VALOROUTROS", DbType.Decimal),
                new Column("DOC_VALORPRINCIP", DbType.Decimal),
                new Column("DOC_HOST", DbType.AnsiString, 1),
                new Column("DOC_AUTENTIC", DbType.AnsiString, 1),
                new Column("DOC_LINHAAUT", DbType.AnsiString, 127),
                new Column("DOC_OCORRENCIA", DbType.AnsiString, 1),
                new Column("DOC_AGENCIAREMET", DbType.AnsiString, 4),
                new Column("DOC_AGENCIADEPOS", DbType.AnsiString, 4),
                new Column("DOC_CONTADEPOS", DbType.AnsiString, 25),
                new Column("DOC_NUMTOQUES", DbType.Int32),
                new Column("DOC_TEMPODIG", DbType.Int32),
                new Column("DOC_FILESIZEF", DbType.Int32),
                new Column("DOC_FILESIZEV", DbType.Int32),
                new Column("DOC_NSUHOST", DbType.AnsiString, 12),
                new Column("DOC_NSUORIGEM", DbType.AnsiString, 12),
                new Column("DOC_SIGNATURE", DbType.AnsiString, 1),
                new Column("DOC_VALORDINH", DbType.Decimal),
                new Column("DOC_VALORCHEQ", DbType.Decimal),
                new Column("DOC_FORMALISTIC0", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC1", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC2", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC3", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC4", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC5", DbType.AnsiString, 1),
                new Column("DOC_FORMALISTIC6", DbType.AnsiString, 1),
                new Column("DOC_REGRA1", DbType.AnsiString, 1),
                new Column("DOC_REGRA2", DbType.AnsiString, 1),
                new Column("DOC_REGRA3", DbType.AnsiString, 1),
                new Column("DOC_REGRA4", DbType.AnsiString, 1),
                new Column("DOC_REGRA5", DbType.AnsiString, 1),
                new Column("DOC_REGRA6", DbType.AnsiString, 1),
                new Column("DOC_REGRA7", DbType.AnsiString, 1),
                new Column("DOC_REGRA8", DbType.AnsiString, 1),
                new Column("DOC_REGRA9", DbType.AnsiString, 1),
                new Column("DOC_REGRA10", DbType.AnsiString, 1),
                new Column("DOC_REGRA11", DbType.AnsiString, 1),
                new Column("DOC_REGRA12", DbType.AnsiString, 1),
                new Column("DOC_FRAUDE", DbType.AnsiString, 1),
                new Column("DOC_FRAUDEREASON", DbType.Int32),
                new Column("MSGCODE_CODEORIG", DbType.AnsiString, 10),
                new Column("MSGCODE_CODECONS", DbType.AnsiString, 254),
                new Column("DOC_NUMTENT", DbType.Int32),
                new Column("DOC_HORATX", DbType.DateTime),
                new Column("DOC_IDTERMINAL", DbType.Int32),
                new Column("DOC_AUTENTSEQ", DbType.Int32),
                new Column("DOC_ORIGEM", DbType.Int32),
                new Column("DOC_PRAZOBLOQ", DbType.Int32),
                new Column("TDC_CODE", DbType.Int32),
                new Column("DOC_TDCSEQ", DbType.Int32),
                new Column("DOC_NSUESTORNO", DbType.AnsiString, 12),
                new Column("DOC_TRANSSEQ", DbType.Int32),
                new Column("DOC_MENSAGEM", DbType.AnsiString, 254),
                new Column("REMESSA_CODE", DbType.Int32),
                new Column("DOC_NSUHOSTATM", DbType.AnsiString, 12),
                new Column("DOC_DIVERGENCIA", DbType.AnsiString, 1),
                new Column("DOC_PVONLINE", DbType.AnsiString, 1),
                new Column("DOC_NSUDESTINO", DbType.AnsiString, 12),
                new Column("DOC_RECAPTURADO", DbType.AnsiString, 1),
                new Column("DOC_MOTIVO", DbType.AnsiString, 254),
                new Column("DOC_HOSTANT", DbType.AnsiString, 1),
                new Column("DOC_TIPOVAR", DbType.AnsiString, 1),
                new Column("DOC_PESSOA", DbType.AnsiString, 1),
                new Column("DOC_PERIODOAPUR2", DbType.AnsiString, 10),
                new Column("DOC_HORAESTORNO", DbType.DateTime),
                new Column("DOC_ORDER", DbType.Int32),
                new Column("BACKUP_CODE", DbType.Int32),
                new Column("DOC_MSG202", DbType.AnsiString, 1),
                new Column("DOC_FORCE", DbType.AnsiString, 1),
                new Column("DOC_FIMDIGIT", DbType.DateTime),
                new Column("DOC_LINHAAUT2", DbType.AnsiString, 127),
                new Column("DOC_LINHAAUT3", DbType.AnsiString, 127),
                new Column("DOC_LINHAAUT4", DbType.AnsiString, 127),
                new Column("DOC_AUTENTQTD", DbType.Decimal),
                new Column("DOC_DEPOSITPOR", DbType.AnsiString, 127),
                new Column("DOC_FINALIDADE", DbType.AnsiString, 127),
                new Column("DOC_CODEREF", DbType.Int32),
                new Column("DOC_TIPOCC", DbType.AnsiString, 1),
                new Column("TYPEDOC_IDAUX", DbType.Int32),
                new Column("DOC_SEQAUTENTAG", DbType.Int32),
                new Column("DOC_PEND", DbType.Decimal),
                new Column("DOC_ICR", DbType.Int32),
                new Column("DOC_CNPJRECO", DbType.AnsiString, 1),
                new Column("DOC_INFORECO", DbType.AnsiString, 1),
                new Column("DOC_DTVENCIMRECO", DbType.AnsiString, 1),
                new Column("DOC_VALORRECO", DbType.AnsiString, 1),
                new Column("DOC_LIVRE1", DbType.AnsiString, 127),
                new Column("DOC_HISTORICO", DbType.AnsiString, 4),
                new Column("DOC_FILETYPEV", DbType.AnsiString, 3),
                new Column("DOC_MARCAS", DbType.AnsiString, 127),
                new Column("DOC_HOSTCST", DbType.AnsiString, 1),
                new Column("DOC_PPAGDHCH", DbType.AnsiString, 1),
                new Column("DOC_PVALORDESCON", DbType.AnsiString, 1),
                new Column("DOC_PVALORJUROS", DbType.AnsiString, 1),
                new Column("DOC_PVALORMULTA", DbType.AnsiString, 1),
                new Column("DOC_PVALORPRINCI", DbType.AnsiString, 1),
                new Column("DOC_PAUTENTDIGIT", DbType.AnsiString, 1));

            this.Database.CreateSequence("SEQ_DOC_TWIN");
        }

        public override void Down()
        {
            this.Database.RemoveTable("DOC_TWIN");
            this.Database.RemoveSequence("SEQ_DOC_TWIN");
        }
    }
}