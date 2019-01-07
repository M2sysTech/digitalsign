//-----------------------------------------------------------------------
// <copyright file="ISpellChecker.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.Text
{
    /// <summary>
    /// Contrato para corretores ortográfricos
    /// </summary>
    public interface ISpellChecker
    {
        /// <summary>
        /// Gets or sets texto sendo checado
        /// </summary>
        string CheckingText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets palavra errada atual do ponteiro
        /// </summary>
        string WrongWord
        {
            get;
        }

        /// <summary>
        /// Gets último caracter da palavra errada acusado pelo
        /// componente de corretor ortográfico. É útil para
        /// componentes que acusam palavras com ponto final
        /// como erradas. Depois de corrigir uma palavra
        /// que realmente estava errada, pode concatenar
        /// com esta propriedade
        /// </summary>
        string LastCharOfWrongWord
        {
            get;
        }

        /// <summary>
        /// Gets posição inicial da palavra errada
        /// </summary>
        int StartPositionOfWrongWord
        {
            get;
        }

        /// <summary>
        /// Procura pela próxima palavra errada. Se houver, preenche a propriedade
        /// WrongWord com a palavra errada.
        /// </summary>
        /// <returns>True se houver palavra errada</returns>
        bool HasError();

        /// <summary>
        /// Altera a palavra errada atual por outra palavra
        /// </summary>
        /// <param name="word">palavra para correção</param>
        void Change(string word);

        /// <summary>
        /// Retorna lista de sugestões para uma palavra errada
        /// </summary>
        /// <param name="word">palavra errada</param>
        /// <returns>lista de sugestões de palavras</returns>
        string[] GetCorrectWordList(string word);
    }
}