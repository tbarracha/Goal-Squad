
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleObjectActive : UIToggleButtonComponent
    {
        /// <summary>
        /// 0-deactive, 1-active
        /// </summary>
        [SerializeField] GameObject[] objects;

        public override void Toggle(bool value)
        {
            if (value == true)
            {
                objects[0].SetActive(false);
                objects[1].SetActive(true);
            }

            else
            {
                objects[0].SetActive(true);
                objects[1].SetActive(false);
            }
        }
    }
}