using FriendlyMonster.Stencil.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FriendlyMonster.Stencil
{
    public class StencilManager : MonoBehaviour
    {
        [SerializeField] private Wall Wall;
        [SerializeField] private Spray Spray;
        [SerializeField] private Papers Papers;
        [SerializeField] private Paint Paint;

        [SerializeField] private Camera ARCamera;
        [SerializeField] private EventSystem EventSystem;
        [SerializeField] private StencilUI StencilUI;

        private bool m_IsUncommittedPaint;
        private bool m_IsWash;

        private static int PaperLayerMask;
        private static int WallLayerMask;

        private void Awake()
        {
            Papers.RegisterOnMove(CommitPaint);
            PaperLayerMask = 1 << LayerMask.NameToLayer("Paper");
            WallLayerMask = 1 << LayerMask.NameToLayer("Wall");
        }

        public void SetSearchingForPlanes(bool isSearching)
        {
            Wall.gameObject.SetActive(!isSearching);
            Spray.gameObject.SetActive(!isSearching);
            Papers.gameObject.SetActive(!isSearching);
            Paint.gameObject.SetActive(!isSearching);
        }

        private void Start()
        {
            SelectTexture(0);
            SelectColor(0);
        }

        private void Update()
        {
            MouseInput.Update();
            if (MouseInput.Touches.Count > 0)
                ProcessTouch(MouseInput.Touches[0]);
            if (Input.touchCount > 0)
                ProcessTouch(Input.GetTouch(0));
        }

        private void ProcessTouch(Touch touch)
        {
            if (touch.phase == TouchPhase.Began && EventSystem.IsPointerOverGameObject(touch.fingerId))
                return;

            Ray ray = ARCamera.ScreenPointToRay(touch.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, PaperLayerMask))
            {
                Papers.OnTouch(touch, hit);
            } else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, WallLayerMask))
                {
                    Papers.OnTouch(touch, hit);
                }
            }
        }

        public void SetHidden(bool isHidden)
        {
            Papers.gameObject.SetActive(!isHidden);
        }

        private void CommitPaint()
        {
            if (m_IsUncommittedPaint)
            {
                Papers.Commit();
                Wall.Commit();
                Paint.Clear();
                Paint.RenderWallPaint();
                Wall.RenderPreview();
                Papers.RenderPreview();
                m_IsUncommittedPaint = false;
            }
        }

        public void PressSpray(bool isJustPressed)
        {
            m_IsUncommittedPaint = true;
            Paint.Render();
            Paint.SetIsWash(false);
            Paint.RenderWallPaint();
            Paint.SetIsWash(m_IsWash);
            Papers.RenderPreview();
            Wall.RenderPreview();
        }

        public void SelectTexture(int textureIndex)
        {
            CommitPaint();
            Papers.SelectTexture(textureIndex);
            Papers.RenderPreview();
        }

        public void SelectWash()
        {
            CommitPaint();
            m_IsWash = true;
            Spray.SelectWash();
            StencilUI.SetIsWash(true);
        }

        public void SelectColor(int colorIndex)
        {
            CommitPaint();
            m_IsWash = false;
            Spray.SelectColor(colorIndex);
            StencilUI.SetIsWash(false);
        }
    }
}