using System;
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
    const string bufferName = "Render Camera";
    CommandBuffer buffer;

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;

        // 绘制天空盒使用上面专门的方法，但其它命令需要使用 Command Buffer
        buffer = new CommandBuffer
        {
            name = bufferName,
        };

        Setup();
        DrawVisiableGeometry();
        Submit();
    }

    void DrawVisiableGeometry()
    {
        // DrawSkybox 方法仅使用相机的 clear flag，确定是否绘制天空盒
        context.DrawSkybox(camera);
    }

    void Setup()
    {
        buffer.BeginSample(bufferName);
        ExecuteCmd();
        // 设置相机的VP矩阵，其包括相机的坐标和朝向（V矩阵）、透视或正交（P矩阵）
        context.SetupCameraProperties(camera);
    }

    void Submit()
    {
        buffer.EndSample(bufferName);
        ExecuteCmd();
        context.Submit();
    }

    void ExecuteCmd()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }
}
