using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public int option;
	void Start () {
		GetComponent<Renderer>().material.color = Color.cyan;
	}

	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.yellow;
	}

	void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.cyan;
	}
	void OnMouseUp(){
		switch(option)
		{
		case 0:
			//Application.LoadLevel (1);
			SceneManager.LoadScene ("map1");
			break;
		case 3:
			Application.Quit ();
			break;
		}
	} 
}
