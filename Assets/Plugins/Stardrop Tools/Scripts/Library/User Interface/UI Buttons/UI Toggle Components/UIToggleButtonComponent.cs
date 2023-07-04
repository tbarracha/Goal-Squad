
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleButtonComponent : MonoBehaviour
    {
        [SerializeField] protected UIToggleButton targetToggleButton;

        public void Initialize()
        {
            targetToggleButton.OnToggleValue.AddListener(Toggle);
        }


        public abstract void Toggle(bool value);



        [NaughtyAttributes.Button("Get Toggle Button")]
        protected void GetButton()
        {
            targetToggleButton = GetComponent<UIToggleButton>();

            if (targetToggleButton == null)
                targetToggleButton = GetComponentInParent<UIToggleButton>();
        }
    }
}