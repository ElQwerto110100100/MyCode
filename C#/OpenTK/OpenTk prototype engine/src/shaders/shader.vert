#version 400 core
in vec3 Position;
in vec2 TexCoord;

out vec2 pass_textureCoords;

out vec3 Colour;

uniform mat4 transform;

void main()
{
    gl_Position = transform * vec4(Position, 1.0f);
    pass_textureCoords = TexCoord;
    Colour = vec3(Position.x + 0.5, 0.5,Position.y+0.5);
}