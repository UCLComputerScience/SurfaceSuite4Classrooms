using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.Networking;



public class Main_Menu : MonoBehaviour {
  //another url http://51.145.24.155:3000/client/info
	string received_json;
	public void playGame(){	
		StartCoroutine (WaitForRequest ());
	}

	IEnumerator WaitForRequest(){
		using (UnityWebRequest www = UnityWebRequest.Get("http://51.145.24.155:3000/client/info")){
		yield return www.Send();
		 if ((www.error == null) && www.downloadHandler.text != "{}"){
				SceneManager.LoadScene ("DynamicScene");
		}
		 
		else {
				Debug.Log(www.error);
		}
	  }
	}

    public void exit(){
        Application.Quit();
    }


}
