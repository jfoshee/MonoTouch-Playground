attribute vec4 position;
attribute vec4 color;

varying vec4 colorVarying;

uniform float translate;
uniform float translate2;

void main()
{
    gl_Position = position;
    gl_Position.y += sin(translate) / 2.0;
    gl_Position.x += cos(translate2) / 2.0;
    colorVarying = color;
}
