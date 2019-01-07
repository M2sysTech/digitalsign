namespace Veros.Paperless.Model.Servicos.Login
{
    public interface IRegistraLoginServico
    {
        void Executar(int usuarioId, string maquina);
    }
}
