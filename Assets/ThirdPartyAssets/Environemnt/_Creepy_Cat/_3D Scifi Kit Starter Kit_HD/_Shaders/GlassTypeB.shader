Shader "Hedgehog Team/Glass Reflective"
{
    Properties
    {
        _MainTint ("Diffuse Color (Alpha Controllable)", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
        _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
        _AO("Ambient Occlusion Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Cube ("Cubemap", CUBE) = ""{}
        _ReflAmount ("Reflection Amount", Range(0,1)) = 0.5
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    
    SubShader
     {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 400
        
        CGPROGRAM
        #pragma surface surf BlinnPhong alpha
        #pragma target 3.0

        samplerCUBE _Cube;
        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _AO;
        float4 _MainTint;
        float _ReflAmount;
        float4 _RimColor;
    float _RimPower;
        half _Shininess;
    
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float2 uv_AO;
            float3 worldRefl;
            float3 viewDir;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            half4 ao = tex2D(_AO, IN.uv_AO);
            c *= ao;
            float3 normals = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap)).rgb;
            o.Normal = normals;
            o.Specular = _Shininess;
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Gloss = c.a;
            o.Emission = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb * _ReflAmount + _RimColor.rgb * pow (rim, _RimPower);
            o.Albedo = c.rgb * _MainTint;
            o.Alpha = c.a * _MainTint.a;
        }
        ENDCG
    }
    FallBack "Transparent/Specular"
} 