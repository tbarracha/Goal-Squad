using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Grid
{
    [RequireComponent(typeof(PointGrid))]
    public class ChildPointGridRenderer : MonoBehaviour
    {
        [Header("Points")]
        [SerializeField] Transform parentGrid;
        [SerializeField] Color pointColor = Color.yellow;
        [SerializeField] float pointRadius = .2f;
        [SerializeField] bool renderPoints = true;

        Transform[] children;

        private void OnDrawGizmos()
        {
            if (renderPoints)
            {
                Gizmos.color = pointColor;

                for (int i = 0; i < children.Length; i++)
                    Gizmos.DrawSphere(children[i].position, pointRadius);
            }
        }

        private void OnValidate()
        {
            if (parentGrid != null && children == null || children.Length != parentGrid.childCount)
                children = Utilities.GetChildren(parentGrid).ToArray();
        }
    }
}