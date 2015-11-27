using UnityEngine;
using System.Collections;
using InControl;

public class MainMenuCursor : MonoBehaviour {

    public GameObject[] options;
    private float elapsedTime;
    private int currentIndex = 0;

    private bool aPressed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (options.Length == 0)
            return;

        if (InputManager.Devices[0])
        {
            InputDevice controller = InputManager.Devices[0];
            Vector3 joystickInput = controller.RightStick;


            elapsedTime -= Time.deltaTime * joystickInput.y * 5f;
            currentIndex = (int)elapsedTime % options.Length;

            if (elapsedTime > 10000)
            {
                elapsedTime = 0;
            }
        }

        currentIndex = Mathf.Abs(currentIndex);

        Vector3 newPos = transform.position;
        newPos.y = options[currentIndex].transform.position.y;
        transform.position = newPos;

        if (InputManager.Devices[0].Action1 && !aPressed)
        {
            aPressed = true;
            //Play
            if (currentIndex == 0)
            {
                Application.LoadLevel(1);
            }

            //Quit
            if (currentIndex == 1)
            {
                Application.Quit();
            }
        }
	}
}
