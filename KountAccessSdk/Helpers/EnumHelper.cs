//-----------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Helpers
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <typeparam name="Expected">The type of field you want to retrieve</typeparam>
        /// <param name="enumeration">The enum value</param>
        /// <returns>The attribute field value of type Expected</returns>
        /// <example>string val = myEnumVariable.GetAttributeValue<EnumMemberAttribute, string>(x => x.Value);</example>
        public static Expected GetAttributeValue<T, Expected>(this Enum enumeration, Func<T, Expected> expression)
            where T : Attribute
        {
            T attribute =
              enumeration
                .GetType()
                .GetMember(enumeration.ToString())
                .Where(member => member.MemberType == MemberTypes.Field)
                .FirstOrDefault()
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();

            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }
    }
}
