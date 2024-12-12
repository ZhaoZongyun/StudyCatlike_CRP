using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 管线资源
/// </summary>
[CreateAssetMenu(menuName ="Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        Debug.Log("调用 RenderPipelineAsset.CreatePipeline 方法");
        var crp = new CustomRenderPipeline();
        return crp;
    }
}
