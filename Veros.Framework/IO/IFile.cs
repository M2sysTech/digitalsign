//-----------------------------------------------------------------------
// <copyright file="IFile.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Ecm.Framework.IO
{
    public interface IFile
    {
        bool Exists(string path);

        long GetFileSize(string fileName);
    }
}