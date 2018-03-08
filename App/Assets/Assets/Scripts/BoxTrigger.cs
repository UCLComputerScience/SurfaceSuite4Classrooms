using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BoxTrigger : MonoBehaviour{


   
    void OnTriggerEnter2D(Collider2D colInfo){

		//To Understad which object is touching the box
		//Debug.Log(colInfo.gameObject.tag);
        //For RotTion
        //colInfo.transform.Rotate(0, 0, 90);

    }

    void OnTriggerStay2D(Collider2D colInfo){
        //Get Object's name wich touches the Trigger
        string a = colInfo.tag;
        //Transforming objects position to the box's center
        Vector3 elempos = transform.position;
        Vector3 newposition = colInfo.transform.position;
        colInfo.transform.position = elempos;

    }

    void OnTriggerExit2D(){
        
    }

    
}