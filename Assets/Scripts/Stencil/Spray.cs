using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class Spray : MonoBehaviour
    {
        [SerializeField] private SprayMesh SprayMesh;
        [SerializeField] private SprayMesh WashMesh;
        [SerializeField] private Transform Nozzle;
        [SerializeField] private Color[] Colors;

        private void Awake()
        {
            SprayMesh.SetTrackedTransform(Nozzle);
            WashMesh.SetTrackedTransform(Nozzle);
        }

        public void SelectWash()
        {
            SprayMesh.gameObject.SetActive(false);
            WashMesh.gameObject.SetActive(true);
        }

        public void SelectColor(int colorIndex)
        {
            SprayMesh.gameObject.SetActive(true);
            WashMesh.gameObject.SetActive(false);

            SprayMesh.GetMaterial().color = Colors[colorIndex];
        }
    }
}