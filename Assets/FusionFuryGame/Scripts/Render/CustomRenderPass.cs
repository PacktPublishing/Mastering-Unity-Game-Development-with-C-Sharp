using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPass : ScriptableRenderPass
{
    public Material material; // Material to use for rendering

    public CustomRenderPass(Material material)
    {
        this.material = material;
        renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var cmd = CommandBufferPool.Get("CustomRenderPass");
        cmd.ClearRenderTarget(true, true, Color.clear); // Clear the render target

        // Set up rendering settings
        var sortingCriteria = SortingCriteria.CommonOpaque;
        var drawSettings = CreateDrawingSettings(new ShaderTagId("UniversalForward"), ref renderingData, sortingCriteria);

        // Apply custom material
        var renderer = renderingData.cameraData.camera.GetComponent<Renderer>();
        if (renderer != null && material != null)
        {
            cmd.DrawRenderer(renderer, material);
        }

        // Execute the command buffer
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
