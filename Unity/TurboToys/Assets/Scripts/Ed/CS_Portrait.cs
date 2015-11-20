using UnityEngine;
using System.Collections.Generic;

public class CS_Portrait : MonoBehaviour {

    public List<CS_Cursor> cursorsOnPortrait = new List<CS_Cursor>();

    void Update()
    {
        //Rearange the player plates on the cursor to make them all visible
        List<CS_Cursor> tempList = new List<CS_Cursor>(cursorsOnPortrait);
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].playerPlate.currentStackPosition = i;
        }
    }

	public void Highlighted(CS_Cursor cursor)
    {
        cursorsOnPortrait.Add(cursor);
    }

    public void NotHighlighted(CS_Cursor cursor)
    {
        cursorsOnPortrait.Remove(cursor);
    }

}
