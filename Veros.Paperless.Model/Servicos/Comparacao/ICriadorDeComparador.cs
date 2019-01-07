//-----------------------------------------------------------------------
// <copyright file="ICriadorDeComparador.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using Entidades;

    public interface ICriadorDeComparador
    {
        IComparador Cria(TipoDado tipoDado);

        IComparador Cria(TipoCampo tipoCampo);
    }
}