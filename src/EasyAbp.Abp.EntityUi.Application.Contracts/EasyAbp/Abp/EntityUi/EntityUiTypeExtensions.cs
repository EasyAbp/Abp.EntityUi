using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyAbp.Abp.EntityUi
{
    public static class EntityUiTypeExtensions
    {
        private static IEnumerable<MemberInfo> GetAllMembers(this Type type) =>
            type.GetTypeInheritance().Concat(type.GetInterfaces()).SelectMany(i => i.GetDeclaredMembers());

        public static MemberInfo GetInheritedMember(this Type type, string name) => type.GetAllMembers().FirstOrDefault(mi => mi.Name == name);

        public static MethodInfo GetInheritedMethod(this Type type, string name)
            => type.GetInheritedMember(name) as MethodInfo ?? throw new ArgumentOutOfRangeException(nameof(name), $"Cannot find method {name} of type {type}.");

        public static IEnumerable<MemberInfo> GetDeclaredMembers(this Type type) => type.GetTypeInfo().DeclaredMembers;

        public static IEnumerable<Type> GetTypeInheritance(this Type type)
        {
            yield return type;

            var baseType = type.BaseType;
            while(baseType != null)
            {
                yield return baseType;
                baseType = baseType.BaseType;
            }
        }
    }
}