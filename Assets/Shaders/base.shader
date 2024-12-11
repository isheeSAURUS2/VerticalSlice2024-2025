Shader "Custom/NormalRoughnessAOShader"
{
    Properties
    {
        _MainTex ("Base Texture (Albedo)", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _RoughnessMap ("Roughness Map", 2D) = "black" {}
        _AOMap ("Ambient Occlusion Map", 2D) = "white" {}
        _Tiling ("Texture Tiling", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        // Uniforms (Texture and tiling info)
        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _RoughnessMap;
        sampler2D _AOMap;
        float4 _Tiling;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float2 uv_RoughnessMap;
            float2 uv_AOMap;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Tiling and UV adjustments
            float2 uv = IN.uv_MainTex * _Tiling.xy + _Tiling.zw;

            // Sample textures
            fixed4 albedo = tex2D(_MainTex, uv);
            fixed4 normalTex = tex2D(_NormalMap, IN.uv_NormalMap);
            fixed4 roughnessTex = tex2D(_RoughnessMap, IN.uv_RoughnessMap);
            fixed4 aoTex = tex2D(_AOMap, IN.uv_AOMap);

            // Base Color with AO applied
            o.Albedo = albedo.rgb * aoTex.r; // AO scales the albedo brightness

            // Normal Map (convert from texture data to a normal vector)
            o.Normal = UnpackNormal(normalTex);

            // Smoothness from Roughness Map (inverted roughness)
            o.Smoothness = 1.0 - roughnessTex.r;

            // Optional metallic (set to 0 for non-metal materials)
            o.Metallic = 0.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
