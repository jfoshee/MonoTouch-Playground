using System;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        MyModel _model;
        ModelBindings _modelBindings;

        public SceneRenderer()
        {
            _model = new MyModel();
            _modelBindings = new ModelBindings(_model);
        }

        public void Render()
        {
            _model.Update();
            _modelBindings.Draw();

        }

        public void Dispose()
        {
            _modelBindings.Dispose();
        }
    }
}

