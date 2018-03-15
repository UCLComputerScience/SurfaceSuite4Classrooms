using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour {

    public void playGame()
    {

        Debug.Log("Scene Loading");
        SceneManager.LoadScene("Scene1");

        //To Load Scene Next To the qeue Indicated by BuildSettings:
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exit(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
