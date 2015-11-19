using UnityEngine;
using System.Collections;

public class CS_Panel : MonoBehaviour {

    public GameObject activeScreen;
    public GameObject inactiveScreen;
    public CharacterSelectCursor cursor;
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
