
using UnityEngine;

namespace StardropTools
{
    public class VelocityCalculator : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 velocity;
        Vector3 prevPosition;

        public Vector3 Velocity => velocity;

        public void SetTarget(Transform target) => this.target = target;

        public Vector3 CalculateVelocity()
        {
            if (target == null)
                return Vector3.zero;

            velocity = (target.position - prevPosition) / Time.deltaTime;
            prevPosition = target.position;
            return velocity;
        }

        // Return the velocity with its magnitude clamped by an input value
        public Vector3 GetClampedVelocity(float maxMagnitude)
        {
            Vector3 velocityClamped = CalculateVelocity();
            if (velocityClamped.magnitude > maxMagnitude)
                velocityClamped = velocityClamped.normalized * maxMagnitude;

            return velocityClamped;
        }

        [NaughtyAttributes.Button("Get Self")]
        void GetSelf()
        {
            if (target == null)
                target = transform;
        }
    }
}