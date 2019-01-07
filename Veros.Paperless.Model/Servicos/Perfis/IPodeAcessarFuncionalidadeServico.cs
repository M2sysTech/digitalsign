namespace Veros.Paperless.Model.Servicos.Perfis
{
    using Veros.Paperless.Model.Entidades;
    using System.Collections.Generic;

    public interface IPodeAcessarFuncionalidadeServico
    {
        bool Executar(Funcionalidade funcionalidade);

        bool Executar(IList<Funcionalidade> funcionalidades);

        bool ChecarSiglaPerfil(IList<string> listaSiglaPerfil);
    }
}