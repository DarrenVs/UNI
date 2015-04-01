using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
	public void OnGUI () {
		if (GUI.Button(new Rect(960/2-150, 600/2-25*2, 100, 25), "Start Game!"))
			Application.LoadLevel("Scene 2");
	}
}