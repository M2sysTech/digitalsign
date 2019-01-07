//-----------------------------------------------------------------------
// <copyright file="IComparador.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Paperless.Model.Servicos.Comparacao
{
    public interface IComparador
    {
        bool SaoIguais(string primeiroValor, string segundoValor);

        bool EhMenor(string primeiroValor, string segundoValor);

        bool EhMaior(string primeiroValor, string segundoValor);

        bool EhMenorOuIgual(string primeiroValor, string segundoValor);

        bool EhMaiorOuIgual(string primeiroValor, string segundoValor);

        bool Contem(string primeiroValor, string segundoValor);
    }
}
