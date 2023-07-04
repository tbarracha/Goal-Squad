using UnityEngine;

namespace StardropTools
{
    public class BillboardFX : BaseComponent
    {
        public Transform camTransform;
        public bool updateOnInitialization = true;

        Quaternion originalRotation;

        protected override void Awake()
        {
            base.Awake();

            if (camTransform == null)
                camTransform = Camera.main.transform;
            originalRotation = transform.rotation;
        }

        public override void Initialize()
        {
            base.Initialize();

            if (updateOnInitialization)
                StartUpdate();
        }

        public override void HandleUpdate()
        {
            base.HandleUpdate();
            transform.rotation = camTransform.rotation * originalRotation;
        }
    }
}
