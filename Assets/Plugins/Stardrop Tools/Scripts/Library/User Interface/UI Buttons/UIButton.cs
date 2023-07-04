
using UnityEngine;

namespace StardropTools.UI
{
    public class UIButton : BaseUIObject
    {
        public int ButtonID;
        [SerializeField] protected UnityEngine.UI.Button button;

        public UnityEngine.UI.Button Button => button;
        public bool Interactible { get => button.interactable; set => button.interactable = value; }

        public UnityEngine.Events.UnityEvent OnClick => button.onClick;
        public readonly EventHandler<int> OnClickID = new EventHandler<int>();

        public override void Initialize()
        {
            base.Initialize();

            OnClick.AddListener(() => OnClickID?.Invoke(ButtonID));
        }

        public void SetInteractible(bool value) => button.interactable = value;



        protected override void OnValidate()
        {
            base.OnValidate();

            if (button == null)
                button = GetComponent<UnityEngine.UI.Button>();

            if (button == null)
                button = GetComponentInParent<UnityEngine.UI.Button>();
        }
    }
}