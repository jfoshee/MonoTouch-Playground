using System;
using System.Collections.Generic;

namespace OpenGLToy
{
    public class ModelBindings : IDisposable
    {
        object _model;
        IList<ShaderProgram> _shaderPrograms; 
        Dictionary<string, int> _uniformLocations;
        Dictionary<string, int> _attributeLocations;

        public ModelBindings(object model)
        {
            _model = model;
            _shaderPrograms = ShaderAttribute.LoadShaders(model);
            _uniformLocations = UniformAttribute.GetLocations(model, _shaderPrograms[0].Program);
            _attributeLocations = VertexAttributeAttribute.GetLocations(model, _shaderPrograms[0].Program);
        }

        public void Draw()
        {
            UseShaders();
            UpdateUniformValues();
            UpdateAttributeValues();
            ModelAttribute.Draw(_model);
        }

        public void UseShaders()
        {
            foreach (var shader in _shaderPrograms)
            {
                shader.Use();
                #if DEBUG
                shader.Validate();
                #endif
            }
        }

        public void UpdateUniformValues()
        {
            foreach (var uniform in _uniformLocations)
                UniformAttribute.UpdateValue(_model, uniform.Key, uniform.Value);
        }

        public void UpdateAttributeValues()
        {
            foreach (var _attribute in _attributeLocations) {
                var type = AttributeHelper.GetPropertyType(_model, _attribute.Key);
                if (type == typeof(float[,]))
                    VertexAttributeAttribute.UpdateValue<float>(_model, _attribute.Key, _attribute.Value);
                else if (type == typeof(byte[,]))
                    VertexAttributeAttribute.UpdateValue<byte>(_model, _attribute.Key, _attribute.Value);
                else
                    throw new NotImplementedException(
                        String.Format("Type of vertex attribute not yet implemented: {0}", type));
            }
        }

        public void Dispose()
        {
            foreach (var shader in _shaderPrograms)
                shader.Dispose();
            _shaderPrograms = null;
        }
    }
}
