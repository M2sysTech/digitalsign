namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface ISsh
    {
        void Conectar(SshContext sshContext);

        void RemoverArquivo(string caminhoRemoto);

        void Desconectar();
    }
}