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

        if (InputManager.Devices.Count <= 0)
            return;

        Vector3 RS = inputDevice[0].RightStick;

        for (int i = 0; i < InputManager.Devices.Count; i++)
        {
            if (InputManager.Devices[i] && InputManager.Devices[i].Action1 && playerPanels[i] && !playerPanels[i].activated)
            {
                playerPanels[i].Activate();
            }
        }


    
        if (!ReadyToPlay())
            return;

        //Press Manu to start the game!
        foreach (InputDevice controller in InputManager.Devices)
        {
            if (controller.MenuWasPressed)
            {
                Application.LoadLevel(Application.loadedLevel + 1);
            }
        }
    }

    private bool ReadyToPlay()
    {
        List<CS_Panel> activePanels = new List<CS_Panel>();

        foreach(CS_Panel panel in playerPanels)
        {
            if (panel.activated)
            {
                activePanels.Add(panel);
            }
        }

        //MAke sure all active panels are ready to start the game
        foreach (CS_Panel panel in activePanels)
        {
            if (!panel.Ready())
            {
                return false;
            }
        }

        return true;
    }
}
