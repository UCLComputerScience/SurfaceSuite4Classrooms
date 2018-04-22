using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using UnityEngine.Networking;


public class submit_Script : MonoBehaviour{

    float finishTime;
	float score_d = 0.0f; 
	float score_r = 0.0f; 
	float score_u = 0.0f; 
	float score_l = 0.0f; 
	int dict_items;
    public GameObject singleBox;
	public GameObject[] boxes;
	Dictionary<string,string> dict = new Dictionary<string, string>();
	string[] temp_string;
	char[] ans_down;
	char[] ans_right;
	char[] ans_up;
	char[] ans_left;
	char[] tempstr;
	bool d,r,u,l;
	public int[] user_id;
	float time_d = 0.0f;
	float time_r = 0.0f;
	float time_u = 0.0f;
	float time_l = 0.0f;
	float currentTime = 0.0f;

	public void get_d(){
		d = true;
		time_d = currentTime;
		Send ();
	}
	public void get_r(){
		time_r = currentTime;
		r = true;
		Send ();
	}

	public void get_u(){
		time_u = currentTime;
		u = true;
		Send ();
	}
	public void get_l(){
		time_l = currentTime;
		l = true;
		Send ();
	}

	//Receiving Time and adjusting it to corresponding score

	public void get_id(int[] id){
		user_id = id;
	}

	//Receiving boxes and letters, cheking them and adjusting scores
	public void receiveBoxLetter(string box_letter){
		temp_string = box_letter.Split(new char[0]);
		dict.Add(temp_string[0],temp_string[1]);
	}

	void receive_answers(string all_ans){
		string[] ans = all_ans.Split(',');
		ans_down = ans[0].ToCharArray();
		ans_right = ans[1].ToCharArray();
		ans_up = ans[2].ToCharArray();
		ans_left = ans [3].ToCharArray();


	}

	void Update () {
		currentTime += Time.deltaTime;
		}

	//Triggeirng events after clicking buttons


	public void Send (){
		if (l && r && d && u) {
			GameObject.Find ("Main Camera").GetComponent ("Scene_Manager").SendMessage ("give_answers");
			GameObject.Find ("Main Camera").GetComponent ("Scene_Manager").SendMessage ("send_id");
			boxes = GameObject.FindGameObjectsWithTag ("Box");
			foreach (GameObject box in boxes) {
				box.GetComponent ("BoxTrigger").SendMessage ("getBoxAndLetter");
			}
			score_detector ();
			WWWForm form_d = new WWWForm ();
			WWWForm form_r = new WWWForm ();
			WWWForm form_u = new WWWForm ();
			WWWForm form_l = new WWWForm ();

			form_l.AddField ("score", score_l.ToString ());
			WWW www1 = new WWW ("http://51.145.24.155:3000/client/edit/" + user_id [0].ToString (), form_l);
			Debug.Log (form_l.data);
			StartCoroutine (postRequest (www1));

			form_d.AddField ("score", score_d.ToString ());
			WWW www2 = new WWW ("http://51.145.24.155:3000/client/edit/" + user_id [1].ToString (), form_d);
			StartCoroutine (postRequest (www2));

			form_r.AddField ("score", score_r.ToString ());
			WWW www3 = new WWW ("http://51.145.24.155:3000/client/edit/" + user_id [2].ToString (), form_r);
			StartCoroutine (postRequest (www3));
				
			form_u.AddField ("score", score_u.ToString ());
			WWW www4 = new WWW ("http://51.145.24.155:3000/client/edit/" + user_id [3].ToString (), form_u);
			StartCoroutine (postRequest (www4));
			SceneManager.LoadScene ("Submision");
		}
	}
		

	IEnumerator postRequest(WWW www) {
		yield return www;

			// check for errors
			if (www.error == null)
			{
			Debug.Log("WWW Ok!: " + www.text);
			} 
			else {
				Debug.Log("WWW Error: "+ www.error);
			}    
	}


	//char[] down,char[] right, char[] up, char[] left
	  	void score_detector(){
		string name;
		dict_items = ans_down.Length + ans_right.Length + ans_up.Length + ans_left.Length;
		for (int i = 0; i < ans_down.Length; i++) {
			name = "Box_D" + (i + 1).ToString ();
			tempstr = (dict[name]).ToCharArray();
			if (tempstr.Length > 0 && tempstr [0] == ans_down[i]) {
				score_d += 100 / ans_down.Length;
				timeAdjuster (score_d, time_d);
			 }

		}

		for (int i = 0; i < ans_right.Length ; i++) {
			name = "Box_R" + (i + 1).ToString();
			tempstr = dict[name].ToCharArray();
			if (tempstr.Length > 0 && tempstr[0] == ans_right[i]) {
				score_r += 100 / ans_right.Length;
				timeAdjuster (score_r, time_r);
			}
		}
		for (int i = 0; i < ans_up.Length ; i++) {
			name = "Box_U" + (i + 1).ToString();
			tempstr = dict[name].ToCharArray();
			if (tempstr.Length > 0 && tempstr[0] == ans_up[i]) {
				score_u += 100 / ans_up.Length;
				timeAdjuster (score_u, time_u);
			}
		}
		for (int i = 0; i < ans_left.Length; i++) {
			name = "Box_L" + (i + 1).ToString();
			tempstr = dict[name].ToCharArray();
			if (tempstr.Length > 0 && tempstr[0] == ans_left[i]) {
				score_l += 100 / ans_left.Length;
				timeAdjuster (score_l, time_l);
			}
		}


		Debug.Log ("down : " + score_d.ToString ());
		Debug.Log ("righrt : "+ score_r.ToString ());
		Debug.Log ("up : " + score_u.ToString ());
		Debug.Log ("left : " + score_l.ToString ());


	}

	void timeAdjuster(float score, float time){
		if (time > 15) {
			score -= time / 3;
		}
	}
}