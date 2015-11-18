using UnityEngine;
using System.Collections;

public class CharacterSelectCursor : MonoBehaviour {

	public void MoveToObjectsPosition(GameObject obj)
    {
        transform.position = obj.transform.position;
    }
}
