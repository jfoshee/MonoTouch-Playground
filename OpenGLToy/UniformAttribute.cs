using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniformAttribute : Attribute
    {
        public static IList<string> GetUniformNames(object o)
        {
            var uniformNames = new List<string>();
            var type = o.GetType();
            foreach (var property in type.GetProperties()) {
                var uniformAttribute = property.GetCustomAttributes(typeof(UniformAttribute), true).FirstOrDefault();
                if (uniformAttribute != null)
                    uniformNames.Add(property.Name);
            }
            return uniformNames;
        }
    }
}

