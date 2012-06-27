using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;
        MyModel _model;

        public SceneRenderer()
        {
            _model = new MyModel();
            _shaderProgram = new ShaderProgram(
                "Shader", 
                new string[] { "position", "color" },
                UniformAttribute.GetUniformNames(_model));
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
        static float transY = 0.0f;
        static float transX = 0.0f;

        public void Render()
        {
            // Use shader program.
            _shaderProgram.Use();
            // Update uniform value.
            _shaderProgram.SetUniform("transX", transX);
            _shaderProgram.SetUniform("transY", transY);
            transY += 0.075f;
            transX += 0.05f;
            // Update attribute values.
            _shaderProgram.SetAttributeArray("position", 2, VertexAttribPointerType.Float, false, 0, squareVertices);
            _shaderProgram.SetAttributeArray("color", 4, VertexAttribPointerType.UnsignedByte, true, 0, squareColors);
#if DEBUG
            // Validate program before drawing. This is a good check, but only really necessary in a debug build.
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

