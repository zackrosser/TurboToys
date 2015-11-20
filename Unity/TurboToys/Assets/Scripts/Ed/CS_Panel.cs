using UnityEngine;

/// <summary>
/// The character panel where the player is prompted to join the game. When activated it displays the selected Character
/// </summary>
public class CS_Panel : MonoBehaviour {

    public GameObject activeScreen;
    public GameObject inactiveScreen;
    public CS_Cursor cursor;
    public bool activated = false;

    public void Activate()
    {
        Debug.Log("activated" + name);
        activeScreen.SetActive(true);
        inactiveScreen.SetActive(false);
        cursor.gameObject.SetActive(true);
        activated = true;
    }
}
