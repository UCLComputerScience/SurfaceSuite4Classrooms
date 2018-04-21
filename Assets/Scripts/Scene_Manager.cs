using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Scene_Manager:MonoBehaviour{

	/* All Game Objects (Letters) + Canvas(UI) Objects that are instantiated on Runtime are Public */
	public GameObject A;
	public GameObject B;
	public GameObject C;
	public GameObject D;
	public GameObject E;
	public GameObject F;
	public GameObject G;
	public GameObject H;
	public GameObject I;
	public GameObject J;
	public GameObject K;
	public GameObject L;
	public GameObject M;
	public GameObject N;
	public GameObject O;
	public GameObject P;
	public GameObject Q;
	public GameObject R;
	public GameObject S;
	public GameObject T;
	public GameObject U;
	public GameObject V;
	public GameObject W;
	public GameObject X;
	public GameObject Y;
	public GameObject Z;

	string jsonString;
	public DynamicScene dynamic_scene;
	public Text stud_name1;
	public Text stud_name2;
	public Text stud_name3;
	public Text stud_name4;
	public Text definition_down;
	public Text definition_right;
	public Text definition_up;
	public Text definition_left;
	public string answer_down;
	public string answer_right;
	public string answer_up;
	public string answer_left;
	char[] ch_ans_down;
	char[] ch_ans_right;
	char[] ch_ans_up;
	char[] ch_ans_left;
	public string all_answers;
	int[] id;
	public float time1;
	public float time2;
	public float time3;
	public float time4;



	//Handling Server Stuff
	public void Start(){	
		StartCoroutine (WaitForRequest ());
	}
	IEnumerator WaitForRequest(){
		using (UnityWebRequest www = UnityWebRequest.Get("http://51.145.24.155:3000/client/info")){
			//Full Scene Json https://8d88de88-1537-49aa-9056-4904cfcb1f59.mock.pstmn.io/client/info
			//https://22a6614f-8cdb-4a51-9d52-8a513e759a6c.mock.pstmn.io/client/info
			//json I needed is https://7356c877-5caf-446d-a4b1-e9863535a5fa.mock.pstmn.io/client/info
			yield return www.Send();
			if ((www.error == null) && www.downloadHandler.text != "{}") {
				jsonString = www.downloadHandler.text;
				dynamic_scene = CreateFromJSON (jsonString);
				displayNames ();
				load_boxes();
				load_all_letters ();

			}
			else{
				Debug.Log(www.error);
			}
		}
	}

	/* ////////    Json Serialisation               ///////// */

	public static DynamicScene CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<DynamicScene>(jsonString);
	}


	public void displayNames(){
		stud_name1.text = dynamic_scene.get_student1();
		stud_name2.text = dynamic_scene.get_student2();
		stud_name3.text = dynamic_scene.get_student3();
		stud_name4.text = dynamic_scene.get_student4();
		definition_down.text = dynamic_scene.get_definition1();
		definition_right.text = dynamic_scene.get_definition2();
		definition_up.text = dynamic_scene.get_definition3();
		definition_left.text = dynamic_scene.get_definition4();
		answer_down = dynamic_scene.get_answer1 ().ToUpper();
		ch_ans_down = answer_down.ToCharArray();
		answer_right = dynamic_scene.get_answer2 ().ToUpper();
		ch_ans_right = answer_right.ToCharArray();
		answer_up = dynamic_scene.get_answer3().ToUpper();
		ch_ans_up = answer_up.ToCharArray();
		answer_left = dynamic_scene.get_answer4().ToUpper();
		ch_ans_left = answer_left.ToCharArray();

	}

	public void send_id(){
		id = dynamic_scene.get_id ();
		GameObject.Find ("Canvas").GetComponent ("submit_Script").SendMessage ("get_id", id);

	}

	/* ////////   Dynamically Loading game Boxes And Letters ///////// */

	public void load_boxes(){
		load_boxes_down();
		load_boxes_right ();
		load_boxes_up ();
		load_boxes_left ();
	}

	public void load_all_letters(){
		load_letters(ch_ans_down);

		load_letters(ch_ans_right);

		load_letters(ch_ans_up);

		load_letters(ch_ans_left);
		all_answers = answer_down + "," + answer_right + "," + answer_up + "," + answer_left;
	}

	public void load_boxes_down(){
		float x = coordinator(ch_ans_down.Length);
		Vector3 down_initial = new Vector3 (x, -4.2f, 0f);
		for (int i = 1; i < ch_ans_down.Length + 1; i++) {
			GameObject.Find ("Box_D" + i.ToString()).transform.position = down_initial;
			x += 0.84f;
			down_initial = new Vector3(x,-4.2f,0f);
		 }
	}
		
	private void load_boxes_right(){
		float y = coordinator(ch_ans_right.Length);
		Vector3 right_initial = new Vector3 (6.0f, y, 0f);
		for (int i = 1; i < ch_ans_right.Length + 1; i++) {
			GameObject.Find ("Box_R" + i.ToString()).transform.position = right_initial;
			y += 0.84f;
			right_initial = new Vector3(6.0f,y,0f);
		}
		
	}

	private void load_boxes_up(){
		float x = coordinator(ch_ans_up.Length);
		Vector3 up_initial = new Vector3 (-x, 4.2f, 0f);
		for (int i = 1; i < ch_ans_up.Length + 1; i++) {
			GameObject.Find ("Box_U" + i.ToString()).transform.position = up_initial;
			x += 0.84f;
			up_initial = new Vector3(-x,4.2f,0f);
		}
	}

	//Changed 5.2f to 7.0 f for surface tablet
	private void load_boxes_left(){
		float y = coordinator(ch_ans_left.Length);
		Vector3 left_initial = new Vector3 (-6.0f, -y, 0f);
		for (int i = 1; i < ch_ans_left.Length + 1; i++) {
			GameObject.Find ("Box_L" + i.ToString()).transform.position = left_initial;
			y += 0.84f;
			left_initial = new Vector3(-6.0f,-y,0f);
		}
	}


	public float coordinator(int size){
		if (size == 2) {
			return -0.42f;
		}

		if (size == 3) {
			return -0.84f;
		}

		if (size == 4) {
			return -1.26f;
		}

		if (size == 5) {
			return -1.68f;
		}

		if (size == 6) {
			return -2.1f;
		}

		if (size == 7) {
			return -2.52f;
		}
		if (size == 8) {
			return -2.94f;
		} 
		else {
			return 0.0f;
		}
	}

	void give_answers(){
		GameObject.Find ("Canvas").GetComponent ("submit_Script").SendMessage ("receive_answers", all_answers);
	} 



	void load_letters(char[] letterArray){
		for (int i = 0; i < letterArray.Length; i++) {
				if (letterArray[i] == 'A') {
					(Instantiate (A, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'B') {
					(Instantiate (B, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'C') {
					(Instantiate (C, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'D') {
					(Instantiate (D, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'E') {
					(Instantiate (E, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'F') {
					(Instantiate (F, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'G') {
					(Instantiate (G, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'H') {
					(Instantiate (H, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'I') {
					(Instantiate (I, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'J') {
					(Instantiate (J, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'K') {
					(Instantiate (K, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'L') {
					(Instantiate (L, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'M') {
					(Instantiate (M, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'N') {
					(Instantiate (N, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'O') {
					(Instantiate (O, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'P') {
					(Instantiate (P, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'Q') {
					(Instantiate (Q, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'R') {
					(Instantiate (R, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'S') {
					(Instantiate (S, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'T') {
					(Instantiate (T, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'U') {
					(Instantiate (U, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'V') {
					(Instantiate (V, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'W') {
					(Instantiate (W, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'X') {
					(Instantiate (X, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'Y') {
					(Instantiate (Y, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else if (letterArray [i] == 'Z') {
					(Instantiate (Z, new Vector3 (Random.Range (-1.5f, 1.5f), Random.Range (-1.5f, 1.8f), 0), Quaternion.identity)).transform.eulerAngles = new Vector3 (0, 0, Random.Range (0.0f, 360.0f));
				} else {
				}
			}


		}


}