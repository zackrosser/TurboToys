﻿using UnityEngine;

/// <summary>
/// The character panel where the player is prompted to join the game. When activated it displays the selected Character
/// </summary>
public class CS_Panel : MonoBehaviour {

    public GameObject activeScreen;
    public GameObject inactiveScreen;
    public GameObject readyScreen;

    public CS_Cursor cursor;
    public bool activated = false;

    public void Activate()
    {
        activeScreen.SetActive(true);
        inactiveScreen.SetActive(false);
        cursor.gameObject.SetActive(true);
        activated = true;
    }

    public bool Ready()
    {
        return readyScreen.activeSelf;
    }
}
