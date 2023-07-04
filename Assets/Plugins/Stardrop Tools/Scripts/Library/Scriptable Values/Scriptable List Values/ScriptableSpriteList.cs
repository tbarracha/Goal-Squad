
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.ScriptableValue
{
    [CreateAssetMenu(menuName = scriptableListMenuName + "Scriptable List Sprite")]
    public class ScriptableSpriteList : ScriptableList
    {
        [NaughtyAttributes.ShowAssetPreview][SerializeField] List<Sprite> list;
        [NaughtyAttributes.ShowAssetPreview][SerializeField] List<Sprite> defaultList;

        public Sprite GetSpriteAtIndex(int index) => list[index];

        public Sprite GetRandom() => list.GetRandom();
        public List<Sprite> GetRandomAmount(int amount) => list.GetRandomNonRepeat(amount);

        protected override void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke();
        }

        [NaughtyAttributes.Button]
        public override void Default(bool invokeEvents = true)
        {
            list = new List<Sprite>();
            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);

            if (invokeEvents == false)
                InvokeEvents(invokeEvents);
        }

        public void AddToList(Sprite sprite, bool checkIfExistsInList = false)
        {
            if (checkIfExistsInList)
                list.AddSafe(sprite);
            else
                list.Add(sprite);
        }

        public void RemoveFromList(Sprite sprite, bool checkIfExistsInList = false)
        {
            if (checkIfExistsInList)
                list.RemoveSafe(sprite);
            else
                list.Remove(sprite);
        }
    }
}
