using System;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class SceneRenderer : IDisposable
    {
        ShaderProgram _shaderProgram;

        public SceneRenderer()
        {
            _shaderProgram = new ShaderProgram(
                "Shader", 
                new string[] { "position", "color" },
                new string[] { "translate", "translate2" });
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
        const int ATTRIB_VERTEX = 0;
        const int ATTRIB_COLOR = 1;

        public void Render()
        {
            // Use shader program.
            _shaderProgram.Use();
                
            // Update uniform value.
            GL.Uniform1(_shaderProgram.Uniforms ["translate"], transY);
            GL.Uniform1(_shaderProgram.Uniforms ["translate2"], transX);
            transY += 0.075f;
            transX += 0.05f;
                
            // Update attribute values.
            GL.VertexAttribPointer(ATTRIB_VERTEX, 2, VertexAttribPointerType.Float, false, 0, squareVertices);
            GL.EnableVertexAttribArray(ATTRIB_VERTEX);
            GL.VertexAttribPointer(ATTRIB_COLOR, 4, VertexAttribPointerType.UnsignedByte, true, 0, squareColors);
            GL.EnableVertexAttribArray(ATTRIB_COLOR);
                
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

