namespace Veros.Paperless.Model.Servicos
{
    public interface IGravaLogGenericoServico
    {
        void Executar(string acao,
            int registro,
            string observacao,
            string modulo,
            string usuarioMatricula = "SYS");
    }
}