attribute vec4 position;
attribute vec4 color;

varying vec4 colorVarying;

uniform float transX;
uniform float transY;

void main()
{
    gl_Position = position;
    gl_Position.x += cos(transX) / 2.0;
    gl_Position.y += sin(transY) / 2.0;
    colorVarying = color;
}
