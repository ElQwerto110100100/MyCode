#version 400 core
in vec3 Position;

out vec3 Colour;

void main()
{
    gl_Position = vec4(Position, 1.0);
    Colour = vec3(Position.x+0.9, 0.5,Position.y+0.5);
}