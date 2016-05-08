using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseAI {
	
	public Vector2 finalPointCoords;
	public float lengthOfAction;
	public int typeOfMove; //0=idle, 1==linear, 2==circular
	public float shootInterval;
	
}
