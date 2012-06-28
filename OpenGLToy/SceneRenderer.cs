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
            _shaderBindings.UpdateUniformValues();
            _shaderBindings.UpdateAttributeValues();
#if DEBUG
            _shaderProgram.Validate();
#endif
            GL.DrawArrays(BeginMode.TriangleStrip, 0, 4);
        }

        public void Dispose()
        {
            if (_shaderProgram != null)
                _shaderProgram.Dispose();
            _shaderProgram = null;
        }
    }
}

