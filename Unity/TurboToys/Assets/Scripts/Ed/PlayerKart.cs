using UnityEngine;
using System.Collections;

public class PlayerKart : MonoBehaviour {

    public KartPicker kart;
    public DriverPicker driver;
    public KartControls kartControls;

    void Start()
    {
        if (!driver || !kart)
            Debug.LogWarning(name + "does not have a Characters or GoKart transforms");

    }

    public void SetDriver(DriverPicker.KartDrivers driver)
    {
        this.driver.UpdateCurrentDriver (driver.ToString());
    }

    public void SetKart(KartPicker.KartTypes kart)
    {
        this.kart.UpdateCurrentKart (kart.ToString());
    }

    public void SetControlID(int id)
    {
        kartControls.playerID = id;
    }
}
