using UnityEngine;
using UnityEngine.UI;

namespace FriendlyMonster.Stencil.UI
{
    public class ButtonGroup : MonoBehaviour
    {
        [SerializeField] private Button[] Buttons;

        private int m_CurrentButtonIndex;
        private bool m_IsDismissed;

        private void Awake()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                int index = i;
                Buttons[i].onClick.AddListener(() => OnClickedButton(index));
            }

            Select(0);
        }

        private void OnClickedButton(int index)
        {
            if (m_IsDismissed)
            {
                OpenMenu();
            } else
            {
                Select(index);
            }
        }

        private void OpenMenu()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (i != m_CurrentButtonIndex)
                {
                    Buttons[i].transform.SetAsFirstSibling();
                }

                Buttons[i].gameObject.SetActive(true);
            }

            m_IsDismissed = false;
        }

        private void Select(int index)
        {
            m_CurrentButtonIndex = index;
            Dismiss();
        }

        private void Dismiss()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].gameObject.SetActive(i == m_CurrentButtonIndex);
            }

            m_IsDismissed = true;
        }
    }
}