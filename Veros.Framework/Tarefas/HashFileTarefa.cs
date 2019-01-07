namespace Veros.Framework.Tarefas
{
    using System.IO;
    using Security;
    using Veros.Framework;

    public class HashFileTarefa : ITarefaM2
    {
        public string TextoDeAjuda
        {
            get
            {
                return "Gera hash de um arquivo: m2 hash file [arquivo]";
            }
        }

        public string Comando
        {
            get
            {
                return "hash file";
            }
        }

        public void Executar(params string[] args)
        {
            if (args.Length != 3)
            {
                Log.Application.InfoFormat("Informe o arquivo. Exemplo: m2 hash file C:\\file.txt");
                return;
            }

            var file = args[2];

            if (File.Exists(file) == false)
            {
                Log.Application.InfoFormat("Arquivo não existe: {0}", file);
                return;
            }

            var hash = new Hash().HashFile(file);

            Log.Application.InfoFormat("Hash do arquivo {0}:", file);
            Log.Application.Info(hash);
        }
    }
}