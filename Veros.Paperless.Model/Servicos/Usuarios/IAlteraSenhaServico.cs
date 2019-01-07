namespace Veros.Paperless.Model.Servicos.Usuarios
{
    public interface IAlteraSenhaServico
    {
        void Executar(int usuarioId, string login, string senhaAtual, string senhaNova);

        void Executar(int usuarioId, string senhaNova);
    }
}