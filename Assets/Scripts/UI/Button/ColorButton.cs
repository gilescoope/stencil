using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FriendlyMonster.Stencil.UI
{
    [RequireComponent(typeof(Button))]
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private StencilManager StencilManager;
        [SerializeField] private int ColorIndex;

        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StencilManager.SelectColor(ColorIndex);
        }
    }
}