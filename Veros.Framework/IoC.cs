//-----------------------------------------------------------------------
// <copyright file="IoC.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using DependencyResolver;

    public class IoC
    {
        static IoC()
        {
            Current = new StructureMapContainer();
        }

        public static IIoC Current
        {
            get;
            set;
        }
    }
}