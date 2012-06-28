using System;
using System.Collections.Generic;
using OpenTK.Graphics.ES20;

namespace OpenGLToy
{
    public class ShaderUtilities
    {
        public static bool CompileShader(ShaderType type, string file, out int shader)
        {
            string src = System.IO.File.ReadAllText(file);
            shader = GL.CreateShader(type);
            GL.ShaderSource(shader, 1, new string[] { src }, (int[])null);
            GL.CompileShader(shader);
#if DEBUG
            int logLength = 0;
            GL.GetShader (shader, ShaderParameter.InfoLogLength, out logLength);
            if (logLength > 0) 
            {
                var infoLog = new System.Text.StringBuilder ();
                GL.GetShaderInfoLog (shader, logLength, out logLength, infoLog);
                Console.WriteLine ("Shader compile log:\n{0}", infoLog);
            }
#endif
            int status = 0;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out status);
            if (status == 0)
            {
                GL.DeleteShader(shader);
                return false;
            }
            
            return true;
        }
        
        public static bool LinkProgram(int prog)
        {
            GL.LinkProgram(prog);
#if DEBUG
            int logLength = 0;
            GL.GetProgram (prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0) 
            {
                var infoLog = new System.Text.StringBuilder ();
                GL.GetProgramInfoLog (prog, logLength, out logLength, infoLog);
                Console.WriteLine ("Program link log:\n{0}", infoLog);
            }
#endif
            int status = 0;
            GL.GetProgram(prog, ProgramParameter.LinkStatus, out status);
            if (status == 0)
                return false;
            return true;
        }
        
        public static bool ValidateProgram(int prog)
        {
            GL.ValidateProgram(prog);
            int logLength = 0;
            GL.GetProgram(prog, ProgramParameter.InfoLogLength, out logLength);
            if (logLength > 0)
            {
                var infoLog = new System.Text.StringBuilder();
                GL.GetProgramInfoLog(prog, logLength, out logLength, infoLog);
                Console.WriteLine("Program validate log:\n{0}", infoLog);
            }
            int status = 0;
            GL.GetProgram(prog, ProgramParameter.LinkStatus, out status);
            if (status == 0)
                return false;
            return true;
        }

        public static Dictionary<string, int> GetLocations(IList<string> names, int shaderProgram, Func<int, string, int> getLocation)
        {
            Dictionary<string, int> locations = new Dictionary<string, int>(names.Count);
            foreach (var name in names)
                locations[name] = getLocation(shaderProgram, name);
            return locations;
        }

    }
}

