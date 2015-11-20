using UnityEngine;
using System.Collections.Generic;
using System;

public class DriverPicker : MonoBehaviour {
    

    public enum KartDrivers{
        carrot,
        legoMan,
        LemonDude
    }

    public KartDrivers currentDriver = KartDrivers.legoMan;

    private KartDrivers previousDriver;

    private List<GameObject> driverList = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        if (driverList.Count <= 0)
        {
            LoadAllDrivers();
        }

        UpdateCurrentDriver(currentDriver.ToString());
        previousDriver = currentDriver;
	}
	
    void Update()
    {
        if (currentDriver != previousDriver)
        {
            previousDriver = currentDriver;
            UpdateCurrentDriver(currentDriver.ToString());
            
        }
    }

    public void UpdateCurrentDriver(string newDriver)
    {
        currentDriver = (KartDrivers) Enum.Parse(typeof(KartDrivers), newDriver); 
        foreach (GameObject driver in driverList)
        {
            if (!driver.name.ToLower().Contains(newDriver.ToLower()))
            {
                driver.SetActive(false);
            }
            else
            {
                driver.SetActive(true);
            }

        }
    }

    private void LoadAllDrivers()
    {
        foreach (KartDrivers val in Enum.GetValues(typeof(KartDrivers)))
        {
            GameObject driver = Instantiate(Resources.Load<GameObject>(val.ToString()));
            driver.transform.parent = transform;
            driver.transform.position = transform.position;
            driver.SetActive(false);
            driverList.Add(driver);
        }
    }
}
