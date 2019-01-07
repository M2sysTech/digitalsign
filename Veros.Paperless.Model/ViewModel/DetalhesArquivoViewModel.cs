namespace Veros.Paperless.Model.ViewModel
{
    using System.Collections.Generic;
    using Entidades;

    public class DetalhesArquivoViewModel
    {
        public Documento Documento { get; set; }

        public Usuario UsuarioResponsavel { get; set; }

        public IList<Documento> VersoesAnteriores { get; set; }

        public static DetalhesArquivoViewModel Criar(Documento documento, Usuario user, IList<Documento> listaAnteriores)
        {
            var documentoViewModel = new DetalhesArquivoViewModel
            {
                Documento = documento,
                UsuarioResponsavel = user,
                VersoesAnteriores = listaAnteriores
            };

            return documentoViewModel;
        }
    }
}
