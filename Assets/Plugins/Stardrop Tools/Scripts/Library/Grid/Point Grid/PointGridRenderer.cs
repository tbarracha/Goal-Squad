using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Grid
{
    [RequireComponent(typeof(PointGrid))]
    public class PointGridRenderer : MonoBehaviour
    {
        [Header("Points")]
        [SerializeField] PointGrid grid;
        [SerializeField] Color pointColor = Color.yellow;
        [SerializeField] float pointRadius = .2f;
        [SerializeField] bool renderPoints = true;

        [Header("Helpers")]
        [SerializeField] Color cornerColor = Color.red;
        [SerializeField] float cornerRadius = .3f;
        [SerializeField] bool renderCorners;

        private void OnDrawGizmos()
        {
            if (renderPoints)
            {
                Gizmos.color = pointColor;

                for (int i = 0; i < grid.GridPoints.Count; i++)
                    Gizmos.DrawSphere(grid.GridPoints[i], pointRadius);
            }

            if (renderCorners)
            {
                Gizmos.color = cornerColor;

                for (int i = 0; i < grid.GridCorners.Count; i++)
                    Gizmos.DrawSphere(grid.GridCorners[i], cornerRadius);
            }
        }


        private void OnValidate()
        {
            if (grid == null)
                grid = GetComponent<PointGrid>();
        }
    }
}