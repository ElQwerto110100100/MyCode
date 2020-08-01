#version 400 core
in vec2 pass_textureCoords;
in vec3 Colour;

out vec4 out_Colour;

uniform sampler2D textureSampler;
uniform sampler2D textureSampler2;

void main()
{
    out_Colour = mix(texture(textureSampler, pass_textureCoords), texture(textureSampler2, pass_textureCoords), 0.3)* vec4(Colour, 1.0);
}