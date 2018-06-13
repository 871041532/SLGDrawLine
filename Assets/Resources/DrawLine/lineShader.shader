Shader "lineShader"{
        Properties
        {
                _MainTex("Line Texture",2D) = "white"{}
        }
                SubShader{
                Pass
        {
                Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout"}
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "Lighting.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;

        struct a2v
        {
                float4 vertex:POSITION;
				fixed normal:NORMAL;
				fixed tangent:TANGENT;
                float4 texcoord:TEXCOORD0;
				fixed3 color:COLOR0;
				
        };

        struct v2f
        {
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD1;
				fixed3 color:COLOR1;
        };

        v2f vert(a2v v)
        {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
                o.uv.y *=1/v.normal.x;//Y轴缩放
                o.uv.y -=1/v.tangent.x* _Time.z; //Y轴随时间偏移
				o.color=v.color;
                return o;
        }

        fixed4 frag(v2f i) :SV_Target
        {
                fixed4 color = tex2D(_MainTex,i.uv.xy);
                return fixed4(i.color,color.a);
        }

        ENDCG
        }
        }
        //Fallback "VertexLit"
}
