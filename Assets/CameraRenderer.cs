using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    const string bufferName = "MyBuffer";
    CommandBuffer buffer;
    CullingResults cullingResults;
    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
    static ShaderTagId[] legacyShaderTagIds = {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM"),
    };

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;

        // 绘制天空盒使用上面专门的方法，但其它命令需要使用 Command Buffer
        buffer = new CommandBuffer
        {
            name = bufferName,
        };

        if (!Cull())
        {
            return;
        }

        Setup();
        DrawVisiableGeometry();
        DrawUnsupportShaders();
        Submit();
    }

    void Setup()
    {
        // 设置相机的VP矩阵，其包括相机的坐标和朝向（V矩阵）、透视或正交（P矩阵）
        context.SetupCameraProperties(camera);
        buffer.ClearRenderTarget(true, true, Color.clear);

        buffer.BeginSample(bufferName);
        ExecuteBuffer();
    }

    void DrawVisiableGeometry()
    {
        //用于确定是正交排序还是基于距离的排序
        SortingSettings sortingSettings = new SortingSettings(camera)
        {
            // 设值 SortingSetting 的 criteria 属性，强制指定渲染顺序
            criteria = SortingCriteria.CommonOpaque
        };
        DrawingSettings drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
        FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.opaque);

        // 先绘制 opaque
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

        // 再绘制 Skybox，该方法仅使用相机的 clear flag，确定是否绘制天空盒
        context.DrawSkybox(camera);

        // 再绘制 transparent
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }

    void DrawUnsupportShaders()
    {
        var drawSettings = new DrawingSettings(legacyShaderTagIds[0], new SortingSettings(camera));
        for (int i = 1; i < legacyShaderTagIds.Length; i++)
        {
            drawSettings.SetShaderPassName(i, legacyShaderTagIds[i]);
        }

        var filteringSettings = FilteringSettings.defaultValue;
        context.DrawRenderers(cullingResults, ref drawSettings, ref filteringSettings);
    }

    bool Cull()
    {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }

        Debug.Log("裁剪失败");
        return false;
    }

    void Submit()
    {
        buffer.EndSample(bufferName);
        ExecuteBuffer();
        context.Submit();
    }

    void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }
}
