#ifndef CUSTOM_LIT_PASS_INCLUDE
#define CUSTOM_LIT_PASS_INCLUDE

 #include "../../ShaderLibrary/Common.hlsl"

struct Attributes {
    float3 positionOS:POSITION;
    float2 baseUV:TEXCOORD;
};

struct Varyings {
    float4 positionCS:SV_POSITION;
};

Varyings UnlitPassVertex(Attributes input) {
    Varyings output;
     float3 positionWS = TransformObjectToWorld(input.positionOS);
     output.positionCS = TransformWorldToHClip(positionWS);

    return output;
}

float4 UnlitPassFragment(Varyings input):SV_TARGET {
    return _BaseColor;
}

#endif
