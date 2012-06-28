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

        public static void UpdateValue(object model, string name, int location)
        {
            var value = AttributeHelper.GetPropertyValue<float[]>(model, name);

            int size = 2; //value.Length;
            bool normalized = false;
            int stride = 0;
            VertexAttribPointerType type = VertexAttribPointerType.Float;

            GL.VertexAttribPointer<float>(location, size, type, normalized, stride, value);
            GL.EnableVertexAttribArray(location);

//            _shaderProgram.SetAttributeArray("position", 2, VertexAttribPointerType.Float, false, 0, _model.position);
//            _shaderProgram.SetAttributeArray("color", 4, VertexAttribPointerType.UnsignedByte, true, 0, _model.color);

        }           
    }
}

