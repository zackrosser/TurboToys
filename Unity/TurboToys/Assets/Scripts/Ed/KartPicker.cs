using UnityEngine;
using System.Collections.Generic;
using System;

public class KartPicker : MonoBehaviour {

    public enum KartTypes
    {
        Carrot_Kart,
        Lego_Kart,
        lemon_kart,
        broccoli_kart,
        cherry_kart,
        cockroach_kart,
        gummybear_kart,
        soldier_kart
    }

    public KartTypes currentKart = KartTypes.Lego_Kart;

    private KartTypes previousKart;

    private List<GameObject> kartList = new List<GameObject>();

    public KartControls controls;      //To Attach the wheels

    // Use this for initialization
    void Start()
    {
        LoadAllDrivers();
        UpdateCurrentDriver(currentKart.ToString());
        previousKart = currentKart;

    }

    void Update()
    {
        if (currentKart != previousKart)
        {
            previousKart = currentKart;
            UpdateCurrentDriver(currentKart.ToString());

        }
    }

    public void UpdateCurrentDriver(string newDriver)
    {

        foreach (GameObject driver in kartList)
        {
            if (!driver.name.ToLower().Contains(newDriver.ToLower()))
            {
                driver.SetActive(false);
            }
            else
            {
                driver.SetActive(true);

                //Attach wheels to KartControls
                if (!controls)
                    return;

                GameObject wheelHolder = driver.transform.FindChild("goKart_Wheels_notAnimated").gameObject;
                GameObject[] wheels = new GameObject[wheelHolder.transform.childCount];

                for (int i = 0; i < wheelHolder.transform.childCount; i++)
                {
                    wheels[i] = wheelHolder.transform.GetChild(i).gameObject;
                }
                controls.wheels = wheels;
            }

        }
    }

    private void LoadAllDrivers()
    {
        foreach (KartTypes val in Enum.GetValues(typeof(KartTypes)))
        {
            GameObject driver = Instantiate(Resources.Load<GameObject>("Karts/" + val.ToString()));
            driver.transform.parent = transform;
            driver.transform.position = transform.position;
            driver.SetActive(false);
            kartList.Add(driver);
        }
    }
}
