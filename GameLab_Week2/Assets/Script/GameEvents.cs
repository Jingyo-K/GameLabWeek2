using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<GameEvents> onGameStart;
    public static Action<GameEvents> onWeaponSwap;
    public static Action<GameEvents> onWeaponChange;
    public static Action<GameEvents> onPlayerTakeDamage;
    public static Action<GameEvents> onPlayerDeath;
    public static Action<GameEvents> onNextStage;
    public static Action<GameEvents> onGameEnd;

    public void callGameStart()
    {
        onGameStart?.Invoke(this);
    }

    public void callWeaponSwap()
    {
        onWeaponSwap?.Invoke(this);
    }
    public void callWeaponChange()
    {
        onWeaponChange?.Invoke(this);
    }
    public void callPlayerTakeDamage()
    {
        onPlayerTakeDamage?.Invoke(this);
    }
    public void callPlayerDeath()
    {
        onPlayerDeath?.Invoke(this);
    }
    public void callNextStage()
    {
        onNextStage?.Invoke(this);
    }
    public void callGameEnd()
    {
        onGameEnd?.Invoke(this);
    }
}
