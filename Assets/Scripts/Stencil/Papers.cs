using System;
using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class Papers : MonoBehaviour
    {
        [SerializeField] private Paper Paper;
        [SerializeField] private Texture[] Textures;

        private RenderTexture[] m_RenderTextures;

        private void Awake()
        {
            m_RenderTextures = new RenderTexture[Textures.Length];
            for (int i = 0; i < m_RenderTextures.Length; i++)
            {
                m_RenderTextures[i] = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            }
        }

        public void RegisterOnMove(Action commitPaint)
        {
            Paper.RegisterOnMove(commitPaint);
        }

        public void OnTouch(Touch touch, RaycastHit hit)
        {
            Paper.OnTouch(touch, hit);
        }

        public void RenderPreview()
        {
            Paper.RenderPreview();
        }

        public void Commit()
        {
            Paper.Commit();
        }

        public void SelectTexture(int textureIndex)
        {
            Paper.SelectTexture(Textures[textureIndex], m_RenderTextures[textureIndex]);
        }
    }
}
