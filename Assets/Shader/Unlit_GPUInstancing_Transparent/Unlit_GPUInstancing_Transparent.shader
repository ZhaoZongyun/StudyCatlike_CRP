Shader "Custom RP/Unlit_GPUInstancing_Transparent"
{
    Properties
    {
        _BaseColor("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _BaseMap("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.BlendMode)] _ScrBlennd("Src Blend",Float) = 0
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("Dst Blend", Float) = 1

        [Enum(Off, 0, On, 1)] _ZWrite("Z Write", Float) = 1
        [Toggle(IsClip)] _Clipping("Alpha Clip", Float) = 0

        _Cutoff("Alpha Clip Threshold", Range(0.0, 1.0)) = 0.5
    }

    SubShader
    {

        Pass
        {
            Blend [_ScrBlennd] [_DstBlend]
            ZWrite [_ZWrite]

            HLSLPROGRAM

            #pragma shader_feature IsClip
            #pragma multi_compile_instancing

            #pragma vertex UnlitPassVertex
            #pragma fragment UnlitPassFragment
          
            #include "UnlitPass_GPUInstancing_Transparent.hlsl" 
            ENDHLSL

        }
    }
}
