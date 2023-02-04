using System.Collections.Generic;
using UnityEngine;

namespace ClownMeister.Navigation
{
    public class VehicleNodeManager : MonoBehaviour
    {
        private static readonly List<VehicleNode> nodes = new();

        public static void AddNode(VehicleNode node)
        {
            nodes.Add(node);
        }

        public static VehicleNode GetClosestNode(Vector3 position)
        {
            VehicleNode closest = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach(VehicleNode potentialTarget in nodes)
            {
                float sqrDistance = (potentialTarget.transform.position - position).sqrMagnitude;

                if (sqrDistance >= closestDistanceSqr) continue;

                closestDistanceSqr = sqrDistance;
                closest = potentialTarget;
            }

            return closest;
        }
    }
}