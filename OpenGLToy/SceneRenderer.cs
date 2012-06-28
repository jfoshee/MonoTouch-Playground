using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;
        MyModel _model;
        ShaderUniforms _modelUniforms;

        public SceneRenderer()
        {
            _model = new MyModel();
            _shaderProgram = new ShaderProgram(
                "Shader", 
                new string[] { "position", "color" });
            _modelUniforms = new ShaderUniforms(_model, _shaderProgram.Program);
        }

        static float[] squareVertices = {
            -0.5f, -0.33f,
            0.5f, -0.33f,
            -0.5f,  0.33f,
            0.5f,  0.33f,
        };
        static byte[] squareColors = {
            255, 255,   0, 255,
            0,   255, 255, 255,
            0,     0,   0,   0,
            255,   0, 255, 255,
        };

        public void Render()
        {
            // Use shader program.
            _shaderProgram.Use();
            // Update uniform value.
            _model.transY += 0.075f;
            _model.transX += 0.05f;
            _modelUniforms.UpdateUniformValues();

            // Update attribute values.
            _shaderProgram.SetAttributeArray("position", 2, VertexAttribPointerType.Float, false, 0, squareVertices);
            _shaderProgram.SetAttributeArray("color", 4, VertexAttribPointerType.UnsignedByte, true, 0, squareColors);
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

