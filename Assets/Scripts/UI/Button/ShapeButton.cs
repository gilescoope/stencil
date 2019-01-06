using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FriendlyMonster.Stencil.UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class ShapeButton : MonoBehaviour
    {
        [SerializeField] private StencilManager StencilManager;
        [SerializeField] private int TextureIndex;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StencilManager.SelectTexture(TextureIndex);
        }
    }
}
