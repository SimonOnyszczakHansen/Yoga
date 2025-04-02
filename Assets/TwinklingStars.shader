Shader "Custom/TwinklingStars"
{
    Properties
    {
        _StarColor ("Star Color", Color) = (1,1,1,1)
        _BackgroundColor ("Background Color", Color) = (0,0,0,1)
        _StarDensity ("Star Density", Range(0, 1)) = 0.5
        _TwinkleSpeed ("Twinkle Speed", Range(0, 5)) = 1
        _Size ("Star Size", Range(0, 1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            // Render both sides of the shader
            Cull Off 
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

            float4 _StarColor;
            float4 _BackgroundColor;
            float _StarDensity;
            float _TwinkleSpeed;
            float _Size;

            // Hash function for generating randomness per grid cell.
            float hash(float2 p)
            {
                float h = dot(p, float2(127.1, 311.7));
                return frac(sin(h) * 43758.5453);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Tile UV space to create a grid of stars.
                float2 tiledUV = i.uv * 100.0;
                float2 gridPos = floor(tiledUV);
                float2 gridFrac = frac(tiledUV);

                // Generate a random value for each cell.
                float random = hash(gridPos);

                // Only create stars in cells based on density.
                if (random > _StarDensity)
                    return _BackgroundColor;

                // Center the star in the cell.
                float2 starCenter = gridFrac - 0.5;
                float distanceToCenter = length(starCenter);

                // Create a circular star shape.
                float star = smoothstep(_Size + 0.01, _Size, distanceToCenter);

                // Combine two sine waves (with a slight frequency variation) and then sharpen the peaks.
                float t1 = abs(sin(_Time.y * _TwinkleSpeed + random * 6.28));
                float t2 = abs(sin(_Time.y * (_TwinkleSpeed * 1.3) + random * 6.28));
                float twinkle = pow((t1 * 0.7 + t2 * 0.3), 3.0);

                star *= twinkle;

                return lerp(_BackgroundColor, _StarColor, star);
            }
            ENDCG
        }
    }
}
