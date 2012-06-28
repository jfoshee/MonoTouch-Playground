using System;
using System.Linq;
using System.Collections.Generic;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class VertexAttributeAttribute : Attribute
    {
        public static Dictionary<string, int> GetLocations(object model, int shaderProgram)
        {
            var names = AttributeHelper.GetPropertiesWithAttribute<VertexAttributeAttribute>(model);
            return ShaderUtilities.GetLocations(names, shaderProgram, GL.GetAttribLocation);
        }

        public static void UpdateValue<T>(object model, string name, int location) where T : struct
        {
            var type = typeof(T);
            var value = AttributeHelper.GetPropertyValue<T[,]>(model, name);

            int size = value.GetLength(1);
            bool normalized = type != typeof(float);
            int stride = 0;
            VertexAttribPointerType glType = type == typeof(float) ?
                VertexAttribPointerType.Float :
                VertexAttribPointerType.UnsignedByte;

            GL.VertexAttribPointer<T>(location, size, glType, normalized, stride, value);
            GL.EnableVertexAttribArray(location);
        }           
    }
}

