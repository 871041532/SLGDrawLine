Shader "lineShader"{
        Properties
        {
                _MainTex("Line Texture",2D) = "white"{}
				_ShowLength("Show Length",Float)=6.0 //展现的长度
				_AlphaLength("Alpha Length",Float)=5.0 //有透明效果的长度
				_AllShowLength("AllShow Length",Float)=10 //临界值，如果线比它短，那么全都显示
        }
         SubShader{
			Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
			Pass
			{
				Tags{ "LightMode" = "ForwardBase" }
				ZWrite Off
				Blend SrcAlpha OneMinusSrcAlpha


        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "Lighting.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float _ShowLength;
		float _AlphaLength;
		float _AllShowLength;

        struct a2v
        {
                float4 vertex:POSITION;
                float4 texcoord:TEXCOORD0;
				fixed4 color:COLOR0;
				float2 uv2 : TEXCOORD1; //x分量存储y轴缩放，y分量存储滚动速度				
        };

        struct v2f
        {
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD1;
				fixed4 color:COLOR1;
				float2 uv2:TEXCOORD2; //x分量存储Y轴缩放固定值，y分量存储滚动前uv.y
        };

        v2f vert(a2v v)
        {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
                o.uv.y *=v.uv2.x;//Y轴缩放
				o.color=v.color;
				
				o.uv2=o.uv;
				o.uv2.x=v.uv2.x; //o.uv2.x存储Y轴缩放固定值，y轴当前uv.y
                o.uv.y -=v.uv2.y* _Time.z; //Y轴随时间偏移
                return o;
        }

        fixed4 frag(v2f i) :SV_Target
        {
                fixed4 color = tex2D(_MainTex,i.uv.xy);
				//后半段透明值与前半段透明值，取最大值
				i.color.a=max(clamp((_ShowLength-(i.uv2.x-i.uv2.y))/_AlphaLength,0,1),clamp((_ShowLength-i.uv2.y)/_AlphaLength,0,1)); //i.uv2.y存储此片元的UV.y
				//当比_AllShowLength小的时候全部显示
				i.color.a+=clamp(_AllShowLength-i.uv2.x,0,1); //大于0
				i.color.a *= color.a;
                return i.color;
        }

        ENDCG
        }
        }
        //Fallback "VertexLit"
}
