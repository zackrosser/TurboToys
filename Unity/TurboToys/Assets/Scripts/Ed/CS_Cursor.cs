using UnityEngine;
using System.Collections.Generic;
using InControl;

public class CS_Cursor : MonoBehaviour {

    public DriverPicker driver;                 //The driver being updated
    public int inputID;

    private List<CS_Portrait> characterButtons;
    private CS_Portrait currentPortrait;
    private int currentIndex;


    private float elapsedTime;
    

    void Awake()
    {
        characterButtons = GetAllPortraits();
    }

    void OnEnable()
    {
        HighlightPortrait(characterButtons[currentIndex]);
    }

    void Update()
    {
        if (InputManager.Devices[inputID])
        {
            InputDevice controller = InputManager.Devices[inputID];
            Vector3 joystickInput = controller.RightStick;

            if (joystickInput.y > 0)
            {

                elapsedTime -= Time.deltaTime * Mathf.Abs(joystickInput.y) * 5f;
                currentIndex = Mathf.RoundToInt(elapsedTime);

                if (currentIndex < 0)
                {
                    elapsedTime = 0;
                    currentIndex = 0;
                }

            }
            if (joystickInput.y < 0)
            {

                elapsedTime += Time.deltaTime * Mathf.Abs(joystickInput.y) * 5f;
                currentIndex = Mathf.RoundToInt(elapsedTime);

                if (currentIndex >= 3)
                {
                    elapsedTime = 2;
                    currentIndex = 2;
                }

            }

            if (currentPortrait != characterButtons[currentIndex])
            {
                currentPortrait.NotHighlighted();
                HighlightPortrait(characterButtons[currentIndex]);
            }
        }
    }

	public void HighlightPortrait(CS_Portrait portrait)
    {

        currentPortrait = portrait;

        currentPortrait.Highlighted();

        transform.position = portrait.transform.position;

        driver.UpdateCurrentDriver(portrait.name);
 
    }

    private List <CS_Portrait> GetAllPortraits()
    {
        List <CS_Portrait> portraits = new List<CS_Portrait>();
        foreach (Transform child in transform.parent)
        {
            if (!child.name.Contains("Cursor"))
            {
                portraits.Add(child.GetComponent<CS_Portrait>());
            }
        }

        return portraits;
    }
}
