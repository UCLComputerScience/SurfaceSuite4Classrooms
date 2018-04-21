using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class DynamicScene{
	public string student1;
	public string student2;
	public string student3;
	public string student4;

	public string definition1;
	public string definition2;
	public string definition3;
	public string definition4;

	public string answer1;
	public string answer2;
	public string answer3;
	public string answer4;

	public int[] id;


	public string get_student1(){
		return student1;
	}

	public string get_student2(){
		return student2;
	}

	public string get_student3(){
		return student3;
	}

	public string get_student4(){
		return student4;
	}

	public string get_definition1(){
		return definition1;
	}

	public string get_definition2(){
		return definition2;
	}

	public string get_definition3(){
		return definition3;
	}

	public string get_definition4(){
		return definition4;
	}

	public string get_answer1(){
		return answer1;
	}

	public string get_answer2(){
		return answer2;
	}

	public string get_answer3(){
		return answer3;
	}

	public string get_answer4(){
		return answer4;
	}

	public int[] get_id(){
		return id;
	}

}
