//-----------------------------------------------------------------------
// <copyright file="AttributeExtensions.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Framework
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Extensão de atributos
    /// </summary>
    public static class AttributeExtensions
    {
        public static bool HasCustomAttribute<T>(this ICustomAttributeProvider member) where T : Attribute
        {
            return member.GetCustomAttributes(typeof(T), false).Length == 1;
        }

        public static bool HasCustomAttribute(this ICustomAttributeProvider member, Type attribute)
        {
            return member.GetCustomAttributes(attribute, false).Length == 1;
        }

        public static T GetCustomAttribute<T>(this ICustomAttributeProvider member) where T : Attribute
        {
            return member.GetCustomAttributes(typeof(T), false).Cast<T>().FirstOrDefault();
        }
    }
}
