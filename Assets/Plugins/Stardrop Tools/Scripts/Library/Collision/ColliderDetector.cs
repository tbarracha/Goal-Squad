
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

namespace StardropTools
{
    /// <summary>
    /// Base class for Physics.Overlap collider detectors
    /// </summary>
    public abstract class ColliderDetector : BaseObject
    {
        [SerializeField] protected LayerMask contactLayer;
        [SerializeField] protected bool hasContact;
        [SerializeField] protected bool ignoreSelf;
        [Space]
        [SerializeField] List<Collider> ignoreTheseColliders;

        protected Collider[] colliders;
        protected List<Collider> contactColliders;

        public bool HasContact => hasContact;

        public readonly UnityEvent OnContactStart = new UnityEvent();
        public readonly UnityEvent OnContactEnd = new UnityEvent();

        public readonly UnityEvent<Collider> OnColliderEnter = new UnityEvent<Collider>();
        public readonly UnityEvent<Collider> OnColliderExit = new UnityEvent<Collider>();

        protected override void Start()
        {
            base.Start();

            if (ignoreSelf)
                IgnoreSelf();
        }

        /// <summary>
        /// Call this on a collider detector to check if there are any colliders within bounds.
        /// <para>Call every frame to check for colliders every frame!</para>
        /// </summary>
        public virtual List<Collider> SearchForColliders()
        {
            if (contactColliders == null)
                contactColliders = new List<Collider>();

            if (colliders == null || colliders.Length == 0)
            {
                if (hasContact == true)
                {
                    OnContactEnd?.Invoke();
                    contactColliders = new List<Collider>();
                    hasContact = false;
                }

                return null;
            }

            if (hasContact == false)
            {
                OnContactStart?.Invoke();
                hasContact = true;
            }

            FilterColliders();
            return contactColliders;
        }

        protected void FilterColliders()
        {
            FilterCollidersToIgnore();

            // if not equal, either entered contact, or a collider has left contact
            if (colliders.Length != contactColliders.Count)
            {
                // collider ENTER
                if (colliders.Length > contactColliders.Count)
                {
                    IEnumerable<Collider> outsideOfContactColliders = colliders.Where(c => contactColliders.Contains(c) == false);
                    Collider[] contactEnters = outsideOfContactColliders.ToArray();

                    for (int i = 0; i < contactEnters.Length; i++)
                        OnColliderEnter?.Invoke(contactEnters[i]);
                }


                // collider EXIT
                else
                {
                    IEnumerable<Collider> outsideOfColliders = contactColliders.Where(c => colliders.Contains(c) == false);
                    Collider[] contactExits = outsideOfColliders.ToArray();

                    for (int i = 0; i < contactExits.Length; i++)
                        OnColliderExit?.Invoke(contactExits[i]);
                }

                contactColliders = colliders.ToList();
            }
        }

        protected void FilterCollidersToIgnore()
        {
            if (ignoreTheseColliders.Exists() == false)
                return;

            IEnumerable<Collider> outsideOfIgnores = colliders.Where(c => ignoreTheseColliders.Contains(c) == false);
            colliders = outsideOfIgnores.ToArray();
        }

        void IgnoreSelf()
        {
            var colliders = GetComponentsInChildren<Collider>();
            var ignores = colliders.ToList();

            Collider collider = GetComponent<Collider>();
            if (collider != null)
                ignores.Add(collider);

            ignoreTheseColliders = ignores;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
        }
    }
}