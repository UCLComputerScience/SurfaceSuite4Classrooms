using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	float timeRemaining = 59;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}

	void OnGUI(){
		GUI.contentColor = Color.red;
		if (timeRemaining > 0) {
			GUI.Label (new Rect (8, 10, 200, 100), "Time Remaining: " + (int)timeRemaining);

		} else {
			GUI.Label (new Rect (8, 10, 200, 100), "Time's Up: " + (int)timeRemaining);
			SceneManager.LoadScene("Time's Up");
		}
	}
}
