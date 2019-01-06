using UnityEngine;
using UnityEngine.UI;

namespace FriendlyMonster.Stencil.UI
{
    public class StencilUI : MonoBehaviour
    {
        [SerializeField] private StencilManager StencilManager;
        [SerializeField] private GameObject SearchingPanel;
        [SerializeField] private GameObject StencilControls;
        [SerializeField] private GameObject VisibilityButton;
        [SerializeField] private Text SprayButtonText;

        private bool IsControlsHidden;
        private bool IsSearching;

        private static string SprayText = "Spray";
        private static string WashText = "Wash";

        public void SetSearchingForPlanes(bool isSearching)
        {
            IsSearching = isSearching;
            UpdateActive();
        }

        public void ToggleControlsHidden()
        {
            IsControlsHidden = !IsControlsHidden;
            UpdateActive();
        }

        private void UpdateActive()
        {
            SearchingPanel.SetActive(IsSearching);
            StencilControls.SetActive(!IsSearching && !IsControlsHidden);
            VisibilityButton.SetActive(!IsSearching);
            StencilManager.SetHidden(IsControlsHidden);
        }

        public void SetIsWash(bool isWash)
        {
            SprayButtonText.text = isWash ? WashText : SprayText;
        }
    }
}
