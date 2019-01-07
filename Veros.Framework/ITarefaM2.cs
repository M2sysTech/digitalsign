namespace Veros.Framework
{
    public interface ITarefaM2
    {
        string TextoDeAjuda
        {
            get;
        }

        string Comando
        {
            get;
        }

        void Executar(params string[] args);
    }
}
