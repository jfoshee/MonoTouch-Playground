using System;

namespace OpenGLToy
{
    public class MyModel
    {
        float[] _position = {
            -0.5f, -0.33f,
            0.5f, -0.33f,
            -0.5f,  0.33f,
            0.5f,  0.33f,
        };
                
        byte[] _color = {
            255, 255,   0, 255,
            0,   255, 255, 255,
            0,     0,   0,   0,
            255,   0, 255, 255,
        };

        [VertexAttribute]
        public float[] position { get { return _position; } }

        [VertexAttribute]
        public byte[] color { get { return _color; } }

        [Uniform]
        public float transY { get; set; }

        [Uniform]
        public float transX  { get; set; }
    }
}

