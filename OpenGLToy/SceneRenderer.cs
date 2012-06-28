using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;
        MyModel _model;
        ModelShaderBindings _shaderBindings;

        public SceneRenderer()
        {
            _model = new MyModel();
            _shaderProgram = new ShaderProgram("Shader");
            _shaderBindings = new ModelShaderBindings(_model, _shaderProgram.Program);
        }

        public void Render()
        {
            _model.Update();
            _shaderProgram.Use();
            _shaderBindings.UpdateShaderBindings();
#if DEBUG
            _shaderProgram.Validate();
#endif
            ModelAttribute.Draw(_model);
        }

        public void Dispose()
        {
            if (_shaderProgram != null)
                _shaderProgram.Dispose();
            _shaderProgram = null;
        }
    }
}

