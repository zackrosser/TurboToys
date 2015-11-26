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

    private int counter = 0;
    private int index = 0;

    // Use this for initialization
    void Awake()
    {
        if (kartList.Count <= 0)
            LoadAllKarts();

        UpdateCurrentKart(currentKart.ToString());
        previousKart = currentKart;
        index = (int)currentKart;
        counter = index;

    }

    void Update()
    {
        if (currentKart != previousKart)
        {
            previousKart = currentKart;
            UpdateCurrentKart(currentKart.ToString());

        }
    }

    public void UpdateCurrentKart(string newKart)
    {
        currentKart = (KartTypes)Enum.Parse(typeof(KartTypes), newKart);
        foreach (GameObject driver in kartList)
        {
            if (!driver.name.ToLower().Contains(newKart.ToLower()))
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
                transform.parent.GetComponent<AIKart>().wheels = wheels;
            }

        }
    }

    private void LoadAllKarts()
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

    public void NextKart()
    {
        counter++;
        index = Mathf.Abs(counter % 8);
        KartTypes kart = (KartTypes)index;
        UpdateCurrentKart(kart.ToString());
    }

    public void PreviousKart()
    {
        counter--;
        index = Mathf.Abs(counter % 8);
        KartTypes kart = (KartTypes)index;
        UpdateCurrentKart(kart.ToString());
    }
}
