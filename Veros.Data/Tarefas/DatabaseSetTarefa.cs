namespace Veros.Data.Tarefas
{
    using System;
    using Veros.Data;
    using Veros.Framework;

    public class DatabaseSetTarefa : ITarefaM2
    {
        private readonly IDatabaseSchema databaseSchema;
        private readonly IUnitOfWork unitOfWork;

        public DatabaseSetTarefa(IDatabaseSchema databaseSchema, IUnitOfWork unitOfWork)
        {
            this.databaseSchema = databaseSchema;
            this.unitOfWork = unitOfWork;
        }

        public string TextoDeAjuda
        {
            get
            {
                return "Seta forçadamente a versão de migration no banco de dados";
            }
        }

        public string Comando
        {
            get
            {
                return "database set";
            }
        }

        public void Executar(params string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Esta faltando numero do migration a ser setado");
                Console.WriteLine("Ex: database set 201209212059");
                return;
            }

            var version = args[2];

            using (this.unitOfWork.Begin())
            {
                this.databaseSchema.SetarVersaoManualmente(Convert.ToInt64(version));
            }
        }
    }
}