using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {

    public static EventsManager instance;

    public delegate void PlayerHitted();
    public static event PlayerHitted OnPlayerHitted;

    public delegate void PlayerExplodes();
    public static event PlayerExplodes OnPlayerExplodes;

    public delegate void WinLevel();
    public static event WinLevel OnWinLevel;

    void Awake() {
        instance = this;
    }

    public void initPlayerHitted() {
        //OnPlayerHitted();
    }

    public void initPlayerExplodes() {
        OnPlayerExplodes();
    }

    public void initWinLevel() {
        OnWinLevel();
    }

}
