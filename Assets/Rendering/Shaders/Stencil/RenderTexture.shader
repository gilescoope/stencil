Shader "Stencil/RenderTexture" {
Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
    Cull Off
    Lighting Off
    ZWrite Off
    ZTest Always

    Blend One OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert (appdata_t v)
            {
                v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                return col;
            }
        ENDCG
    }
}

}
