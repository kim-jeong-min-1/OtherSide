using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalType
{
    Basic,
    InteractPlayer
}

public class ClearPortal : Walkable
{
    public System.Func<Transform, Tween> GetWalkPointAction;

    public GameObject interactPlayer;
    public PortalType portalType;

    private void Awake()
    {
        if (portalType == PortalType.InteractPlayer) GetWalkPointAction = GetWalkPointInteract;
        else GetWalkPointAction = GetWalkPointBasic;
    }

    public Tween GetWalkPointInteract(Transform tr)
    {
        if (tr.gameObject == interactPlayer) return tr.DOMove(GetWalkPoint(), 0.25f);
        return null;
    }

    public Tween GetWalkPointBasic(Transform tr)
    {
        return tr.DOMove(GetWalkPoint(), 0.25f);
    }
}
