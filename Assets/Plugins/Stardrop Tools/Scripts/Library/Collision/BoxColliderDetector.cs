
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Detect colliders within box dimensions/bounds
    /// </summary>
    public class BoxColliderDetector : ColliderDetector
    {
        public Vector3 boxDimensions = Vector3.one;
        [SerializeField] bool uniform;

        public override System.Collections.Generic.List<Collider> SearchForColliders()
        {
            colliders = Physics.OverlapBox(Position, boxDimensions * .5f, Rotation, contactLayer);

            return base.SearchForColliders();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (uniform)
            {
                for (int i = 0; i < 3; i++)
                    boxDimensions[i] = boxDimensions[0];
            }
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
            Gizmos.DrawWireCube(Position, boxDimensions);
        }

#endif
    }
}