using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Scene_Manager:MonoBehaviour{
	public string jsonString;
	public DynamicScene dynamic_scene;
	public Text stud_name1;
	public Text stud_name2;
	public Text stud_name3;
	public Text stud_name4;

	public void Start(){	
		StartCoroutine (WaitForRequest ());
	}
	IEnumerator WaitForRequest(){
		using (UnityWebRequest www = UnityWebRequest.Get("https://22a6614f-8cdb-4a51-9d52-8a513e759a6c.mock.pstmn.io/client/info")){
			//Full Scene Json https://8d88de88-1537-49aa-9056-4904cfcb1f59.mock.pstmn.io/client/info
			//https://22a6614f-8cdb-4a51-9d52-8a513e759a6c.mock.pstmn.io/client/info
			yield return www.Send();
			if ((www.error == null) && www.downloadHandler.text != "{}") {
				jsonString = www.downloadHandler.text;
				dynamic_scene = CreateFromJSON (jsonString);
				    displayNames ();
				Debug.Log (jsonString);
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

	public void displayNames(){
		stud_name1.text = dynamic_scene.get_student1();
		stud_name2.text = dynamic_scene.get_student2();
		stud_name3.text = dynamic_scene.get_student3();
		stud_name4.text = dynamic_scene.get_student4();
	}
}
		
