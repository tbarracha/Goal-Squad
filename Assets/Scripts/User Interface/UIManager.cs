
using StardropTools;
using StardropTools.UI;
using UnityEngine;

public class UIManager : BaseManager
{
    [SerializeField] UIMenuBase[] menus;

    public override void Initialize()
    {
        base.Initialize();
        Utilities.InitializeBaseComponents(menus);
    }

    public override void LateInitialize()
    {
        base.LateInitialize();
        Utilities.LateInitializeBaseComponents(menus);
    }

    protected override void EventFlow()
    {
        GameManager.OnMainMenu.AddListener(     () => ChangeMenu(0));
        GameManager.OnPlayStart.AddListener(    () => ChangeMenu(1));
        GameManager.OnWin.AddListener(          () => ChangeMenu(2));
        GameManager.OnLose.AddListener(         () => ChangeMenu(3));
        GameManager.OnRestart.AddListener(      () => ChangeMenu(0));
    }

    void ChangeMenu(int menuID)
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].Close();

        menus[menuID].Open();
    }
}
