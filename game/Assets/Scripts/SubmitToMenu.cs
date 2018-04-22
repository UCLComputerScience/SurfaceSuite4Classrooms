using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitToMenu : MonoBehaviour {

	string jsonString;
	public DynamicScene dynamic_scene;
	public Text down;
	public Text right;
	public Text up;
	public Text left;


	public void Start(){	
		StartCoroutine (WaitForRequest ());
	}

	IEnumerator WaitForRequest(){
		using (UnityWebRequest www = UnityWebRequest.Get("http://51.145.24.155:3000/client/info")){
			yield return www.Send();
			if ((www.error == null) && www.downloadHandler.text != "{}") {
				jsonString = www.downloadHandler.text;
				dynamic_scene = CreateFromJSON (jsonString);
				show_answers ();
			}
			else{
				Debug.Log(www.error);
			}
		}
	}

	public static DynamicScene CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<DynamicScene>(jsonString);
	}

	public void show_answers(){
		down.text = dynamic_scene.get_answer1 ().ToUpper();
		right.text = dynamic_scene.get_answer2 ().ToUpper();
		up.text = dynamic_scene.get_answer3().ToUpper();
		left.text = dynamic_scene.get_answer4().ToUpper();
	}

	public void Button_Click(){
		SceneManager.LoadScene("MenuScene");
	}
}
