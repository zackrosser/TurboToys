using UnityEngine;

public class CS_Portrait : MonoBehaviour {

    public int cursorsOnPortrait = 0;

    void Update()
    {
        if (name.Equals("carrot"))
        {
            Debug.Log(cursorsOnPortrait);
        }
    }

	public void Highlighted()
    {
        cursorsOnPortrait++;
    }

    public void NotHighlighted()
    {
        cursorsOnPortrait--;
    }

    public void Selected()
    {

    }
}
