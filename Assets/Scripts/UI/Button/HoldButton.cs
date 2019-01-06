using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FriendlyMonster.Stencil.UI
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private bool m_IsDown;
        private bool m_IsJustDown;
        private Action<bool> m_OnDown;
        private Action m_OnUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsDown = true;
            m_IsJustDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_IsDown)
            {
                m_OnUp.Invoke();
            }

            m_IsDown = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_IsDown = false;
        }

        public void RegisterOnDown(Action<bool> onDown)
        {
            m_OnDown += onDown;
        }

        public void RegisterOnUp(Action onUp)
        {
            m_OnUp += onUp;
        }

        private void Update()
        {
            if (m_IsDown)
            {
                if (m_OnDown != null)
                {
                    m_OnDown.Invoke(m_IsJustDown);
                }

                m_IsJustDown = false;
            }
        }
    }
}