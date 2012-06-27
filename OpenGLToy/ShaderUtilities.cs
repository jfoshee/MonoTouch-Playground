using System;
using OpenTK.Graphics.ES20;
using MonoTouch.Foundation;

namespace OpenGLToy
{
    public class ShaderProgram : IDisposable
    {
        internal int _program;
        // TODO: make private and move validation check here

        public ShaderProgram (string shaderName, string[] attributes)
        {
            int vertShader, fragShader;
            
            // Create shader program.
            _program = GL.CreateProgram ();
            
            // Create and compile vertex shader.
            var vertShaderPathname = NSBundle.MainBundle.PathForResource (shaderName, "vsh");
            if (!ShaderUtilities.CompileShader (ShaderType.VertexShader, vertShaderPathname, out vertShader)) {
                Console.WriteLine ("Failed to compile vertex shader");
                throw new Exception("Failed to compile vertex shader");
            }
            
            // Create and compile fragment shader.
            var fragShaderPathname = NSBundle.MainBundle.PathForResource (shaderName, "fsh");
            if (!ShaderUtilities.CompileShader (ShaderType.FragmentShader, fragShaderPathname, out fragShader)) {
                Console.WriteLine ("Failed to compile fragment shader");
                throw new Exception("Failed to compile fragment shader");
            }
            
            // Attach vertex shader to program.
            GL.AttachShader (_program, vertShader);
            
            // Attach fragment shader to program.
            GL.AttachShader (_program, fragShader);
            
            // Bind attribute locations.
            // This needs to be done prior to linking.
            for (int i = 0; i < attributes.Length; i++)
                GL.BindAttribLocation(_program, i, attributes[i]);
//            GL.BindAttribLocation (program, ATTRIB_VERTEX, "position");
//            GL.BindAttribLocation (program, ATTRIB_COLOR, "color");
            
            // Link program.
            if (!ShaderUtilities.LinkProgram (_program)) {
                Console.WriteLine ("Failed to link program: {0:x}", _program);
                
                if (vertShader != 0)
                    GL.DeleteShader (vertShader);
                
                if (fragShader != 0)
                    GL.DeleteShader (fragShader);
                
                if (_program != 0) {
                    GL.DeleteProgram (_program);
                    _program = 0;
                }
                throw new Exception(String.Format("Failed to link program: {0:x}", _program));
            }
                        
            // Get uniform locations.
            //uniforms [UNIFORM_TRANSLATE] = GL.GetUniformLocation (program, "translate");
            
            // Release vertex and fragment shaders.
            if (vertShader != 0) {
                GL.DetachShader (_program, vertShader);
                GL.DeleteShader (vertShader);
            }
                
            if (fragShader != 0) {
                GL.DetachShader (_program, fragShader);
                GL.DeleteShader (fragShader);
            }
            
        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        public void Dispose()
        {
            if (_program != 0)
                GL.DeleteProgram (_program);
            _program = 0;
        }
    }

    public class ShaderUtilities
    {
        //int program;


//        bool LoadShaders(string shaderName, string[] attributes)
//        {
//            int vertShader, fragShader;
//            
//            // Create shader program.
//            program = GL.CreateProgram ();
//            
//            // Create and compile vertex shader.
//            var vertShaderPathname = NSBundle.MainBundle.PathForResource (shaderName, "vsh");
//            if (!CompileShader (ShaderType.VertexShader, vertShaderPathname, out vertShader)) {
//                Console.WriteLine ("Failed to compile vertex shader");
//                return false;
//            }
//            
//            // Create and compile fragment shader.
//            var fragShaderPathname = NSBundle.MainBundle.PathForResource (shaderName, "fsh");
//            if (!CompileShader (ShaderType.FragmentShader, fragShaderPathname, out fragShader)) {
//                Console.WriteLine ("Failed to compile fragment shader");
//                return false;
//            }
//            
//            // Attach vertex shader to program.
//            GL.AttachShader (program, vertShader);
//            
//            // Attach fragment shader to program.
//            GL.AttachShader (program, fragShader);
//            
//            // Bind attribute locations.
//            // This needs to be done prior to linking.
//            for (int i = 0; i < attributes.Length; i++)
//                GL.BindAttribLocation(program, i, attributes[i]);
////            GL.BindAttribLocation (program, ATTRIB_VERTEX, "position");
////            GL.BindAttribLocation (program, ATTRIB_COLOR, "color");
//            
//            // Link program.
//            if (!LinkProgram (program)) {
//                Console.WriteLine ("Failed to link program: {0:x}", program);
//                
//                if (vertShader != 0)
//                    GL.DeleteShader (vertShader);
//                
//                if (fragShader != 0)
//                    GL.DeleteShader (fragShader);
//                
//                if (program != 0) {
//                    GL.DeleteProgram (program);
//                    program = 0;
//                }
//                return false;
//            }
//                        
//            // Get uniform locations.
//            //uniforms [UNIFORM_TRANSLATE] = GL.GetUniformLocation (program, "translate");
//            
//            // Release vertex and fragment shaders.
//            if (vertShader != 0) {
//                GL.DetachShader (program, vertShader);
//                GL.DeleteShader (vertShader);
//            }
//                
//            if (fragShader != 0) {
//                GL.DetachShader (program, fragShader);
//                GL.DeleteShader (fragShader);
//            }
//            
//            return true;
//        }
//

        void DestroyShaderProgram (int program)
        {
            if (program != 0) {
                GL.DeleteProgram (program);
            }
        }
        
        public static bool CompileShader (ShaderType type, string file, out int shader)
        {
            string src = System.IO.File.ReadAllText (file);
            shader = GL.CreateShader (type);
            GL.ShaderSource (shader, 1, new string[] { src }, (int[])null);
            GL.CompileShader (shader);
            
#if DEBUG
            int logLength = 0;
            GL.GetShader (shader, ShaderParameter.InfoLogLength, out logLength);
            if (logLength > 0) {
                var infoLog = new System.Text.StringBuilder ();
                GL.GetShaderInfoLog (shader, logLength, out logLength, infoLog);
                Console.WriteLine ("Shader compile log:\n{0}", infoLog);
            }
#endif
            int status = 0;
            GL.GetShader (shader, ShaderParameter.CompileStatus, out status);
            if (status == 0) {
                GL.DeleteShader (shader);
                return false;
            }
            
            return true;
        }
        
        public static bool LinkProgram (int prog)
        {
            GL.LinkProgram (prog);
            
#if DEBUG
            int logLength = 0;
            GL.GetProgram (prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0) {
                var infoLog = new System.Text.StringBuilder ();
                GL.GetProgramInfoLog (prog, logLength, out logLength, infoLog);
                Console.WriteLine ("Program link log:\n{0}", infoLog);
            }
#endif
            int status = 0;
            GL.GetProgram (prog, ProgramParameter.LinkStatus, out status);
            if (status == 0)
                return false;
            
            return true;
        }
        
        public static bool ValidateProgram (int prog)
        {
            GL.ValidateProgram (prog);
            
            int logLength = 0;
            GL.GetProgram (prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0) {
                var infoLog = new System.Text.StringBuilder ();
                GL.GetProgramInfoLog (prog, logLength, out logLength, infoLog);
                Console.WriteLine ("Program validate log:\n{0}", infoLog);
            }

            int status = 0;
            GL.GetProgram (prog, ProgramParameter.LinkStatus, out status);
            if (status == 0)
                return false;
            
            return true;
        }
    }
}

