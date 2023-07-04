
using StardropTools.UI;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    public class UIMenuBase : BaseComponent, IMenu
    {
        [Header("Open & Close Buttons")]
        [SerializeField] protected Button[] openButtons;
        [SerializeField] protected Button[] closeButtons;

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < openButtons.Length; i++)
                openButtons[i].onClick.AddListener(Open);

            for (int i = 0; i < closeButtons.Length; i++)
                closeButtons[i].onClick.AddListener(Close);
        }

        public virtual void Open()
        {
            SetActive(true);
        }

        public virtual void Close()
        {

        }
    }
}