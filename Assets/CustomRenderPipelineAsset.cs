using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// ������Դ
/// </summary>
[CreateAssetMenu(menuName ="Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        var crp = new CustomRenderPipeline();
        return crp;
    }
}
