Shader "Unlit/SimpleDeformation"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                //o.vertex.y += sqrt(1*2 - o.vertex.x*o.vertex.x - o.vertex.z*o.vertex.z);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 worldPos = o.worldPos;                
                if (sqrt(worldPos.x*worldPos.x + worldPos.z*worldPos.z) <= 4) {
                    o.vertex.y += (sqrt(4*4 - worldPos.x*worldPos.x - worldPos.z*worldPos.z)) * 1;
                } else {
                   
                }
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;             
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                if (sqrt(i.worldPos.x*i.worldPos.x + i.worldPos.z*i.worldPos.z) <= 3.8) {
                    col.a = 1;
                }
                else {
                    col.a = 0;
                }

                return col;
            }
            ENDCG
        }
    }
}
