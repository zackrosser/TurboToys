using UnityEngine;
using System.Collections.Generic;
using InControl;

public class CS_Cursor : MonoBehaviour {

    public DriverPicker driver;                 //The driver being updated
    public KartPicker kart;
    public GameObject readyOverlay;
    public int inputID;

    [HideInInspector]
    public CS_PlayerPlate playerPlate;

    private List<CS_Portrait> characterPortraits;
    private CS_Portrait currentPortrait;
    private Rotate_Object highlight;
    private Controller charLoader;
   
    private int currentIndex;


    private float elapsedTime;

    private enum SelectionState { inactive, selecting, lockedIn}
    private SelectionState currentSelectionState = SelectionState.inactive;

    private bool RightTriggerPressed = false;
    private bool LeftTriggerPressed = false;

    Controller.PlayerData player = new Controller.PlayerData();
                

    void Awake()
    {
        highlight = transform.FindChild("Highlight").GetComponent<Rotate_Object>();
        characterPortraits = GetAllPortraits();
        playerPlate = GetComponentInChildren<CS_PlayerPlate>();
        charLoader = GameObject.FindGameObjectWithTag("CharacterLoader").GetComponent<Controller>();
        
    }

    void OnEnable()
    {
        HighlightPortrait(characterPortraits[currentIndex]);
        currentSelectionState = SelectionState.inactive;
        StartCoroutine(AllowSelection());

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
                currentPortrait.NotHighlighted(this);
                HighlightPortrait(characterPortraits[currentIndex]);
            }


            if (currentSelectionState == SelectionState.selecting && currentPortrait && InputManager.Devices[inputID].Action1)
            {
                //Character Selected!!
                player.driver = driver.currentDriver;
                player.kart = kart.currentKart;
                Debug.Log(player.driver.ToString() + " " + player.kart.ToString());
                player.controllerIndex = inputID;
                charLoader.players.Add(player);
                
                currentSelectionState = SelectionState.lockedIn;
                readyOverlay.SetActive(true);
                highlight.stop = false;
                
            }

            if (currentSelectionState == SelectionState.lockedIn && currentPortrait && InputManager.Devices[inputID].Action2)
            {
                //Character Deselected
                charLoader.players.Remove(player);
                currentSelectionState = SelectionState.selecting;
                readyOverlay.SetActive(false);
                highlight.stop = true;
            }


            if (currentSelectionState != SelectionState.lockedIn)
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

    private System.Collections.IEnumerator AllowSelection()
    {
        yield return new WaitForSeconds(.5f);
        currentSelectionState = SelectionState.selecting;
    }
}
