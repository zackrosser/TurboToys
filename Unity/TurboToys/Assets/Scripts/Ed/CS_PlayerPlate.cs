using UnityEngine;
using System.Collections;

public class CS_PlayerPlate : MonoBehaviour {

    public float stackingYOffset = 80;
    public int currentStackPosition;

    private float baseYPosition;
    private int previousStackPosition;
    private RectTransform UItrans;

    // Use this for initialization
    void Start()
    {
        currentStackPosition = 1;
        previousStackPosition = currentStackPosition;
        UItrans = GetComponent<RectTransform>();
        baseYPosition = UItrans.anchoredPosition.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (previousStackPosition != currentStackPosition)
        {

            previousStackPosition = currentStackPosition;

            Vector3 newPos = UItrans.anchoredPosition;
            newPos.y = baseYPosition + (stackingYOffset * currentStackPosition);
            UItrans.anchoredPosition = newPos;
        }

    }
}
