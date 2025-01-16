﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInstancingMesh : MonoBehaviour
{
    static int baseColorId = Shader.PropertyToID("_BaseColor");
    MaterialPropertyBlock block;

    [SerializeField]
    Mesh mesh = default(Mesh);

    [SerializeField]
    Material material = default(Material);

    Matrix4x4[] matrices = new Matrix4x4[1023];
    Vector4[] baseColors = new Vector4[1023];

    private void Awake()
    {
        for (int i = 0; i < matrices.Length; i++)
        {
            matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere * 10f, Quaternion.identity, Vector3.one);
            baseColors[i] = new Vector4(Random.value, Random.value, Random.value, 1f);
        }
    }

    private void Update()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
            block.SetVectorArray(baseColorId, baseColors);
        }

        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, block);
    }
}
