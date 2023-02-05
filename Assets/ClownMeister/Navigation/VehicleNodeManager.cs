using System.Collections.Generic;
using UnityEngine;

namespace ClownMeister.Navigation
{
    public static class VehicleNodeManager
    {
        private static readonly List<VehicleNode> Nodes = new();

        public static void AddNode(VehicleNode node)
        {
            Nodes.Add(node);
        }

        public static VehicleNode GetClosestNode(VehicleNode node)
        {
            VehicleNode closest = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach(VehicleNode potentialNode in Nodes)
            {
                if (node == potentialNode) continue;

                float sqrDistance = (potentialNode.transform.position - node.transform.position).sqrMagnitude;

                if (sqrDistance >= closestDistanceSqr) continue;

                closestDistanceSqr = sqrDistance;
                closest = potentialNode;
            }

            return closest;
        }

        public static VehicleNode GetClosestNode(Vector3 position)
        {
            VehicleNode closest = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach(VehicleNode potentialNode in Nodes)
            {
                float sqrDistance = (potentialNode.transform.position - position).sqrMagnitude;

                if (sqrDistance >= closestDistanceSqr) continue;

                closestDistanceSqr = sqrDistance;
                closest = potentialNode;
            }

            return closest;
        }
    }
}