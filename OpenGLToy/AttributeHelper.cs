using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace OpenGLToy
{
    public static class AttributeHelper
    {
        public static IList<string> GetPropertiesWithAttribute<TAttribute>(object o) where TAttribute : Attribute
        {
            var uniformNames = new List<string>();
            var type = o.GetType();
            foreach (var property in type.GetProperties()) {
                var uniformAttribute = GetAttribute<TAttribute>(property);
                if (uniformAttribute != null)
                    uniformNames.Add(property.Name);
            }
            return uniformNames;
        }

        public static TAttribute GetAttribute<TAttribute>(object o) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(o.GetType());
        }

        public static TAttribute GetAttribute<TAttribute>(MemberInfo member) where TAttribute : Attribute
        {
            return (TAttribute)member.
                GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
        }

        public static TAttribute[] GetAttributes<TAttribute>(object o) where TAttribute : Attribute
        {
            return (TAttribute[])o.GetType().
                GetCustomAttributes(typeof(TAttribute), true);
        }

        static PropertyInfo GetProperty(object model, string propertyName)
        {
            return model.GetType().GetProperty(propertyName);
        }

        public static T GetPropertyValue<T>(object model, string propertyName)
        {
            var property = GetProperty(model, propertyName);
            return (T)property.GetValue(model, new object[]{});
        }

        public static Type GetPropertyType(object model, string propertyName)
        {
            var property = GetProperty(model, propertyName);
            return property.PropertyType;
        }
    }
}

