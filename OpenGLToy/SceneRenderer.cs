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
                new string[] { "position", "color" });
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
        const int UNIFORM_TRANSLATE = 0;
        const int UNIFORM_COUNT = 1;
        int[] uniforms = new int [UNIFORM_COUNT];
                
        const int ATTRIB_VERTEX = 0;
        const int ATTRIB_COLOR = 1;
        const int ATTRIB_COUNT = 2;

        public void Render()
        {
                // Use shader program.
            _shaderProgram.Use();
                //GL.UseProgram (program);
                
                // Update uniform value.
                GL.Uniform1 (uniforms [UNIFORM_TRANSLATE], transY);
                transY += 0.075f;
                
                // Update attribute values.
                GL.VertexAttribPointer (ATTRIB_VERTEX, 2, VertexAttribPointerType.Float, false, 0, squareVertices);
                GL.EnableVertexAttribArray (ATTRIB_VERTEX);
                GL.VertexAttribPointer (ATTRIB_COLOR, 4, VertexAttribPointerType.UnsignedByte, true, 0, squareColors);
                GL.EnableVertexAttribArray (ATTRIB_COLOR);
                
                // Validate program before drawing. This is a good check, but only really necessary in a debug build.
#if DEBUG
                if (!ShaderUtilities.ValidateProgram (_shaderProgram._program)) {
                    Console.WriteLine ("Failed to validate program {0:x}", _shaderProgram._program);
                    return;
                }
#endif

            GL.DrawArrays (BeginMode.TriangleStrip, 0, 4);
        }

        public void Dispose()
        {
            if (_shaderProgram != null)
                _shaderProgram.Dispose();
            _shaderProgram = null;
        }
    }
}

