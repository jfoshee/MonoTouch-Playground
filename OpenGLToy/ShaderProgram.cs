using System;
using OpenTK.Graphics.ES20;
using MonoTouch.Foundation;

namespace OpenGLToy
{
    public class ShaderProgram : IDisposable
    {
        int _program;
        public int Program { get { return _program; } } 

        public ShaderProgram(string shaderName)
        {
            int vertShader, fragShader;
            
            // Create shader program.
            _program = GL.CreateProgram();
            
            // Create and compile vertex shader.
            var vertShaderPathname = NSBundle.MainBundle.PathForResource(shaderName, "vsh");
            if (!ShaderUtilities.CompileShader(ShaderType.VertexShader, vertShaderPathname, out vertShader))
            {
                Console.WriteLine("Failed to compile vertex shader");
                throw new Exception("Failed to compile vertex shader");
            }
            
            // Create and compile fragment shader.
            var fragShaderPathname = NSBundle.MainBundle.PathForResource(shaderName, "fsh");
            if (!ShaderUtilities.CompileShader(ShaderType.FragmentShader, fragShaderPathname, out fragShader))
            {
                Console.WriteLine("Failed to compile fragment shader");
                throw new Exception("Failed to compile fragment shader");
            }
            GL.AttachShader(_program, vertShader);
            GL.AttachShader(_program, fragShader);

            // If we wanted to bind attribute locations we must do it here, before linking

            // Link program.
            if (!ShaderUtilities.LinkProgram(_program))
            {
                Console.WriteLine("Failed to link program: {0:x}", _program);
                if (vertShader != 0)
                    GL.DeleteShader(vertShader);
                if (fragShader != 0)
                    GL.DeleteShader(fragShader);
                if (_program != 0)
                {
                    GL.DeleteProgram(_program);
                    _program = 0;
                }
                throw new Exception(String.Format("Failed to link program: {0:x}", _program));
            }
            // Release vertex and fragment shaders.
            if (vertShader != 0)
            {
                GL.DetachShader(_program, vertShader);
                GL.DeleteShader(vertShader);
            }
            if (fragShader != 0)
            {
                GL.DetachShader(_program, fragShader);
                GL.DeleteShader(fragShader);
            }
        }

        public void Validate()
        {
            if (!ShaderUtilities.ValidateProgram(_program))
            {
                Console.WriteLine("Failed to validate program {0:x}", _program);
                throw new Exception("Frailed to validate program.");
            }
        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        public void Dispose()
        {
            if (_program != 0)
                GL.DeleteProgram(_program);
            _program = 0;
        }
    }
}

