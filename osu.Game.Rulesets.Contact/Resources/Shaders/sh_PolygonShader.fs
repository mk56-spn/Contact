#define HIGH_PRECISION_VERTEX

#include "sh_Utils.h"
#include "sh_Masking.h"

uniform vec2 m_CenterPos;
varying highp vec2 v_TexCoord;


float sdCircle( in vec2 p, in float r )
{
    return length(p)-r;
}


void main()
{

    highp vec2 pixelPos = v_TexCoord / (v_TexRect.zw - v_TexRect.xy) - vec2(0.5);
    
    float d = sdCircle(pixelPos,0.1);

    // coloring
    vec3 col = (d>0.0) ? vec3(0.9,0.6,0.3) : vec3(0.65,0.85,1.0);
    col *= 1.0 - exp(-6.0*abs(d));
    col *= 0.8 + 0.2*cos(150.0*d);
    col = mix( col, vec3(1.0), 1.0-smoothstep(0.0,0.01,abs(d)) );
    
    gl_FragColor = vec4(col,1.0);
}