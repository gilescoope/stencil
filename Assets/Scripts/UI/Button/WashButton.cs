using UnityEngine;
using UnityEngine.UI;

namespace FriendlyMonster.Stencil.UI
{
    public class WashButton : MonoBehaviour
    {
        public StencilManager StencilManager;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StencilManager.SelectWash();
        }
    }
}