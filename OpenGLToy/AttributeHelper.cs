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
                var uniformAttribute = property.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();
                if (uniformAttribute != null)
                    uniformNames.Add(property.Name);
            }
            return uniformNames;
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
    }
}

