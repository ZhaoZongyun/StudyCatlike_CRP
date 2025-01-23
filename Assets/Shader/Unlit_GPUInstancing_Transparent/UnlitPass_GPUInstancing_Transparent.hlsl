#ifndef CUSTOM_UNLIT_PASS_INCLUDE
#define CUSTOM_UNLIT_PASS_INCLUDE

#include "../../ShaderLibrary/Common_GPUInstancing.hlsl"

TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);

// 属性定义 _BaseColor
UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
    UNITY_DEFINE_INSTANCED_PROP(float4, _BaseColor)
    UNITY_DEFINE_INSTANCED_PROP(float4, _BaseMap_ST)
    UNITY_DEFINE_INSTANCED_PROP(float, _Cutoff)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

struct Attributes {
    float3 positionOS:POSITION;
    float2 uv:TEXCOORD;
    UNITY_VERTEX_INPUT_INSTANCE_ID      // 对象索引
};

struct Varyings {
    float4 positionCS:SV_POSITION;
    float2 baseUV: VAR_BASEUV;
    UNITY_VERTEX_INPUT_INSTANCE_ID      // 对象索引
};

Varyings UnlitPassVertex(Attributes input) {
    Varyings output;
    UNITY_SETUP_INSTANCE_ID(input);    // 从input中提取索引，并存储在其它实例化宏所依赖的全局静态变量中
    UNITY_TRANSFER_INSTANCE_ID(input, output);
    float3 positionWS = TransformObjectToWorld(input.positionOS);
    output.positionCS = TransformWorldToHClip(positionWS);

    // 计算UV的缩放和偏移
    float4 baseST = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap_ST);
    output.baseUV = input.uv * baseST.xy + baseST.zw;

    return output;
}

float4 UnlitPassFragment(Varyings input):SV_TARGET {
    UNITY_SETUP_INSTANCE_ID(input);

    // 对纹理进行采样
    float4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.baseUV);
    float4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);

    // 颜色相乘
    float4 color = baseMap * baseColor;

    // 裁剪函数 
    #if defined (IsClip)
        clip(color.a - UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Cutoff));
     #endif

    return color;
}

#endif
