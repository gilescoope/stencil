Shader "Stencil/Spray" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _NozzlePosition ("Nozzle Position", Vector) = (0, 0, 0, 0)
    _NozzleDirection ("Nozzle Direction", Vector) = (0, 0, 1, 0)
    _NozzleAngle ("Nozzle Angle", Float) = 0.5
    _RandomSeed ("Random Seed", Float) = 0
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    Cull Off
    Lighting Off
    ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha

    Pass {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _NozzlePosition;
            float4 _NozzleDirection;
            float _NozzleAngle;
            float _RandomSeed;
            fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                return o;
            }
            
            float rand(float3 co)
            {
                return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 position = i.worldPos.xyz - _NozzlePosition.xyz;
                float distance = dot(position, _NozzleDirection);
                float angle = acos(distance/length(position));
                float alpha = smoothstep(_NozzleAngle, 0.5*_NozzleAngle, angle) * min(1, 0.1 / (distance*distance));
                alpha = alpha * _Color.a;
                float noisyAlpha = max(0, min(1, alpha * 2*rand(_RandomSeed + i.worldPos)));
                fixed4 col = fixed4(_Color.rgb, noisyAlpha);
                return col;
            }
        ENDCG
    }
}

}
