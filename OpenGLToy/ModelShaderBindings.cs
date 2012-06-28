using System.Collections.Generic;

namespace OpenGLToy
{
    public class ModelShaderBindings
    {
        private object _model;
        Dictionary<string, int> _uniformLocations;
        Dictionary<string, int> _attributeLocations;

        public ModelShaderBindings(object model, int shaderProgram)
        {
            _model = model;
            _uniformLocations = UniformAttribute.GetLocations(model, shaderProgram);
            _attributeLocations = VertexAttributeAttribute.GetLocations(model, shaderProgram);
        }

        public void UpdateUniformValues()
        {
            foreach (var uniform in _uniformLocations)
                UniformAttribute.UpdateValue(_model, uniform.Key, uniform.Value);
        }

        public void UpdateAttributeValues()
        {
            // TODO: Loop through vertex attributes
            VertexAttributeAttribute.UpdateValue<float>(_model, "position", _attributeLocations["position"]);
            VertexAttributeAttribute.UpdateValue<byte>(_model, "color", _attributeLocations["color"]);
        }
    }
}

