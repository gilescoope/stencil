using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class SprayMesh : MonoBehaviour
    {
        [SerializeField] private float SprayAngle = 0.05f;

        private Material m_Material;
        private Transform m_TrackedTransform;

        private void Awake()
        {
            m_Material = GetComponent<MeshRenderer>().material;
        }

        public void SetTrackedTransform(Transform transform)
        {
            m_TrackedTransform = transform;
        }

        public Material GetMaterial()
        {
            return m_Material;
        }

        private void Update()
        {
            m_Material.SetVector("_NozzlePosition", m_TrackedTransform.position);
            m_Material.SetVector("_NozzleDirection", m_TrackedTransform.forward);
            m_Material.SetFloat("_NozzleAngle", SprayAngle);
            m_Material.SetFloat("_RandomSeed", Random.value);
            m_Material.color = new Color(m_Material.color.r, m_Material.color.g, m_Material.color.b, 0.12f * 60 * Time.deltaTime);
        }
    }
}