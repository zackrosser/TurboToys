using UnityEngine;
using System.Collections;

public class CS_Panel : MonoBehaviour {

    public GameObject activeScreen;
    public GameObject inactiveScreen;
    public bool activated = false;

    public void Activate()
    {
        activeScreen.SetActive(true);
        inactiveScreen.SetActive(false);
        activated = true;
    }
}
