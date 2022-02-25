#version 330 core

layout (location = 0) in vec3 vPos;
layout (location = 1) in vec4 vColor;
layout (location = 2) in vec2 vTextureUV;

uniform float uRed;
uniform float uGreen;
uniform float uBlue;
uniform float uAlpha;

out vec4 fColor;
out vec2 fUv;

void main() {
	gl_Position = vec4(vPos, 1.0);
	fColor = vec4((uRed+vColor.r)/2, (uGreen+vColor.g)/2, (uBlue+vColor.b)/2, (uAlpha+vColor.a)/2);
	fUv = vTextureUV;
}
