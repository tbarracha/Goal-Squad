
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleOtherToggles : UIToggleButtonComponent
    {
        [SerializeField] UIToggleButton[] toggles;

        public override void Toggle(bool value)
        {
            if (value == true)
                for (int i = 0; i < toggles.Length; i++)
                    toggles[i].Toggle(false, true);
        }

        [NaughtyAttributes.Button("Get Other Toggles")]
        void GetOtherToggles()
        {
            if (targetToggleButton == null)
                return;

            var toggleList = Utilities.GetListComponentsInChildren<UIToggleButton>(targetToggleButton.Parent);
            if(toggleList.Count == 0)
                toggleList = Utilities.GetListComponentsInChildren<UIToggleButton>(targetToggleButton.Parent.parent);
            
            for (int i = 0; i < toggleList.Count; i++)
            {
                if (toggleList[i] == targetToggleButton)
                    toggleList.Remove(toggleList[i]);
            }

            toggles = toggleList.ToArray();
        }
    }
}