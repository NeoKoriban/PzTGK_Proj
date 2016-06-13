using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NMovement : EnemyScript {

	// Use this for initialization
	void Start () {
		base.Start ();
		destroyAfterLastAction = true;

		List<BaseAI> aiq = new List<BaseAI> ();
		BaseAI forward = new BaseAI ();
		forward.finalPointCoords = new Vector2 (12.0f, 21.0f);
		forward.typeOfMove = 1;
		forward.lengthOfAction = 2.0f;
		aiq.Add (forward);
		BaseAI back = new BaseAI ();
		back.finalPointCoords = new Vector2 (-12.0f, 60.0f);
		back.typeOfMove = 1;
		back.lengthOfAction = 2.0f;
		aiq.Add (back);
		BaseAI forward2 = new BaseAI ();
		forward2.finalPointCoords = new Vector2 (-12.0f, 61.0f);
		forward2.typeOfMove = 1;
		forward2.lengthOfAction = 2.0f;
		aiq.Add (forward2);
		BaseAI exit = new BaseAI ();
		exit.typeOfMove = 0;
		exit.lengthOfAction = 5.0f;
		aiq.Add (exit);
		setAIQueue (aiq);
		UpdateDifference ();
	}
}
