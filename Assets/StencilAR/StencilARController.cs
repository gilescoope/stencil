using System.Collections.Generic;
using FriendlyMonster.Stencil.UI;
using GoogleARCore;
using UnityEngine;

namespace FriendlyMonster.Stencil
{
    public class StencilARController : MonoBehaviour
    {
        [SerializeField] private StencilUI StencilUI;
        [SerializeField] private StencilManager StencilManager;

        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
        private DetectedPlane m_TrackedPlane;

        private void Awake()
        {
            SetSearchingForPlane(true);
        }

        private void Update()
        {
            if (m_TrackedPlane == null)
            {
                Session.GetTrackables(m_AllPlanes);
                for (int i = 0; i < m_AllPlanes.Count; i++)
                {
                    if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                    {
                        OnFoundTrackedPlane(m_AllPlanes[i]);
                        break;
                    }
                }
            }
        }

        private void OnFoundTrackedPlane(DetectedPlane detectedPlane)
        {
            m_TrackedPlane = detectedPlane;
            SetSearchingForPlane(false);
            SetAnchor(m_TrackedPlane, m_TrackedPlane.CenterPose);
        }

        private void SetAnchor(Trackable trackable, Pose pose)
        {
            Anchor anchor = trackable.CreateAnchor(pose);
            StencilManager.transform.position = pose.position;
            StencilManager.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(pose.up.x, pose.up.z), 0);
            StencilManager.transform.parent = anchor.transform;
        }

        private void SetSearchingForPlane(bool isSearching)
        {
            StencilUI.SetSearchingForPlanes(isSearching);
            StencilManager.SetSearchingForPlanes(isSearching);
        }
    }
}