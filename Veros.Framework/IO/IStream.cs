//-----------------------------------------------------------------------
// <copyright file="IStream.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework.IO
{
    public interface IStream
    {
        long Length
        {
            get;
        }

        void ReceiveFile(string path, long size);
        
        System.IO.Stream GetStream();
        
        string GetText();
    }
}
