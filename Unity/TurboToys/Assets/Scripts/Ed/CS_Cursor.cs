using UnityEngine;
using System.Collections.Generic;
using InControl;

public class CS_Cursor : MonoBehaviour {

    public DriverPicker driver;                 //The driver being updated
    public KartPicker kart;
    public int inputID;

    [HideInInspector]
    public CS_PlayerPlate playerPlate;

    private List<CS_Portrait> characterPortraits;
    private CS_Portrait currentPortrait;
    private Rotate_Object highlight;
   
    private int currentIndex;


    private float elapsedTime;

    private enum SelectionState { inactive, selecting, lockedIn}
    private SelectionState currentSelectionState = SelectionState.inactive;

    private bool RightTriggerPressed = false;
    private bool LeftTriggerPressed = false;


    void Awake()
    {
        highlight = transform.FindChild("Highlight").GetComponent<Rotate_Object>();
        characterPortraits = GetAllPortraits();
        playerPlate = GetComponentInChildren<CS_PlayerPlate>();
        
        
    }

    void OnEnable()
    {
        HighlightPortrait(characterPortraits[currentIndex]);
        
    }

    void Update()
    {
        if (InputManager.Devices[inputID])
        {
            if (currentSelectionState != SelectionState.lockedIn)
            {
                ProcessCSInput();
                
            }

            if (currentPortrait != characterPortraits[currentIndex])
            {
                currentSelectionState = SelectionState.selecting;       //KnownBug: Player cant select the first character the cursor appears on (fix by moving cursor to another char and then back to the original)
                currentPortrait.NotHighlighted(this);
                HighlightPortrait(characterPortraits[currentIndex]);
            }


            if (currentSelectionState == SelectionState.selecting && currentPortrait && InputManager.Devices[inputID].Action1)
            {
                //Character Selected!!
                currentSelectionState = SelectionState.lockedIn;
                highlight.stop = false;
            }

            if (currentSelectionState == SelectionState.lockedIn && currentPortrait && InputManager.Devices[inputID].Action2)
            {
                currentSelectionState = SelectionState.selecting;
                highlight.stop = true;
            }



            ProcessKartSelectionInput();

        }
    }

	public void HighlightPortrait(CS_Portrait portrait)
    {

        currentPortrait = portrait;

        currentPortrait.Highlighted(this);

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

    private void ProcessCSInput()
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
    }

    private void ProcessKartSelectionInput()
    {
        if (InputManager.Devices[inputID].RightTrigger)
        {
            if (!RightTriggerPressed)
            {
                RightTriggerPressed = true;
                kart.NextKart();
            }
        }
        else
        {
            RightTriggerPressed = false;
        }

        if (InputManager.Devices[inputID].LeftTrigger)
        {
            if (!LeftTriggerPressed)
            {
                LeftTriggerPressed = true;
                kart.PreviousKart();
            }
        }
        else
        {
            LeftTriggerPressed = false;
        }
    }
}
