
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Detect colliders within sphere radius/bounds
    /// </summary>
    public class SphereColliderDetector : ColliderDetector
    {
        public float radius = 1;

        public override System.Collections.Generic.List<Collider> SearchForColliders()
        {
            colliders = Physics.OverlapSphere(Position, radius, contactLayer);

            return base.SearchForColliders();
        }

#if UNITY_EDITOR
        [Header("Gizmos")]
        [SerializeField] Color gizmoColor = Color.red;
        [SerializeField] bool drawGizmos;

        private void OnDrawGizmos()
        {
            if (drawGizmos == false)
                return;

            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(Position, radius);
        }

#endif
    }
}