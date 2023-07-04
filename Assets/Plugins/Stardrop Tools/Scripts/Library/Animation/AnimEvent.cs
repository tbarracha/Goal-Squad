using System.Collections;
using UnityEngine;

/// <summary>
/// Work in progress. Script anim events to work with SingleLayerAnimation
/// </summary>
[System.Serializable]
public class AnimEvent
{
    [SerializeField] string name;
    [SerializeField] float normalizedTimeEvent;
    [SerializeField] bool triggered;
}