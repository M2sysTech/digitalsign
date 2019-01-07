namespace Veros.Data.Providers
{
    using System.IO;
    using System.Reflection;
    using Framework;

    public static class AssemblyExtensions
    {
        public static void ExtractResource(this Assembly assembly, string resource, string path)
        {
            if (File.Exists(path))
            {
                return;
            }

            if (string.IsNullOrEmpty(Aplicacao.Caminho) == false)
            {
                path = Path.Combine(
                    Aplicacao.Caminho,
                    path);
            }

            Log.Application.DebugFormat("Extraindo {0} para {1}", resource, path);

            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream == null)
                {
                    return;
                }

                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                using (var fstream = new FileStream(path, FileMode.Create))
                {
                    fstream.Write(buffer, 0, buffer.Length);
                    fstream.Close();
                }
            }            
        }
    }
}
