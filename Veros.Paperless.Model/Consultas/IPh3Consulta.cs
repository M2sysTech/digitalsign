namespace Veros.Paperless.Model.Consultas
{
    using Ph3;

    public interface IPh3Consulta
    {
        ConsultaPf Obter(string cpf);
    }
}