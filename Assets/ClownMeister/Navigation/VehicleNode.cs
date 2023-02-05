using System;
using System.Collections.Generic;
using ClownMeister.Util;
using UnityEngine;

namespace ClownMeister.Navigation
{
    public class VehicleNode : MonoBehaviour
    {
        public bool connectAutomatically = true;
        public VehicleNodeType nodeType = VehicleNodeType.Normal;
        public List<VehicleNode> traversableNeighbours = new();

        private static readonly Color NormalColor = new(0.17f, 0.75f, 0f, 0.5f);
        private static readonly Color DespawnColor = new(1, 0, 0, 0.5f);
        private static readonly Color SpawnColor = new(0, 0, 1, 0.5f);

        public float speedModifier = 1;

        private void Awake()
        {
            VehicleNodeManager.AddNode(this);
        }

        private void Start()
        {
            if (this.connectAutomatically) this.traversableNeighbours.Add(VehicleNodeManager.GetClosestNode(this));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = this.nodeType switch
            {
                VehicleNodeType.Normal => NormalColor,
                VehicleNodeType.Start => SpawnColor,
                VehicleNodeType.End => DespawnColor,
                _ => throw new ArgumentOutOfRangeException()
            };

            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));

            if (this.traversableNeighbours == null) return;

            Gizmos.color = new Color(0.95f, 1f, 0.05f);
            foreach (VehicleNode traversableNeighbour in this.traversableNeighbours) {
                GizmoUtils.DrawArrow(transform.position, traversableNeighbour.transform, arrowHeadLength: 2);
            }
        }
    }
}
