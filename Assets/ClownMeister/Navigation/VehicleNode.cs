using System;
using ClownMeister.Util;
using UnityEngine;

namespace ClownMeister.Navigation
{
    public class VehicleNode : MonoBehaviour
    {
        public VehicleNode nextNode = null;

        private void Awake()
        {
            VehicleNodeManager.AddNode(this);
        }

        private void Start()
        {
            if (this.nextNode == null) this.nextNode = VehicleNodeManager.GetClosestNode(transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.17f, 0.75f, 0f, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));

            if (this.nextNode == null) return;

            Gizmos.color = new Color(0.95f, 1f, 0.05f);
            GizmoUtils.DrawArrow(transform.position, this.nextNode.transform, arrowHeadLength: 2);
        }
    }
}
