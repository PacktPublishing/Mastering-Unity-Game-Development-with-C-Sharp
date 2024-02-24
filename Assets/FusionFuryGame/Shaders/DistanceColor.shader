Shader"Custom/DistanceColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _DistanceFactor ("Distance Factor", Float) = 0.1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
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
    float distance : TEXCOORD0;
};

float4 _Color;
float _DistanceFactor;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.distance = length(UnityObjectToViewPos(v.vertex));
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 col = _Color;
    col.rgb *= saturate(1.0 - i.distance * _DistanceFactor);
    return col;
}
            ENDCG
        }
    }
}
