using System;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;
        MyModel _model;
        ModelBindings _modelBindings;

        public SceneRenderer()
        {
            _model = new MyModel();
            _shaderProgram = new ShaderProgram("Shader");
            _modelBindings = new ModelBindings(_model, _shaderProgram.Program);
        }

        public void Render()
        {
            _model.Update();
            _shaderProgram.Use();
            _modelBindings.Draw();
#if DEBUG
            _shaderProgram.Validate();
#endif
        }

        public void Dispose()
        {
            if (_shaderProgram != null)
                _shaderProgram.Dispose();
            _shaderProgram = null;
        }
    }
}

