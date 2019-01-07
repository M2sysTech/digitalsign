//-----------------------------------------------------------------------
// <copyright file="ISigner.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------
namespace Veros.Framework.Sign
{
    /// <summary>
    /// Contrato para assinatura digital
    /// </summary>
    public interface ISigner
    {
        /// <summary>
        /// Efetua a assinatura digital do arquivo
        /// </summary>
        /// <param name="file">documento para a assinatura.</param>
        /// <param name="pin">senha do certificado (pin)</param>
        /// <returns>retorna arquivo assinado em bytes</returns>
        byte[] Sign(byte[] file, string pin);
    }
}
