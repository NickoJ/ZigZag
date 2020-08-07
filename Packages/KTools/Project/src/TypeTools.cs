using System;
using System.Collections.Generic;

namespace Klyukay.KTools
{
    
    public static class TypeTools
    {

        private static readonly Dictionary<string, Type> TypesByFullName = new Dictionary<string, Type>();
        private static readonly Dictionary<Type, string> FullNamesByType = new Dictionary<Type, string>();

        public static Type GetType(string typeName)
        {
            if (TypesByFullName.TryGetValue(typeName, out var type)) return type;
            
            type = Type.GetType(typeName);
            if (type == null) throw new ArgumentException($"Can't find type \"{nameof(typeName)}\".");
            
            TypesByFullName[typeName] = type;
            FullNamesByType[type] = typeName;
            return type;
        }

        public static string GetFullName<T>() => GetFullName(typeof(T));
        
        public static string GetFullName(Type type)
        {
            if (type == null) throw new ArgumentNullException($"{nameof(type)} can't be null");
            if (FullNamesByType.TryGetValue(type, out var typeName)) return typeName;

            typeName = type.FullName;
            if (typeName == null) return null;
            
            TypesByFullName[typeName] = type;
            FullNamesByType[type] = typeName;
            return typeName;
        }
        
    }
    
}