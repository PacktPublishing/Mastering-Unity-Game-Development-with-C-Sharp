Shader"Universal Render Pipeline/OutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range (0.0, 0.1)) = 0.005
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalRenderPipeline" }

        Pass
        {
Name"Outline Pass"
            Tags
{
                "LightMode" = "UniversalForward"
}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
};

struct v2f
{
    float4 pos : SV_POSITION;
};

float _OutlineWidth;
float4 _OutlineColor;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 color = _OutlineColor;
    return color;
}
            ENDCG
        }
    }
FallBack"Diffuse"
}
