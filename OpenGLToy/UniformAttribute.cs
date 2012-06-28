using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniformAttribute : Attribute
    {
        public static Dictionary<string, int> GetUniformLocations(object model, int shaderProgram)
        {
            return GetUniformLocations(GetUniformNames(model), shaderProgram);
        }

        public static void UpdateUniformValue(object model, string uniformName, int uniformLocation)
        {
            var value = GetValueForUniform(model, uniformName);
            GL.Uniform1(uniformLocation, value);
        }

        #region Behind the scenes

        static IList<string> GetUniformNames(object model)
        {
            var uniformNames = new List<string>();
            var type = model.GetType();
            foreach (var property in type.GetProperties()) {
                var uniformAttribute = property.GetCustomAttributes(typeof(UniformAttribute), true).FirstOrDefault();
                if (uniformAttribute != null)
                    uniformNames.Add(property.Name);
            }
            return uniformNames;
        }

        static PropertyInfo GetPropertyForUniform(object model, string uniformName)
        {
            return model.GetType().GetProperty(uniformName);
        }

        static float GetValueForUniform(object model, string uniformName)
        {
            var property = GetPropertyForUniform(model, uniformName);
            return (float)property.GetValue(model, new object[]{});
        }

        static Dictionary<string, int> GetUniformLocations(IList<string> uniformNames, int shaderProgram)
        {
            Dictionary<string, int> uniforms = new Dictionary<string, int>(uniformNames.Count);
            foreach (var name in uniformNames)
                uniforms[name] = GL.GetUniformLocation(shaderProgram, name);
            return uniforms;
        }

        #endregion
    }
}

