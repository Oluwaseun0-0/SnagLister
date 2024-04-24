Shader "Custom/InvisibleExceptIntersection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float depthValue = LinearEyeDepth(tex2D(_CameraDepthTexture, i.screenPos.xy));
                

                // Check if depth value is closer than current fragment's depth
                if (depthValue >= i.screenPos.z)
                {
                    // Fragment is not occluded, render it normally
                    return tex2D(_MainTex, i.screenPos.xy);
                }
                else
                {
                    // Fragment is occluded, make it invisible
                    return half4(0,0,0,0);
                    //discard;
                }
            }
            ENDCG
        }
    }
}
/*Dont forget to enable "Depth Texture" on your camera.
Make sure your lights are not messing up your material aswell*/
