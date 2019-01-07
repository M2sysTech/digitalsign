namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;
    using Servicos;

    public class ItemParaSeparacao
    {
        private readonly IManipulaImagemServico manipulaImagemServico;

        public ItemParaSeparacao()
        {
            this.manipulaImagemServico = IoC.Current.Resolve<IManipulaImagemServico>();
        }

        public Pagina Pagina
        {
            get;
            set;
        }

        public string ArquivoBaixado
        {
            get;
            set;
        }

        public ImagemProcessada ImagemProcessada
        {
            get;
            set;
        }

        public void CarregarBitmap()
        {
            this.ImagemProcessada = this.manipulaImagemServico.ObterImagemParaProcessamento(this.ArquivoBaixado);
        }

        public void DescarregarBitmap()
        {
            try
            {
                this.ImagemProcessada.BitmapProcessado.Dispose();
                this.ImagemProcessada.BitmapTopo.Dispose();
                this.ImagemProcessada.BitmapEsquerda.Dispose();
                this.ImagemProcessada.BitmapDireita.Dispose();
                this.ImagemProcessada.BitmapBaixo.Dispose();
            }
            catch (Exception)
            {
                //// faz nada. 
            }
        }
    }
}