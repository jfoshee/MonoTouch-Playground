using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelAttribute : Attribute
    {
        public BeginMode BeginMode { get; set; }
        public string VertexPropertyName { get; set; }

        public ModelAttribute (BeginMode beginMode, string vertexPropertyName)
        {
            BeginMode = beginMode;
            VertexPropertyName = vertexPropertyName;
        }

        public static void Draw(object model)
        {
            var modelAttribute = AttributeHelper.GetAttribute<ModelAttribute>(model);
            if (modelAttribute == null)
                throw new InvalidOperationException("No ModelAttribute on given object");
            var vertices = AttributeHelper.GetPropertyValue<Array>(model, modelAttribute.VertexPropertyName);
            GL.DrawArrays(modelAttribute.BeginMode, 0, vertices.GetLength(0));
        }
    }
}

