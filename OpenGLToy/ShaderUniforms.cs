using System.Collections.Generic;

namespace OpenGLToy
{
    public class ShaderUniforms
    {
        private object _model;
        Dictionary<string, int> _uniformLocations;

        public ShaderUniforms(object model, int shaderProgram)
        {
            _model = model;
            _uniformLocations = UniformAttribute.GetUniformLocations(model, shaderProgram);
        }

        public void UpdateUniformValues()
        {
            foreach (var uniform in _uniformLocations)
                UniformAttribute.UpdateUniformValue(_model, uniform.Key, uniform.Value);
        }
    }
}

