namespace Veros.Data.Helpers
{
    using System.IO;
    using System.Text;
    using Veros.Data.Hibernate;
    using Veros.Framework;

    public class SqlScriptExecutor : DaoBase
    {
        public void Executar(string arquivoSql)
        {
            this.unitOfWork.Transacionar(() =>
            {
                Log.Application.InfoFormat("Executando script {0}", arquivoSql);

                using (var stream = new StreamReader(arquivoSql))
                {
                    while (stream.EndOfStream == false)
                    {
                        var sql = stream.ReadLine();

                        if (sql.ComecaCom("--", "REM ", "SET "))
                        {
                            continue;
                        }

                        sql = sql.RemoveLastCharIfIs(";");
                        Log.Application.Debug(sql);
                        this.Session.CreateSQLQuery(sql).ExecuteUpdate();
                    }
                }

                Log.Application.InfoFormat("Script {0} executado com sucesso", arquivoSql);
            });
        }
    }
}