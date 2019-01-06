using System.Collections.Generic;
using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public static class MouseInput
    {
        public static List<Touch> Touches = new List<Touch>();
        private static Vector3 m_LastPosition;
        private static bool m_LastIsTouched;

        public static void Update()
        {
            Touches.Clear();
            if (m_LastIsTouched)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 deltaPosition = Input.mousePosition - m_LastPosition;
                    if (deltaPosition != Vector2.zero)
                    {
                        Touches.Add(new Touch {position = Input.mousePosition, phase = TouchPhase.Moved, deltaPosition = deltaPosition});
                    } else
                    {
                        Touches.Add(new Touch {position = Input.mousePosition, phase = TouchPhase.Stationary});
                    }
                } else
                {
                    Touches.Add(new Touch {position = Input.mousePosition, phase = TouchPhase.Ended});
                }
            } else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Touches.Add(new Touch {position = Input.mousePosition, phase = TouchPhase.Began});
                }
            }

            m_LastPosition = Input.mousePosition;
            m_LastIsTouched = Input.GetMouseButton(0);
        }
    }
}
