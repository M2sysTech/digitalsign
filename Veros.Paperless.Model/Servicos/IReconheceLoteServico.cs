//-----------------------------------------------------------------------
// <copyright file="IReconheceLoteServico.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informa��o Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IReconheceLoteServico
    {
        void Executar(Lote lote);
    }
}