#ifndef CUSTOM_UNITY_INPUT_INCLUDE
#define CUSTOM_UNITY_INPUT_INCLUDE

float4x4 unity_ObjectToWorld;
float4x4 unity_WorldToObject;

float4x4 unity_prev_MatrixM;
float4x4 unity_prev_MatrixIM;
float4x4 unity_MatrixV;
float4x4 unity_MatrixInvV;
float4x4 unity_MatrixVP;
float4x4 glstate_matrix_projection;

#endif