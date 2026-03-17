Shader "Custom/GrassShader"
{
    Properties
    {
        _MainTex ("Grass Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _BladeCount ("Blades Per Clump", Float) = 8
        _ClumpRadius ("Clump Radius", Float) = 0.15
        _RandomHeight ("Height Variation", Float) = 0.3
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }

        Cull Off
        ZWrite On

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;

            float _BladeCount;
            float _ClumpRadius;
            float _RandomHeight;
            float _Cutoff;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint instanceID : SV_InstanceID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float rand(float2 co)
            {
                return frac(sin(dot(co,float2(12.9898,78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;

                float id = v.instanceID;

                float angle = rand(float2(id,1)) * 6.28;
                float radius = rand(float2(id,2)) * _ClumpRadius;

                float2 offset;
                offset.x = cos(angle) * radius;
                offset.y = sin(angle) * radius;

                float heightScale = 1 + rand(float2(id,3)) * _RandomHeight;

                float3 pos = v.vertex.xyz;

                pos.xz += offset;
                pos.y *= heightScale;

                float4 world = mul(unity_ObjectToWorld, float4(pos,1));
                o.vertex = mul(UNITY_MATRIX_VP, world);

                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);

                tex *= _Color;

                clip(tex.a - _Cutoff);

                return tex;
            }

            ENDCG
        }
    }
}