namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    /// <summary>
    /// TODO: rever nome
    /// </summary>
    public class CamposValidacao : Entidade
    {
        public virtual Campo CampoDocumentoMestre
        {
            get; 
            set;
        }

        public virtual Campo CampoDocumentoComprovacao
        {
            get; 
            set;
        }
    }
}