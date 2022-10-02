using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void GameStart(){
        SceneManager.LoadScene("Stage1"); 
    }

    void Update()
    {
        // Escでタイトルへtest
        if (Input.GetKey (KeyCode.Escape)) { 
            SceneManager.LoadScene("Title");
        }
    }

    
}
