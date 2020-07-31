#version 400 core
in vec3 Colour;

out vec4 out_Colour;

void main()
{
    out_Colour = vec4(Colour, 1.0);
}