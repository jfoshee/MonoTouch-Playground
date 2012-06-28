using System;
using OpenTK.Graphics.ES20;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace OpenGLToy
{
    public class ShaderProgram : IDisposable
    {
        int _program;
        public int Program { get { return _program; } } 
        public Dictionary<string, int> Attributes { get; private set; }

        public ShaderProgram(string shaderName, IList<string> attributes)
        {
            Attributes = new Dictionary<string, int>();

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
            
            // Attach vertex shader to program.
            GL.AttachShader(_program, vertShader);
            
            // Attach fragment shader to program.
            GL.AttachShader(_program, fragShader);
            
            // Bind attribute locations.
            // This needs to be done prior to linking.
            for (int i = 0; i < attributes.Count; i++)
            {
                GL.BindAttribLocation(_program, i, attributes [i]);
                Attributes [attributes [i]] = i;
            }
            
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

        public void SetAttributeArray(string attributeName, int size, VertexAttribPointerType type, bool normalized, int stride, float[] values)
        {
            GL.VertexAttribPointer(Attributes[attributeName], size, type, normalized, stride, values);
            GL.EnableVertexAttribArray(Attributes[attributeName]);
        }

        public void SetAttributeArray(string attributeName, int size, VertexAttribPointerType type, bool normalized, int stride, byte[] values)
        {
            GL.VertexAttribPointer(Attributes[attributeName], size, type, normalized, stride, values);
            GL.EnableVertexAttribArray(Attributes[attributeName]);
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

