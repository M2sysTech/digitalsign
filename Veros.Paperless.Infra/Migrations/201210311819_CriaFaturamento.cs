namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201210311819)]
    public class CriaFaturamento : Migration
    {
        public override void Up()
        {
            this.Database.AddTable("FATURAMENTO",
            this.WithId("FATURAMENTO_CODE"),
            new Column("DT_FATURAMENTO", DbType.DateTime),
            new Column("QTDCONTAS_RECEPCIONADAS", DbType.Int64),
            new Column("QTD_SLAOK", DbType.Int64),
            new Column("PERCENTUAL_SLAOK", DbType.Double),
            new Column("QTDSLA_NOK", DbType.Int64),
            new Column("PERCENTUAL_NOK", DbType.Double),
            new Column("QTDNAO_TRATADAS", DbType.Int64),
            new Column("PERCENTUAL_NAOTRATADAS", DbType.Double),
            new Column("QTDRECONHECIMENTO_AUTO", DbType.Int64),
            new Column("PERCENTUAL_RECONHECIMENTO", DbType.Int64),
            new Column("QTDANALISE_MANUAL", DbType.Int64),
            new Column("PERCENTUAL_ANALISEMANUAL", DbType.Double),
            new Column("QTDALTERACAO_CADASTRAL", DbType.Int64),
            new Column("PERCENTUAL_ALTERACAOCADASTRAL", DbType.Double),
            new Column("QTDATUACAO_BANCO", DbType.Int64),
            new Column("PERCENTUAL_ATUACAOBANCO", DbType.Double),
            new Column("QTDLIBERADAS", DbType.Int64),
            new Column("PERCENTUAL_LIBERADAS", DbType.Double),
            new Column("QTDDEVOLVIDAS", DbType.Int64),
            new Column("TIPO_FATURAMENTO", DbType.Int64),
            new Column("NOME_ARQUIVO", DbType.AnsiString, 400),
            new Column("STATUS_ARQUIVO", DbType.Int64),
            new Column("DT_RECEPCAOARQUIVO", DbType.DateTime),
            new Column("DT_PROCESSSAMENTO", DbType.DateTime),
            new Column("PERCENTUAL_DEVOLVIDAS", DbType.Double),
            new Column("QTDAPONTAMENTO_FRAUDE", DbType.Int64),
            new Column("PERCENTUAL_FRAUDE", DbType.Double));

             this.Database.CreateSequence("SEQ_FATURAMENTO");
         }

         public override void Down()
         {
             this.Database.RemoveTable("FATURAMENTO");
             this.Database.RemoveSequence("SEQ_FATURAMENTO");
         }
    }
}
