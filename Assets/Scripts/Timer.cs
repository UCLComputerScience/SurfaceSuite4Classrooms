using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	private float currentTime = 1;
	public Text time;
	float minutes;
	float seconds;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime < 300) {
			minutes = Mathf.Floor(currentTime / 60);
			seconds = currentTime%60;
			time.text = "Time: " + minutes + ":" + seconds.ToString ("0.");

		}
		else {
			SceneManager.LoadScene("Time's Up");
		}
	}

	public void getTime(){
		GameObject.Find("Canvas").GetComponent("submit_Script").SendMessage("receiver", currentTime);
	}
}
