using System;
using System.Collections.Generic;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniformAttribute : Attribute
    {
        public static Dictionary<string, int> GetLocations(object model, int shaderProgram)
        {
            var names = AttributeHelper.GetPropertiesWithAttribute<UniformAttribute>(model);
            return ShaderUtilities.GetLocations(names, shaderProgram, GL.GetUniformLocation);
        }

        public static void UpdateValue(object model, string uniformName, int uniformLocation)
        {
            var value = AttributeHelper.GetPropertyValue<float>(model, uniformName);
            GL.Uniform1(uniformLocation, value);
        }
    }
}

