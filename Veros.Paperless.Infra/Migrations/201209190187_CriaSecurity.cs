namespace Veros.Paperless.Infra.Migrations
{
    using System.Data;
    using Veros.Data;
    using Migrator.Framework;

    [Migration(201209190187)]
    public class CriaSecurity : Migration
    {
        public override void Up()
        {
            this.Database.AddTable(
                "SECURITY",
                this.WithId("SECURITY_CODE"),
                new Column("SECURITY_CRYPTO", DbType.AnsiString, 1),
                new Column("SECURITY_CRALGOR", DbType.Int32),
                new Column("SECURITY_CRMODO", DbType.Int32),
                new Column("SECURITY_CRPWD", DbType.AnsiString, 127),
                new Column("SECURITY_CRSIGN", DbType.Int32),
                new Column("SECURITY_HASH", DbType.AnsiString, 1),
                new Column("SECURITY_HASHALG", DbType.Int32),
                new Column("SECURITY_PWDMINSZ", DbType.Int32),
                new Column("SECURITY_PWDMAXSZ", DbType.Int32),
                new Column("SECURITY_PWDMAXCHG", DbType.Int32),
                new Column("SECURITY_PWDSPEC", DbType.AnsiString, 1),
                new Column("SECURITY_PWDOBV", DbType.AnsiString, 1),
                new Column("SECURITY_PWDBLK", DbType.AnsiString, 1),
                new Column("SECURITY_PWDBLKT", DbType.Int32),
                new Column("SECURITY_PWDREPEAT", DbType.AnsiString, 1),
                new Column("SECURITY_PWDREPEATN", DbType.Int32),
                new Column("SECURITY_PWDERR", DbType.AnsiString, 1),
                new Column("SECURITY_PWDERRN", DbType.Int32),
                new Column("SECURITY_ASSINATATIVAR", DbType.AnsiString, 1, ColumnProperty.NotNull),
                new Column("SECURITY_ASSINATALGORITMO", DbType.Int32),
                new Column("SECURITY_ASSINATCHAVEPRIVADA1", DbType.AnsiString, 4000),
                new Column("SECURITY_ASSINATCHAVEPRIVADA2", DbType.AnsiString, 4000));

            this.Database.CreateSequence("SEQ_SECURITY");
        }

        public override void Down()
        {
            this.Database.RemoveTable("SECURITY");
            this.Database.RemoveSequence("SEQ_SECURITY");
        }
    }
}
