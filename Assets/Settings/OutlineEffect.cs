using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OutlineEffect : ScriptableRendererFeature
{
    class OutlineRenderPass : ScriptableRenderPass
    {
        public List<Material> outlineMaterials;

        public OutlineRenderPass(List<Material> materials)
        {
            this.outlineMaterials = materials;
            renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }

        // This method is called before executing the render pass..
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("OutlineRenderPass");

            // Set the render target to the camera's depth buffer
            cmd.SetRenderTarget(renderingData.cameraData.renderer.cameraDepthTargetHandle);

            // Clear the depth buffer to ensure the outline is rendered correctly
            cmd.ClearRenderTarget(false, true, Color.clear);

            var settings = new DrawingSettings(new ShaderTagId("UniversalForward"), new SortingSettings(renderingData.cameraData.camera));
            var filterSettings = new FilteringSettings(RenderQueueRange.opaque);
            context.DrawRenderers(renderingData.cullResults, ref settings, ref filterSettings);

            // Draw objects with outline materials
            // Draw objects with outline materials
            foreach (Material material in outlineMaterials)
            {
                var drawSettings = new DrawingSettings(new ShaderTagId("Outline"), new SortingSettings(renderingData.cameraData.camera))
                {
                    overrideMaterial = material
                };
                var filterSettingsOutline = new FilteringSettings(RenderQueueRange.opaque);
                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettingsOutline);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }

    OutlineRenderPass outlinePass;
    public List<Material> outlineMaterials;

    /// <inheritdoc/>
    public override void Create()
    {
        outlinePass = new OutlineRenderPass(outlineMaterials);
        outlinePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(outlinePass);
    }
}


