
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsVector
{
    public static Vector3 SetVectorX(Vector3 vector, float x)
    {
        vector[0] = x;
        return vector;
    }

    public static Vector3 SetVectorY(Vector3 vector, float y)
    {
        vector[1] = y;
        return vector;
    }

    public static Vector3 SetVectorZ(Vector3 vector, float z)
    {
        vector[2] = z;
        return vector;
    }

    public static Vector3 SetVectorXZ(Vector3 vector, float x, float z)
    {
        vector[0] = x;
        vector[2] = z;

        return vector;
    }


    // Set Transform Position
    // =======================================================================

    public static Vector3 SetPositionX(this Transform transform, float x)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorX(pos, x);

        return pos;
    }

    public static Vector3 SetPositionY(this Transform transform, float y)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorY(pos, y);

        return pos;
    }

    public static Vector3 SetPositionZ(this Transform transform, float z)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorZ(pos, z);

        return pos;
    }



    // Set Transform Local Position
    // =======================================================================

    public static Vector3 SetLocalPositionX(this Transform transform, float x)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorX(pos, x);

        return pos;
    }

    public static Vector3 SetLocalPositionY(this Transform transform, float y)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorY(pos, y);

        return pos;
    }

    public static Vector3 SetLocalPositionZ(this Transform transform, float z)
    {
        Vector3 pos = transform.position;
        transform.position = SetVectorZ(pos, z);

        return pos;
    }



    // Set Transform Scale
    // =======================================================================

    public static Vector3 SetScaleX(this Transform transform, float x)
    {
        Vector3 scale = transform.localScale;
        scale[0] = x;
        transform.localScale = scale;

        return scale;
    }

    public static Vector3 SetScaleY(this Transform transform, float y)
    {
        Vector3 scale = transform.localScale;
        scale[1] = y;
        transform.localScale = scale;

        return scale;
    }

    public static Vector3 SetScaleZ(this Transform transform, float z)
    {
        Vector3 scale = transform.localScale;
        scale[2] = z;
        transform.localScale = scale;

        return scale;
    }


    // Set Rotation
    // =========================================================================

    public static Quaternion LookAtMouse2D(Transform target, float offset = 0)
    {
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(target.position);
        var direction = Input.mousePosition - target.position;
        return LookAtXY(direction, offset);
    }

    public static Quaternion LookAtXY(Vector3 startPosition, Vector3 targetPosition, float offset = 0)
    {
        Vector3 direction = targetPosition - startPosition;
        return LookAtXY(direction, offset);
    }

    public static Quaternion LookAtXY(Vector3 direction, float offset = 0)
    {
        float angle = AngleLookAtDirectionXY(direction, offset);
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Quaternion LookAtXZ(Vector3 startPosition, Vector3 targetPosition, float offset = 0)
    {
        Vector3 direction = targetPosition - startPosition;
        return LookAtXZ(direction, offset);
    }

    public static Quaternion LookAtXZ(Vector3 direction, float offset = 0)
    {
        float angle = AngleLookAtDirectionXZ(direction, offset);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    public static float AngleLookAtDirectionXY(Vector3 direction, float offset = 0)
        => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offset;

    public static float AngleLookAtDirectionXZ(Vector3 direction, float offset = 0)
        => Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + offset;


    /// <summary>
    /// Returns the Direction from ObserverPos TO TargetPos
    /// </summary>
    public static Vector3 DirectionTo(Vector3 observerPos, Vector3 targetPos) => targetPos - observerPos;

    /// <summary>
    /// Returns the Direction from this Observers position TO Target Transform
    /// </summary>
    public static Vector3 DirectionTo(Transform observer, Transform target) => target.position - observer.position;

    /// <summary>
    /// Returns the Direction FROM Target Position to the Observers Position
    /// </summary>
    public static Vector3 DirectionFrom(Vector3 targetPos, Vector3 observerPos) => observerPos - targetPos;

    /// <summary>
    /// Returns the Direction FROM target Transform to the Observers Transform
    /// </summary>
    public static Vector3 DirectionFrom(Transform target, Transform observer) => observer.position - target.position;


    public static Vector3 GetMidPoint(Vector3 vectorOne, Vector3 vectorTwo)
        => (vectorOne + vectorTwo) * .5f;

    public static Vector3 GetMidPoint(Vector3[] vectorArray)
    {
        Vector3 totalPoints = Vector3.zero;
        for (int i = 0; i < vectorArray.Length; i++)
            totalPoints += vectorArray[i];

        return totalPoints / vectorArray.Length;
    }


    /// <summary>
    /// Axis is the axis to influence | ex: axis.up = horizonal
    /// </summary>
    public static Vector3 GetPerpendicular(Vector3 direction, Vector3 axis)
        => Vector3.Cross(direction, axis).normalized;

    /// <summary>
    /// dirA = direction start, dirB = direction end | ex: direction = dirB - dirA.
    /// Axis = the axis to influence | ex: axis.up = horizonal
    /// </summary>
    public static Vector3 GetPerpendicular(Vector3 dirA, Vector3 dirB, Vector3 axis)
    {
        Vector3 direction = dirB - dirA;
        return GetPerpendicular(direction, axis);
    }


    public static Vector3 GetVelocity(Vector3 currentPosition, Vector3 lastPosition)
        => (currentPosition - lastPosition) / Time.deltaTime;

    public static Vector3 GetIntersectionPoint(Vector3 observerPosition, Vector3 targetPosition, Vector3 targetVelocity, float observerSpeed)
    {
        // Calculate the distance between the ball and the enemy
        float distance = Vector3.Distance(targetPosition, observerPosition);

        // Calculate the time it will take for the enemy to reach the ball
        float timeToIntercept = distance / observerSpeed;

        // Calculate the expected ball position at the time of interception
        Vector3 ballIntersectionPosition = targetPosition + (targetVelocity * timeToIntercept);

        return ballIntersectionPosition;
    }


    public static Quaternion SmoothLookAt(Transform rotator, Vector3 direction, float lookSpeed, bool lockX = true, bool lockY = false, bool lockZ = true)
    {
        if (direction == Vector3.zero)
            return Quaternion.identity;

        Quaternion lookRot = Quaternion.LookRotation(direction);
        Quaternion targetRot = Quaternion.Slerp(rotator.rotation, lookRot, Time.deltaTime * lookSpeed);

        if (lockX) lookRot.x = 0;
        if (lockY) lookRot.y = 0;
        if (lockZ) lookRot.z = 0;

        rotator.rotation = targetRot;
        return targetRot;
    }

    public static Quaternion SmoothLookAt(Transform observer, Transform target, float lookSpeed, bool lockX = true, bool lockY = false, bool lockZ = true)
    {
        Vector3 direction = DirectionTo(observer, target);
        return SmoothLookAt(observer, direction, lookSpeed, lockX, lockY, lockZ);
    }


    /// <summary>
    /// Returns a random Vector3 on the horizontal axis, around a reference point inside a set radius
    /// </summary>
    public static Vector3 RandomInsideUnitCircleXZ(Vector3 referencePoint, float radius)
    {
        Vector2 circleRandom = Random.insideUnitCircle * radius;
        Vector3 circleRandomVector = new Vector3(circleRandom.x, 0, circleRandom.y);

        return circleRandomVector + referencePoint;
    }

    /// <summary>
    /// Returns a random Vector3 on the horizontal axis, around a reference point inside a set radius
    /// </summary>
    public static Vector3 RandomInsideUnitCircleXZ(Transform referencePoint, float radius)
    {
        Vector2 circleRandom = Random.insideUnitCircle * radius;
        Vector3 circleRandomVector = new Vector3(circleRandom.x, 0, circleRandom.y);

        return circleRandomVector + referencePoint.position;
    }

    /// <summary>
    /// Creates a smooth Curve based on provided anchor points (At least 3 points needed)
    /// </summary>
    public static Vector3[] SmoothCurve(Vector3[] arrayToCurve, float smoothness)
    {
        if (arrayToCurve.Length > 2)
        {
            List<Vector3> points;
            List<Vector3> curvedPoints;
            int pointsLength = 0;
            int curvedLength = 0;

            if (smoothness < 1.0f) smoothness = 1.0f;

            pointsLength = arrayToCurve.Length;

            curvedLength = (pointsLength * Mathf.RoundToInt(smoothness)) - 1;
            curvedPoints = new List<Vector3>(curvedLength);

            float t = 0.0f;
            for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
            {
                t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

                points = new List<Vector3>(arrayToCurve);

                for (int j = pointsLength - 1; j > 0; j--)
                {
                    for (int i = 0; i < j; i++)
                        points[i] = (1 - t) * points[i] + t * points[i + 1];
                }

                curvedPoints.Add(points[0]);
            }

            curvedPoints.Add(arrayToCurve.GetLast());
            return curvedPoints.ToArray();
        }

        else
        {
            Debug.Log("Curve requires at least 3 anchor points");
            return null;
        }
    }



    /// <summary>
    /// Creates a smooth Bezier that passes through all control points (At least 3 points needed)
    /// </summary>
    public static Vector3[] GenerateBezierCurve(Vector3[] controlPoints, float smoothness)
    {
        int curvedLength = (controlPoints.Length * Mathf.RoundToInt(smoothness)) - 1;
        Vector3[] bezierPoints = new Vector3[curvedLength];

        Vector3 A = controlPoints[0];
        Vector3 B = controlPoints[1];
        Vector3 C = controlPoints[2];

        Vector3 adjustedB = 2 * B - 0.5f * A - 0.5f * C;

        for (int i = 0; i < curvedLength; i++)
        {
            float t = (float)i / (float)(curvedLength - 1);
            Vector3 pointOnCurve = Mathf.Pow(1 - t, 2) * A + 2 * (1 - t) * t * adjustedB + Mathf.Pow(t, 2) * C;
            bezierPoints[i] = pointOnCurve;
        }

        return bezierPoints;
    }



    /// <summary>
    /// Create a point circle with designated rotation vector
    /// direction; 1 = left, -1 right
    /// </summary>
    public static Vector3[] CreatePointCircle(Vector3 centerPos, Vector3 targetRotation, int vertexNumber, float radius, int direction = -1)
    {
        Vector3[] points = new Vector3[vertexNumber];
        float angle = 2 * direction * Mathf.PI / vertexNumber;
        Vector3 initialRelativePosition = new Vector3(radius, 0, 0); // orbit.targetAngle * radius;

        Quaternion rotation = Quaternion.Euler(targetRotation);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                                     new Vector4(0, 0, 1, 0),
                                                     new Vector4(0, 0, 0, 1));

            points[i] = centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition);
            Vector3 vector = points[i];
            points[i] = m.MultiplyPoint3x4(vector);
        }

        return points;
    }

    /// <summary>
    /// Create a point circle with designated rotation vector
    /// direction; 1 = left, -1 right
    /// </summary>
    public static Vector3[] CreatePointCircleHorizontal(Vector3 centerPos, int vertexNumber, float radius, int direction = -1)
        => CreatePointCircle(centerPos, new Vector3(90, 0, 90), vertexNumber, radius, direction);

    /// <summary>
    /// Creates a point circle with adjustable angle (0 - 360)
    /// </summary>
    public static Vector3[] CreateAnglePointCircle(float startAngle, float endAngle, float radius, int resolution, Vector3 targetRotation)
    {
        List<Vector3> arcPoints = new List<Vector3>();
        float angle = startAngle;
        float arcLength = endAngle - startAngle;

        Quaternion rotation = Quaternion.Euler(targetRotation);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);

        for (int i = 0; i <= resolution; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            arcPoints.Add(new Vector3(x, y, 0));
            arcPoints[i] = m.MultiplyPoint3x4(arcPoints[i]);

            angle += (arcLength / resolution);
        }

        return arcPoints.ToArray();
    }


    public static Vector3 GetPointOnParabola(Vector3 startPosition, Vector3 direction, float speed, float gravity, float time)
    {
        Vector3 point = startPosition + (direction * speed * time);
        Vector3 gravityVector = Vector3.down * gravity * time * time;

        return point + gravityVector;
    }

    /// <summary>
    /// Returns Force needed to reach Jump Height affected by (positive) Gravity
    /// </summary>
    public static float GetJumpForce(float jumpHeight, float gravity)
        => Mathf.Sqrt(jumpHeight * -2 * -gravity);


    /// <summary>
    /// 0-X, 1-Y, 2-Z
    /// </summary>
    /// <param name="targetVector"> The vector to change </param>
    /// <param name="axisID"> The axis id of a vector </param>
    /// <returns></returns>
    public static Vector3 InverseVector3Axis(Vector3 targetVector, int axisID)
    {
        float inversedAxis = targetVector[axisID] * -1;
        targetVector[axisID] = inversedAxis;

        return targetVector;
    }


    /// <summary>
    /// Function that takes in a midpoint, a line direction, a distance, and generates a set of points evenly distributed within a line at the specified distance from the midpoint
    /// </summary>
    public static Vector3[] SpreadPointsWithinLine(Vector3 midpoint, Vector3 lineDirection, float distance, int numPoints)
    {
        Vector3[] points = new Vector3[numPoints];

        // Calculate the start point of the line
        Vector3 startPoint = midpoint - (lineDirection.normalized * (distance * (numPoints - 1) / 2));

        // Iterate through each point and calculate its position
        for (int i = 0; i < numPoints; i++)
        {
            // Calculate the position of the current point along the line
            Vector3 position = startPoint + (lineDirection.normalized * (i * distance));

            // Add the point to the array
            points[i] = position;
        }

        return points;
    }

}