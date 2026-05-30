shader_type spatial;
render_mode unshaded;

uniform vec3 color: source_color = vec3(1.0, 1.0, 1.0);
uniform vec3 border_color: source_color = vec3(0, 0, 0);

uniform float border_width : hint_range(0.0, 1.0) = 0.5;
uniform float border_softness : hint_range(0.001, 0.5) = 0.2;

void fragment() {
    float fresnel = dot(NORMAL, VIEW);
    float border = smoothstep(border_width - border_softness, border_width + border_softness, fresnel);
    ALBEDO = mix(border_color, color, border);
}