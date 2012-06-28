using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;
        MyModel _model;
        ModelUniforms _modelUniforms;

        public SceneRenderer()
        {
            _model = new MyModel();
            _shaderProgram = new ShaderProgram(
                "Shader", 
                new string[] { "position", "color" });
            _modelUniforms = new ModelUniforms(_model, _shaderProgram.Program);
        }

        public void Render()
        {
            _model.Update();
            
            _shaderProgram.Use();
            _modelUniforms.UpdateUniformValues();


            // Update attribute values.
            var vertexAttributes = VertexAttributeAttribute.GetLocations(_model, _shaderProgram.Program);
            VertexAttributeAttribute.UpdateValue(_model, "position", vertexAttributes["position"]);

            //_shaderProgram.SetAttributeArray("position", 2, VertexAttribPointerType.Float, false, 0, _model.position);
            _shaderProgram.SetAttributeArray("color", 4, VertexAttribPointerType.UnsignedByte, true, 0, _model.color);
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

