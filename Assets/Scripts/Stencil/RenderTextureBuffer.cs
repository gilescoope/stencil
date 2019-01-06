using UnityEngine;
using UnityEngine.Rendering;

namespace FriendlyMonster.Stencil
{
    public class RenderTextureBuffer : MonoBehaviour
    {
        private const float RenderScale = 1 / 512f;

        [SerializeField] private RenderTextureSettings RenderTextureSettings;

        [SerializeField] private Mesh QuadMesh;
        [SerializeField] private Shader MeshShader;
        [SerializeField] private int MeshRenderQueue;
        [SerializeField] private Shader RenderTextureShader;

        [SerializeField] private LayerMask CameraLayerMask;

        private GameObject m_MeshObject;
        private MeshFilter m_MeshFilter;
        private MeshRenderer m_MeshRenderer;

        [SerializeField] private bool IsPersist;

        private RenderTexture[] m_RenderTextures;

        private GameObject m_CameraObject;
        private Camera m_Camera;

        public MeshRenderer GetMeshRenderer()
        {
            return m_MeshRenderer;
        }

        public Vector3 GetMeshScale()
        {
            return new Vector3(RenderTextureSettings.Width * RenderScale, RenderTextureSettings.Height * RenderScale, 1);
        }

        public void Awake()
        {
            m_RenderTextures = new RenderTexture[IsPersist ? 2 : 1];
            for (int i = 0; i < m_RenderTextures.Length; i++)
            {
                m_RenderTextures[i] = RenderTextureSettings.GetRenderTexture();
            }

            m_MeshObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
            m_MeshObject.transform.parent = transform;
            m_MeshObject.layer = gameObject.layer;
            m_MeshObject.transform.localScale = GetMeshScale();
            m_MeshFilter = m_MeshObject.GetComponent<MeshFilter>();
            m_MeshFilter.mesh = QuadMesh;
            m_MeshRenderer = m_MeshObject.GetComponent<MeshRenderer>();
            m_MeshRenderer.material = new Material(MeshShader);
            m_MeshRenderer.material.renderQueue = MeshRenderQueue;

            m_CameraObject = new GameObject("Camera", typeof(Camera));
            m_CameraObject.transform.parent = transform;
            m_Camera = m_CameraObject.GetComponent<Camera>();
            m_Camera.transform.localPosition = Vector3.back;
            m_Camera.clearFlags = CameraClearFlags.SolidColor;
            m_Camera.backgroundColor = Color.clear;
            m_Camera.cullingMask = 0;
            m_Camera.orthographic = true;
            m_Camera.orthographicSize = 0.5f * RenderTextureSettings.Height * RenderScale;
            m_Camera.nearClipPlane = 0.1f;
            m_Camera.farClipPlane = 2;
            m_Camera.cullingMask = CameraLayerMask;
            m_CameraObject.SetActive(false);

            Clear();
            SetRenderTexture(m_RenderTextures[0]);
        }

        public void Clear()
        {
            RenderTexture rt = RenderTexture.active;
            RenderTexture.active = m_RenderTextures[0];
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = rt;
        }

        public void Render()
        {
            if (IsPersist)
            {
                m_RenderTextures[1].DiscardContents();
                Graphics.Blit(m_RenderTextures[0], m_RenderTextures[1]);
                m_Camera.RemoveAllCommandBuffers();
                CommandBuffer commandBuffer = new CommandBuffer();
                Material material = new Material(RenderTextureShader);
                commandBuffer.SetGlobalTexture("_MainTex", m_RenderTextures[1]);
                commandBuffer.Blit(m_RenderTextures[1], m_RenderTextures[0], material, 0);
                m_Camera.AddCommandBuffer(CameraEvent.BeforeForwardOpaque, commandBuffer);
                m_Camera.Render();
            } else
            {
                Clear();
                m_Camera.Render();
            }
        }

        public RenderTexture GetRenderTexture()
        {
            return m_RenderTextures[0];
        }

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            m_RenderTextures[0] = renderTexture;
            m_Camera.targetTexture = m_RenderTextures[0];
            m_MeshRenderer.material.mainTexture = m_RenderTextures[0];
        }
    }
}