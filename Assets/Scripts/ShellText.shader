// This shader fills the mesh shape with a color that a user can change using the
// Inspector window on a Material.
Shader "Perso/ShellTexturing"
{
    // The _BaseColor variable is visible in the Material's Inspector, as a field
    // called Base Color. You can use it to select a custom color. This variable
    // has the default value (1, 1, 1, 1).
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [NoGrass] _NoGrass("No Grass Color", Color) = (0, 0, 0, 1)
        _Density ("Density", Int) = 100
        _ShellCount ("Shell Count", Int) = 8
        _ShellIndex ("Shell Index", Int) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "Queue"="AlphaTest+51"}

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

         struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // This is a hashing function that takes in an unsigned integer seed and shuffles it around to make it seem random
			// The output is in the range 0 to 1, so you do not have to worry about that and can easily convert it to any other
			// range you desire by multiplying the output with any number.
			float hash(uint n) {
				// integer hash copied from Hugo Elias
				n = (n << 13U) ^ n;
				n = n * (n * n * 15731U + 0x789221U) + 0x1376312589U;
				return float(n & uint(0x7fffffffU)) / float(0x7fffffff);
			}

            CBUFFER_START(UnityPerMaterial)
                // The following line declares the _BaseColor variable, so that you
                // can use it in the fragment shader.
                half4 _BaseColor;
                half4 _NoGrass;
                int _Density;
                int _ShellCount;
                int _ShellIndex;
            CBUFFER_END

            v2f vert (
                float4 vertex : POSITION, // vertex position input
                float2 uv : TEXCOORD0 // first texture coordinate input
                )
            {
                v2f o;
                o.pos = TransformObjectToHClip(vertex);
                o.uv = uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 newUV = i.uv * _Density;
                uint2 tid = newUV;
				uint seed = tid.x + 100 * tid.y + 100 * 10;
                float rng = hash(seed);
                float shellIndex = _ShellIndex;
                float shellCount = _ShellCount;
                float h = shellIndex / shellCount;
                if(rng <= h)
                {
                    discard;
                }
                return _BaseColor*h;
            }
            ENDHLSL
        }
    }
}