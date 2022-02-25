#version 330 core

in vec4 fColor;
in vec2 fUv;

uniform sampler2D uTexture0;

out vec4 FragColor;

void main() {
	vec4 tmpColor = fColor;
	tmpColor +=	texture(uTexture0, fUv);
	FragColor = tmpColor;
}