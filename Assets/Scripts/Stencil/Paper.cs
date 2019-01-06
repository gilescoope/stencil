using System;
using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class Paper : MonoBehaviour
    {
        [SerializeField] private MeshRenderer Stencil;
        [SerializeField] private RenderTextureBuffer PaperFinal;
        [SerializeField] private RenderTextureBuffer PaperPreview;
        [SerializeField] private MeshRenderer PaperVisible;
        [SerializeField] private Collider Collider;

        private Action OnMove;
        private int m_TouchFingerId = -1;
        private Vector3 m_LocalTouchPosition;

        private void Awake()
        {
            PaperVisible.material.SetTexture("_MainTex", PaperPreview.GetRenderTexture());
            PaperVisible.transform.localScale = PaperPreview.GetMeshScale();
            Stencil.transform.localScale = PaperPreview.GetMeshScale();
            Collider.transform.localScale = PaperPreview.GetMeshScale();
        }

        public void RenderPreview()
        {
            PaperPreview.Render();
        }

        public void Commit()
        {
            PaperFinal.Render();
        }

        public void RegisterOnMove(Action onMove)
        {
            OnMove += onMove;
        }

        public void OnTouch(Touch touch, RaycastHit hit)
        {
            if (touch.fingerId == m_TouchFingerId)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 touchPosition = transform.InverseTransformPoint(hit.point);
                    float angle = Mathf.Atan2(touchPosition.y, touchPosition.x) - Mathf.Atan2(m_LocalTouchPosition.y, m_LocalTouchPosition.x);
                    while (Mathf.Abs(angle) > Mathf.PI)
                        angle -= Mathf.Sign(angle) * 2 * Mathf.PI;
                    angle = 2 * angle * 4 * m_LocalTouchPosition.sqrMagnitude;
                    Vector3 positionDelta = touchPosition - m_LocalTouchPosition;
                    transform.position = transform.position + transform.TransformVector(positionDelta);
                    RotateTransformAroundPivot(transform, transform.TransformPoint(m_LocalTouchPosition), transform.forward, angle * Mathf.Rad2Deg);
                    CorrectTransform();
                    if (OnMove != null)
                    {
                        OnMove.Invoke();
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    m_TouchFingerId = -1;
                }
            } else if (hit.collider == Collider)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    m_TouchFingerId = touch.fingerId;
                    m_LocalTouchPosition = transform.InverseTransformPoint(hit.point);
                }
            }
        }

        private void CorrectTransform()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            transform.rotation = Quaternion.FromToRotation(transform.forward, transform.parent.forward) * transform.rotation;
        }

        private static void RotateTransformAroundPivot(Transform transform, Vector3 pivot, Vector3 axis, float angle)
        {
            transform.rotation = Quaternion.AngleAxis(angle, axis) * transform.rotation;
            transform.position = RotatePointAroundPivot(transform.position, pivot, axis, angle);
        }

        private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 axis, float angle)
        {
            Vector3 direction = point - pivot;
            direction = Quaternion.AngleAxis(angle, axis) * direction;
            return direction + pivot;
        }

        public void SelectTexture(Texture texture, RenderTexture renderTexture)
        {
            Stencil.material.SetTexture("_MainTex", texture);
            PaperFinal.SetRenderTexture(renderTexture);
            PaperVisible.material.SetTexture("_StencilTex", texture);
        }
    }
}