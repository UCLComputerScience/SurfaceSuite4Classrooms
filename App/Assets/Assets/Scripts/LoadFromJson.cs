//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//
//public class LoadFromJson : MonoBehaviour
//{
//
//    string path;
//    string jsonString;
//
//    public void Start()
//    {
//        //path = Application.streamingAssetsPath + "/myJson.json";
//        //jsonString = File.ReadAllText(path);
//        //Letter l = JsonUtility.FromJson<Letter>(jsonString);
//        //Debug.Log(l.Name);
//        path = Application.streamingAssetsPath + "/myJson.json";
//        Letter myObject = new Letter();
//        myObject.Name = "H";
//        myObject.Tag = "Godzila";
//        string json = JsonUtility.ToJson(myObject);
//        JsonUtility.FromJsonOverwrite(path, myObject);
//
//    }
//}
//
//
//[System.Serializable]
//class Letter
//{
//    public string Name; //preferebly call with lower letter : name
//    public string Tag;
//
//}
