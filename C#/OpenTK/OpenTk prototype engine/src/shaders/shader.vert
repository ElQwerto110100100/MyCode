#version 400 core
in vec3 Position;
in vec2 TexCoord;

out vec2 pass_textureCoords;

out vec3 Colour;

void main()
{
    gl_Position = vec4(Position, 1.0);
    pass_textureCoords = TexCoord;
    Colour = vec3(Position.x + 0.5, 0.5,Position.y+0.5);
}