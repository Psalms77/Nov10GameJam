Shader "Hidden/LineTest"
{
    Properties
    {
//        _MainTex ("Texture", 2D) = "white" {}
        _LineDensity("激光粗细", Range(0.1, 1)) = 0.2 
        _Color ("激光颜色", Color) = (1, 1, 1, 1)
        _Discrete ("离散化层数，越高越渐变", Range(1, 10)) = 4
        _LumiAdjust ("亮度调整系数，越高，整体亮度越高", Range(0.5, 4)) = 1
        _AlphaAdjust ("透明度调整系数，越高，整体亮度越高", Range(0.5, 4)) = 1
    }
    SubShader
    {
        Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 _Color;
            float _LineDensity;
            float _Discrete;
            float _LumiAdjust;
            float _AlphaAdjust;

            fixed4 frag (v2f i) : SV_Target
            {
                // in fragment shader
                fixed4 resultColor = _Color;
                resultColor.rgb = _Color.rgb * floor((1 - abs(i.uv.y - 0.5) * 1 / _LineDensity) * _Discrete) / _Discrete;
                resultColor.a = floor((1 - abs(i.uv.y - 0.5) * 1 / _LineDensity) * _Discrete) / _Discrete;
                resultColor.rgb = resultColor.rgb * _LumiAdjust;
                resultColor.a = resultColor.a * _AlphaAdjust;
                return resultColor;
            }
            
            ENDCG
        }
    }
}
