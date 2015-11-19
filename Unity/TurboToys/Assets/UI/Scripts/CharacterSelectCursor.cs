using UnityEngine;
using System.Collections;
using InControl;

public class CharacterSelectCursor : MonoBehaviour {

    public int inputID;

    private GameObject[] characterButtons;
    
    void Update()
    {
        if (InputManager.Devices[inputID])
        {
            InputDevice controller = InputManager.Devices[inputID];
            Vector3 joystickInput = controller.RightStick;
            Debug.Log(joystickInput + name);
        }
    }

	public void MoveToObjectsPosition(GameObject obj)
    {
        transform.position = obj.transform.position;
    }
}
