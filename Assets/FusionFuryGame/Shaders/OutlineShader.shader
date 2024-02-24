Shader"Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range (0.0, 0.1)) = 0.03
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
Name"OUTLINE"
            Tags
{"LightMode" = "Always"
}

Cull Front

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
    float4 pos : POSITION;
    float4 color : COLOR;
};

float _OutlineWidth;
float4 _OutlineColor;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);

    float3 norm = normalize(mul((float3x3) unity_ObjectToWorld, v.vertex.xyz));
    o.pos.xy += norm.xy * _OutlineWidth * o.pos.w;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    return _OutlineColor;
}
            ENDCG
        }

        Pass
        {
Name"BASE"
            Tags
{"LightMode" = "Always"
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
    float4 pos : POSITION;
    float2 uv : TEXCOORD0;
};

sampler2D _MainTex;
fixed4 _OutlineColor;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = float2(v.vertex.x, v.vertex.y);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    fixed4 col = tex2D(_MainTex, i.uv);
    return col;
}
            ENDCG
        }
    }
}
