#version 330 core

layout (location = 0) in vec3 vPos;
layout (location = 1) in vec4 vColor;

uniform float uRed;
uniform float uGreen;
uniform float uBlue;

out vec4 fColor;

void main() {
	gl_Position = vec4(vPos, 1.0);
	
	vec4 color = vec4(uGreen*vPos.x, uBlue*vPos.y, uRed*vPos.z,0);
	
	float alpha = ((color.r+color.g+color.b)/3);
	
	fColor = vec4(color.r,color.g,color.b, alpha);
}
