// 贴图和自定义颜色正片叠底（*=）
Shader "zzy/ColorMultiply"
{
    Properties
    {
        _Color("color", Color)=(1,0,0,1)
        _MainTex ("texture", 2D) = "white" {}       // 属性定义
    }

    SubShader                                       // 子着色器
    {
        Tags { "RenderType"="Opaque" }              // 标签、叠加模式、LOD
        LOD 100

        Pass                                        // 通道
        {
            Tags {"LightMode" = "MyTest"}
            CGPROGRAM                               // CG 程序块
            #pragma vertex vert                     // 顶点、片段函数定义
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog               // 变体定义

            #include "UnityCG.cginc"

            struct appdata                          // 结构体定义（用于函数参数）
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;                          // 自定义属性、贴图采样器
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)                    // 顶点函数
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target         // 片段函数
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // 正片叠底
                col*= _Color;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"                              // 备选着色器
}
