namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;

    public interface IValidaDatasDoComprovanteDeRenda
    {
        bool Validar(Processo processo);
    }
}
