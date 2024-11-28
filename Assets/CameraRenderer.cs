using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// �����Ⱦ��
///     �����൱�� URP �Ŀɱ����Ⱦ��
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
