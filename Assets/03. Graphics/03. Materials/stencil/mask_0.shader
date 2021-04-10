Shader "Custom/mask_0"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
        {
             Stencil
            {
                Ref 0
                Comp always
                Pass replace
            }

            Tags { "RenderType" = "Opaque" }
            LOD 200
            ColorMask 0
            ZWrite off

            Pass
            {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                struct appdata
                {
                    float4 vertex : POSITION;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                };

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                half4 frag(v2f i) : COLOR
                {
                    return half4(1,1,0,1);
                }
            ENDCG
            }
        }
    //FallBack "Diffuse"
}
