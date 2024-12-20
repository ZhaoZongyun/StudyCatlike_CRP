using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 管线
/// </summary>
public class CustomRenderPipeline : RenderPipeline
{
    public CustomRenderPipeline()
    {
        GraphicsSettings.useScriptableRenderPipelineBatching = true;
    }

    CameraRenderer renderer = new CameraRenderer();

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {

    }

    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        base.Render(context, cameras);

        for (int i = 0; i < cameras.Count; i++)
        {
            //Debug.Log(cameras[i].gameObject.name);
            renderer.Render(context, cameras[i]);
        }
    }
}
