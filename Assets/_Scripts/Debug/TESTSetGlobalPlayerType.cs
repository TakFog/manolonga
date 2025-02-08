using System;
using UnityEngine;

public class TESTSetGlobalPlayerType : MonoBehaviour
{
    public PlayerType playerType;

#if UNITY_EDITOR
    private void Awake()
    {
        Globals.PlayerType = playerType;
    }
#endif
}
