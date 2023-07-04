
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Grid
{
    /// <summary>
    /// Grid made of single points, seperated by a certain distance, or made to fit inside certain bounds
    /// </summary>
    public class PointGrid : MonoBehaviour
    {
        [Header("Generate")]
        [SerializeField] bool validate;
        
        [Header("Grid Parameters")]
        [SerializeField] GridType gridType = GridType.XZ;
        [SerializeField] GridTransformRelation transformRelation = GridTransformRelation.TransformIsCenter;
        [SerializeField] GridFit gridFit = GridFit.Free;
        [Space]
        [SerializeField] Vector2Int iterations =     new Vector2Int(5, 5);
        [SerializeField] Vector2 distances =         new Vector2(1, 1);

        [NaughtyAttributes.ShowIf("showBounds")]
        [SerializeField] Vector2 bounds =            new Vector2(2, 2);
        
        [Header("Points")]
        [SerializeField] List<Vector3> gridPoints =  new List<Vector3>();
        [SerializeField] List<Vector3> gridCorners = new List<Vector3>();

        Vector3 gridOrigin;
        bool showBounds;

        float xDimensions;
        float yDimensions;
        float zDimensions;

        public int GridPointCount => gridPoints.Count;

        public List<Vector3> GridPoints =>  gridPoints;
        public List<Vector3> GridCorners => gridCorners;

        public Vector3 CurrentPosition => transform.position;

        public Vector3 GetRandomPoint() => gridPoints.GetRandom();

        public Vector3 GetGridPointByIndex(int index) => gridPoints[index];


        public List<Vector3> GetRandomPoints(int amount, bool addCurrentGridPosition = false)
        {
            var positions = gridPoints.GetRandomNonRepeat(amount);

            // Add current position, to get Positions in relation to grid world position
            if (addCurrentGridPosition)
                for (int i = 0; i < positions.Count; i++)
                    positions[i] += CurrentPosition;

            return positions;
        }


        public Vector3 GetRandomCorner()
        {
            // Add current position, to get Corner Position in relation to grid world position
            Vector3 corner = gridCorners.GetRandom() + CurrentPosition;
            return corner;
        }



        public List<Vector3> GenerateGrid(Vector2Int iterations, Vector2 distances)
        {
            this.iterations = iterations;
            this.distances = distances;

            GenerateGrid();

            return gridPoints;
        }


        // 1) Get Current position
        // 2) Get Grid Type Multiplier : this way we filter grid axis with less comparisons
        Vector3 GetGridOrigin()
        {
            gridOrigin = CurrentPosition;

            xDimensions = distances.x * (iterations.x - 1);
            yDimensions = distances.y * (iterations.y - 1);
            zDimensions = yDimensions;

            if (transformRelation == GridTransformRelation.TransformIsCenter)
            {
                switch (gridType)
                {
                    case GridType.XZ:

                        // Half of inversed hipotenuse vector
                        Vector3 cornerUpRightXZ = CurrentPosition + new Vector3(xDimensions, CurrentPosition.y, zDimensions);

                        Vector3 hipotenuseVectorXZ = cornerUpRightXZ - CurrentPosition;
                        gridOrigin = CurrentPosition + -hipotenuseVectorXZ * .5f;

                        break;


                    case GridType.XY:

                        Vector3 cornerUpRightXY = CurrentPosition + new Vector3(xDimensions, yDimensions, CurrentPosition.z);

                        Vector3 hipotenuseVectorXY = cornerUpRightXY - CurrentPosition;
                        gridOrigin = CurrentPosition + -hipotenuseVectorXY * .5f;

                        break;


                    case GridType.YZ:

                        Vector3 cornerUpRightYZ = CurrentPosition + new Vector3(CurrentPosition.x, yDimensions, zDimensions);

                        Vector3 hipotenuseVectorYZ = cornerUpRightYZ - CurrentPosition;
                        gridOrigin = CurrentPosition + -hipotenuseVectorYZ * .5f;

                        break;
                }
            }

            Vector3 gridTypeMultiplier = GetGridTypeMultiplier();

            gridOrigin.x *= gridTypeMultiplier.x;
            gridOrigin.y *= gridTypeMultiplier.y;
            gridOrigin.z *= gridTypeMultiplier.z;

            return gridOrigin;
        }



        Vector3 GetGridTypeMultiplier()
        {
            switch (gridType)
            {
                case GridType.XZ:
                    return new Vector3(1, 0, 1);

                case GridType.XY:
                    return new Vector3(1, 1, 0);

                case GridType.YZ:
                    return new Vector3(0, 1, 1);

                default:  // XYZ
                    return Vector3.one;
            }
        }

        // 1) Get Grid Origin
        // 2) Generate Points
        [NaughtyAttributes.Button("Generate Grid")]
        public void GenerateGrid() //List<Vector3>
        {
            GetGridOrigin();

            if (gridFit == GridFit.FitInsideBounds)
            {
                distances.x = bounds.x / iterations.x;
                distances.y = bounds.y / iterations.y;
            }

            switch (gridType)
            {
                case GridType.XZ:
                    gridOrigin.y = CurrentPosition.y;
                    GeneratePointGridXZ(gridOrigin);
                    break;

                case GridType.XY:
                    gridOrigin.z = CurrentPosition.z;
                    GeneratePointGridXY(gridOrigin);
                    break;

                case GridType.YZ:
                    gridOrigin.x = CurrentPosition.x;
                    GeneratePointGridYZ(gridOrigin);
                    break;
            }
        }

        /// <summary>
        /// Generate a grid, starting at the Origin Position
        /// </summary>
        public List<Vector3> GeneratePointGridXZ(Vector3 originPosition)
        {
            gridPoints = new List<Vector3>();
            Vector3 point = originPosition;

            for (int z = 0; z < iterations.y; z++)
            {
                for (int x = 0; x < iterations.x; x++)
                {
                    gridPoints.Add(point);
                    point.x += distances.x;
                }

                point.x = originPosition.x;
                point.z += distances.y;
            }


            GetCorners();
            return gridPoints;
        }


        public List<Vector3> GeneratePointGridXY(Vector3 originPosition)
        {
            gridPoints = new List<Vector3>();
            Vector3 point = originPosition;

            for (int y = 0; y < iterations.y; y++)
            {
                for (int x = 0; x < iterations.x; x++)
                {
                    gridPoints.Add(point);
                    point.x += distances.x;
                }

                point.x = originPosition.x;
                point.y += distances.y;
            }


            GetCorners();
            return gridPoints;
        }


        public List<Vector3> GeneratePointGridYZ(Vector3 originPosition)
        {
            gridPoints = new List<Vector3>();
            Vector3 point = originPosition;

            for (int y = 0; y < iterations.x; y++)
            {
                for (int z = 0; z < iterations.y; z++)
                {
                    gridPoints.Add(point);
                    point.z += distances.x;
                }

                point.z = originPosition.z;
                point.y += distances.y;
            }


            GetCorners();
            return gridPoints;
        }

        void GetCorners()
        {
            if (gridPoints.Count < 1)
                return;

            gridCorners = new List<Vector3>();
            Vector3 first = gridPoints[0];

            switch (gridType)
            {
                case GridType.XZ:
                    gridCorners.Add(first);                                                                         // Lower Left
                    gridCorners.Add(new Vector3(first.x + xDimensions, CurrentPosition.y, first.z));                // Lower Right
                    gridCorners.Add(new Vector3(first.x, CurrentPosition.y, first.z + zDimensions));                // Upper Left
                    gridCorners.Add(new Vector3(first.x + xDimensions, CurrentPosition.y, first.z + zDimensions));  // Upper Right
                    break;


                case GridType.XY:
                    gridCorners.Add(first);                                                                         // Lower Left
                    gridCorners.Add(new Vector3(first.x + xDimensions, first.y, CurrentPosition.z));                // Lower Right
                    gridCorners.Add(new Vector3(first.x, first.y + yDimensions, CurrentPosition.z));                // Upper Left
                    gridCorners.Add(new Vector3(first.x + xDimensions, first.y + yDimensions, CurrentPosition.z));  // Upper Right
                    break;


                case GridType.YZ:
                    gridCorners.Add(first);                                                                         // Lower Left
                    gridCorners.Add(new Vector3(first.x, first.y, first.z + zDimensions));                          // Lower Right
                    gridCorners.Add(new Vector3(first.x, first.y + yDimensions, first.z));                          // Upper Left
                    gridCorners.Add(new Vector3(first.x, first.y + yDimensions, first.z + zDimensions));            // Upper Right
                    break;
            }
        }

        void RefreshShowBounds()
        {
            if (gridFit == GridFit.FitInsideBounds)
                showBounds = true;
            else
                showBounds = false;
        }

        public void CreateGridTransformPoints(Transform parent)
        {
            if (parent == null && gridPoints.Exists() == false)
                return;

            for (int i = 0; i < gridPoints.Count; i++)
                Utilities.CreateEmpty("Grid Point - " + i, gridPoints[i], parent);
        }

        [NaughtyAttributes.Button("Create Grid Points")]
        void CreateGridTransformPoints()
        {
            Transform parent = transform;
            CreateGridTransformPoints(parent);
        }

        private void OnValidate()
        {
            RefreshShowBounds();

            if (validate)
                GenerateGrid();
        }
    }
}