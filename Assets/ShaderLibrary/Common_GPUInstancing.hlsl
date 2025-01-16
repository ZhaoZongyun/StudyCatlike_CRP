#ifndef CUSTOM_COMMON_INCLUDE
#define CUSTOM_COMMON_INCLUDE

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
 
#include "UnityInput_GPUInstancing.hlsl"

#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject

#define UNITY_PREV_MATRIX_M unity_prev_MatrixM1
#define UNITY_PREV_MATRIX_I_M unity_prev_MatrixIM1
#define UNITY_MATRIX_V unity_MatrixV1
#define UNITY_MATRIX_I_V unity_MatrixInvV1
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection1

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif
