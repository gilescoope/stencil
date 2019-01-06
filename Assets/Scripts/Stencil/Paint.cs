using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class Paint : MonoBehaviour
    {
        [SerializeField] private RenderTextureBuffer PaintRender;
        [SerializeField] private RenderTextureBuffer WallPaintRender;
        [SerializeField] private Shader PaintShader;
        [SerializeField] private Shader WashShader;

        public void SetIsWash(bool isWash)
        {
            Shader shader = isWash ? WashShader : PaintShader;
            PaintRender.GetMeshRenderer().material.shader = shader;
            PaintRender.GetMeshRenderer().material.renderQueue = 3020;
            WallPaintRender.GetMeshRenderer().material.shader = shader;
            WallPaintRender.GetMeshRenderer().material.renderQueue = 3020;
        }

        public void Render()
        {
            PaintRender.Render();
        }

        public void Clear()
        {
            PaintRender.Clear();
        }

        public void RenderWallPaint()
        {
            WallPaintRender.Render();
        }
    }
}
