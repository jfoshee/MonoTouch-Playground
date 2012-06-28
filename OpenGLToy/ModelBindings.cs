using System;
using System.Collections.Generic;

namespace OpenGLToy
{
    public class ModelBindings
    {
        private object _model;
        Dictionary<string, int> _uniformLocations;
        Dictionary<string, int> _attributeLocations;

        public ModelBindings(object model, int shaderProgram)
        {
            _model = model;
            _uniformLocations = UniformAttribute.GetLocations(model, shaderProgram);
            _attributeLocations = VertexAttributeAttribute.GetLocations(model, shaderProgram);
        }

        public void Draw()
        {
            UpdateUniformValues();
            UpdateAttributeValues();
            ModelAttribute.Draw(_model);
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
    }
}
