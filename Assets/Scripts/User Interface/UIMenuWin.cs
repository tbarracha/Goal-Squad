using StardropTools.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions.CasualGame;

public class UIMenuWin : UIMenu
{
    [SerializeField] UIParticleSystem uIParticleSystem;
    [SerializeField] new ParticleSystem particleSystem;

    public override void Initialize()
    {
        base.Initialize();
        ParticleSystem.ShapeModule shape = particleSystem.shape;
        shape.radius = Screen.width / 4;
    }
}
