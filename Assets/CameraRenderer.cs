using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 相机渲染器
///     大致相当于 URP 的可编程渲染器
/// </summary>
public class CameraRenderer
{
    ScriptableRenderContext context;
    Camera camera;

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;
    }

    void DrawVisiableGeometry()
    {
        context.DrawSkybox(camera);
    }
}
