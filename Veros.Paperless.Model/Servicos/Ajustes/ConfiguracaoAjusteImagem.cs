namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System;
    using System.IO;
    using Framework;
    using Framework.IO;

    public class ConfiguracaoAjusteImagem
    {
        private static string caminhoPadrao;

        public static string CaminhoPadrao
        {
            get
            {
                if (string.IsNullOrEmpty(caminhoPadrao))
                {
                    throw new InvalidOperationException("Caminho padrão para ajusta não configurado. Utilize metodo estático ConfiguracaoAjusteImagem.ConfigurarCaminhoPadrao(string <processoId>) para configurar o ambiente de trabalho");
                }

                return caminhoPadrao;
            }

            set
            {
                caminhoPadrao = value;
            }
        }

        public static void ConfigurarCaminhoPadrao(int processoId)
        {
            caminhoPadrao = Path.Combine(
                Aplicacao.Caminho, 
                string.Format("ImagesAjuste_{0}", processoId));

            Directories.CreateIfNotExist(caminhoPadrao);
        }
    }
}