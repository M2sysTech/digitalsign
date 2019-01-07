namespace Veros.Paperless.Model.Repositorios
{
    using Veros.Framework.Modelo;
    using Veros.Paperless.Model.Entidades;

    public interface IListaNegraCpfRepositorio : IRepositorio<ListaNegraCpf>
    {
        bool CpfEstaNaListaNegra(string cpf);
    }
}