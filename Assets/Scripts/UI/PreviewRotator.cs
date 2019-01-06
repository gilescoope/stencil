using UnityEngine;

namespace FriendlyMonster.Stencil.UI
{
    public class PreviewRotator : MonoBehaviour
    {
        private const float OffSpeed = 15;
        private const float OnSpeed = 45;

        private float m_Rotation;
        private bool m_IsOn;

        private void Update()
        {
            float RotationSpeed = m_IsOn ? OnSpeed : OffSpeed;
            m_Rotation += RotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(m_Rotation * Vector3.back);
        }

        public void SetOn()
        {
            m_IsOn = true;
        }

        public void SetOff()
        {
            m_IsOn = false;
        }
    }
}
