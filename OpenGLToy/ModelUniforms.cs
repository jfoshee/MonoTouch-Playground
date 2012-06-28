using System.Collections.Generic;

namespace OpenGLToy
{
    public class ModelUniforms
    {
        private object _model;
        Dictionary<string, int> _uniformLocations;

        public ModelUniforms(object model, int shaderProgram)
        {
            _model = model;
            _uniformLocations = UniformAttribute.GetLocations(model, shaderProgram);
        }

        public void UpdateUniformValues()
        {
            foreach (var uniform in _uniformLocations)
                UniformAttribute.UpdateValue(_model, uniform.Key, uniform.Value);
        }
    }
}

