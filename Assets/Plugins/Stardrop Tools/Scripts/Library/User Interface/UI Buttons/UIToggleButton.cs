
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleButton : UIButton
    {
        [Header("Toggle Button")]
        [SerializeField] protected bool initialToggle;
        [SerializeField] protected Toggle toggle;
        [SerializeField] protected Transform parentComponents;
        [SerializeField] protected UIToggleButtonComponent[] toggleComponents;

        public bool Value => toggle.Value;

        public EventHandler OnToggle => toggle.OnToggle;
        public EventHandler<bool> OnToggleValue => toggle.OnToggleValue;
        
        public readonly EventHandler OnToggleTrue = new EventHandler();
        public readonly EventHandler OnToggleFalse = new EventHandler();

        public readonly EventHandler<UIToggleButton> OnToggleClass = new EventHandler<UIToggleButton>();
        public readonly EventHandler<int> OnToggleIndex = new EventHandler<int>();

        public readonly EventHandler<int> OnToggleTrueIndex = new EventHandler<int>();
        public readonly EventHandler<int> OnToggleFalseIndex = new EventHandler<int>();



        public override void Initialize()
        {
            base.Initialize();

            toggle.ToggleValue(initialToggle);
            RefreshToggleComponents();

            OnClick.AddListener(Toggle);
            OnToggle.AddListener(() => OnToggleClass?.Invoke(this));
            OnToggle.AddListener(() => OnToggleIndex?.Invoke(ButtonID));

            if (toggleComponents.Exists())
                for (int i = 0; i < toggleComponents.Length; i++)
                    toggleComponents[i].Initialize();
        }


        public void Toggle()
        {
            toggle.ToggleValue();
            OnValueChanged();
        }

        public void Toggle(bool value, bool validate = true)
        {
            if (validate && value == Value)
                return;

            toggle.ToggleValue(value);
            OnValueChanged();
        }

        protected void RefreshToggleComponents()
        {
            for (int i = 0; i < toggleComponents.Length; i++)
                toggleComponents[i].Toggle(Value);
        }


        protected void OnValueChanged()
        {
            if (Value == true)
                OnTrue();
            else
                OnFalse();

            RefreshToggleComponents();
            //Debug.Log(name + ", toggled: " + Value);
        }

        protected virtual void OnTrue()
        {
            OnToggleTrue?.Invoke();
            OnToggleTrueIndex?.Invoke(ButtonID);
        }

        protected virtual void OnFalse()
        {
            OnToggleFalse?.Invoke();
            OnToggleFalseIndex?.Invoke(ButtonID);
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (toggle == null)
                toggle = GetComponent<Toggle>();

            if (parentComponents != null && Utilities.GetListComponentsInChildren<UIToggleButtonComponent>(parentComponents) != null)
                toggleComponents = Utilities.GetListComponentsInChildren<UIToggleButtonComponent>(parentComponents).ToArray();
        }
    }
}