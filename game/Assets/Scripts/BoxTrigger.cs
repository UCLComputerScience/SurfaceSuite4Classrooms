using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BoxTrigger : MonoBehaviour{
	string result;
	string touchingObjcet;
	string name;
	Vector3 down = new Vector3 (0, 0, 0);
	Vector3 right = new Vector3 (0, 0, 90);
	Vector3 up = new Vector3(0,0,180);
	Vector3 left = new Vector3(0,0,270);
    void OnTriggerEnter2D(Collider2D colInfo){

    }

    void OnTriggerStay2D(Collider2D colInfo){
		//Transforming objects position to the box's center


		Vector3 elempos = transform.position;
		Vector3 newposition = colInfo.transform.position;
		colInfo.transform.position = elempos;
		if ((gameObject.name).Remove (gameObject.name.Length - 1) == "Box_D") {
			colInfo.transform.eulerAngles = down;
		} else if ((gameObject.name).Remove (gameObject.name.Length - 1) == "Box_R") {
			colInfo.transform.eulerAngles = right;
		} else if ((gameObject.name).Remove (gameObject.name.Length - 1) == "Box_U") {
			colInfo.transform.eulerAngles = up;
		} else if ((gameObject.name).Remove (gameObject.name.Length - 1) == "Box_L") {
			colInfo.transform.eulerAngles = left;
		}
		//Get Object's tagname wich touches the Trigger
		name = colInfo.tag;

	}

    void OnTriggerExit2D(){
		
    }


	//Checks for box whether correct letter was received or not, if yes sends 1, if not send 0 to grading system.
	void getBoxAndLetter(){
		{
			result = gameObject.name + " " + name;
			GameObject.Find ("Canvas").GetComponent ("submit_Script").SendMessage ("receiveBoxLetter", result);	

		}

	}
    
}