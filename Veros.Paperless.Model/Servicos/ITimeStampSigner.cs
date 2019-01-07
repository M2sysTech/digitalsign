namespace Veros.Paperless.Model.Servicos
{
    using AssinaturaDigital;

    public interface ITimeStampSigner
    {
        string Execute(SingInfo singInfo);
    }
}