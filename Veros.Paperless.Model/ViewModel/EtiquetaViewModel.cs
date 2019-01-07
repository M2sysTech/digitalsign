namespace Veros.Paperless.Model.ViewModel
{
    using System;
    using Framework;

    public class EtiquetaViewModel
    {
        public int Sequencial { get; set; }

        public int ColetaId { get; set; }

        public int TipoProcessoId { get; set; }

        public static EtiquetaViewModel Carregar(string etiqueta)
        {
            if (string.IsNullOrEmpty(etiqueta) || etiqueta.Length != 14)
            {
                throw new Exception("Código de etiqueta inválido!");
            }

            var viewModel = new EtiquetaViewModel
            {
                TipoProcessoId = etiqueta.Substring(0, 1).ToInt(),
                ColetaId = etiqueta.Substring(1, 9).ToInt(),
                Sequencial = etiqueta.Substring(10).ToInt()
            };

            if (viewModel.Sequencial < 1 || 
                viewModel.ColetaId < 1 || 
                viewModel.Sequencial < 1)
            {
                throw new Exception("Código de etiqueta inválido!");
            }

            return viewModel;
        }

        public string NumeroFormatado()
        {
            return string.Format("{0}{1}{2}", this.Sequencial, this.ColetaId.ToString("00000000"), this.Sequencial.ToString("0000"));
        }
    }
}
