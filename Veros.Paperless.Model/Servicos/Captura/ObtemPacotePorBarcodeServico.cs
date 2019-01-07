namespace Veros.Paperless.Model.Servicos.Captura
{
    using Framework.Modelo;
    using Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.ViewModel;
    using Veros.Paperless.Model.Repositorios;
    using System;

    public class ObtemPacotePorBarcodeServico : IObtemPacotePorBarcodeServico
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly ISessaoDoUsuario sessaoDoUsuario;

        public ObtemPacotePorBarcodeServico(
            IPacoteRepositorio pacoteRepositorio, 
            IColetaRepositorio coletaRepositorio, 
            ISessaoDoUsuario sessaoDoUsuario)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.coletaRepositorio = coletaRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
        }

        public Pacote Executar(string barcodeCaixa)
        {
            var pacote = this.pacoteRepositorio.ObterPorIdentificacaoCaixa(barcodeCaixa);

            if (pacote == null)
            {
                var etiqueta = EtiquetaViewModel.Carregar(barcodeCaixa);
                var coleta = this.coletaRepositorio.ObterPorId(etiqueta.ColetaId);

                if (coleta == null)
                {
                    throw new RegraDeNegocioException("Código de Coleta não encontrado!");
                }

                pacote = new Pacote
                {
                    Agencia = 1231,
                    Batido = "N",
                    Bdu = "0000000",
                    DataMovimento = DateTime.Now,
                    FromHost = "N",
                    HoraInicio = DateTime.Now,
                    Estacao = "1231",
                    Status = Pacote.Aberto,
                    Identificacao = barcodeCaixa,
                    Coleta = coleta
                };
            }

            if (pacote.DataFimPreparo.HasValue == false)
            {
                pacote.UsuarioPreparo = (Usuario)this.sessaoDoUsuario.UsuarioAtual;
                pacote.DataFimPreparo = DateTime.Now;
            }

            return pacote;
        }
    }
}
