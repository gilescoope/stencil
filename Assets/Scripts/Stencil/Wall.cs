using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private RenderTextureBuffer WallFinal;
        [SerializeField] private RenderTextureBuffer WallPreview;

        public void RenderPreview()
        {
            WallPreview.Render();
        }

        public void Commit()
        {
            WallFinal.Render();
        }
    }
}
