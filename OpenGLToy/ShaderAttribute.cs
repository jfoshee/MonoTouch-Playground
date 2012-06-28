using OpenTK.Graphics.ES20;
using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShaderAttribute : Attribute
    {
        public string ShaderName { get; set; }

        public ShaderAttribute(string shaderName)
        {
            ShaderName = shaderName;
        }

        public static IList<ShaderProgram> LoadShaders(object model)
        {
            var shaderAttributes = AttributeHelper.GetAttributes<ShaderAttribute>(model);
            return shaderAttributes.Select(sa => new ShaderProgram(sa.ShaderName)).ToList();
        }
    }
}
