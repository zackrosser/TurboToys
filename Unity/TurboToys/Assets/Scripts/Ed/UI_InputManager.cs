using UnityEngine;
using System.Collections.Generic;
using InControl;
public class UI_InputManager : MonoBehaviour {

    public List<GameObject> playerCursors = new List<GameObject>();
    public List<CS_Panel> playerPanels = new List<CS_Panel>();

    private List<InputDevice> inputDevice = new List<InputDevice>();


	// Use this for initialization
	void Start () {
        for (int i = 0; i < InputManager.Devices.Count; i++ )
        {
            inputDevice.Add(InputManager.Devices[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 RS = inputDevice[0].RightStick;
        Debug.Log(InputManager.Devices.Count);

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i] && InputManager.Devices[i].Action1 && playerPanels[i] && !playerPanels[i].activated)
            {
                playerPanels[i].Activate();
            }
        }
	}
}
