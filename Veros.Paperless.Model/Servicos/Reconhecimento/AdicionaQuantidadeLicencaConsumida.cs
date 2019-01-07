namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Repositorios;

    public class AdicionaQuantidadeLicencaConsumida : IAdicionaQuantidadeLicencaConsumida
    {
        private readonly ILicencaConsumidaRepositorio licencaConsumidaRepositorio;

        public AdicionaQuantidadeLicencaConsumida(ILicencaConsumidaRepositorio licencaConsumidaRepositorio)
        {
            this.licencaConsumidaRepositorio = licencaConsumidaRepositorio;
        }

        public LicencaConsumida Executar(Pagina pagina, int quantidadeLicencasUtilizadas)
        {
            var licencaConsumida = new LicencaConsumida
            {
                Pagina = pagina,
                Quantidade = quantidadeLicencasUtilizadas
            };

            this.licencaConsumidaRepositorio.Salvar(licencaConsumida);

            return licencaConsumida;
        }
    }
}